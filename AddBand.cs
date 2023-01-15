using System;
using System.IO;
using System.Windows.Forms;
using Amazon;
using Amazon.Runtime;
using Amazon.S3;
using Amazon.S3.Model;
using MongoDB.Bson;
using MongoDB.Driver;

namespace markojudas_music
{
    public partial class AddBand : Form
    {
        static string nameOfBand;
        static string imagePath;
        static string nameOfAlbum;
        static string pathOfAlbumCover;
        private static Secret secret;
        private static bool okToProceedToDB;

        public AddBand()
        {
            okToProceedToDB = true;
            secret = new Secret();
            InitializeComponent();
        }

        private void btnBandPhoto_Click(object sender, EventArgs e)
        {
            //Getting the band-photo
            string imgPath = "";
            OpenFileDialog path = new OpenFileDialog();
            path.Filter = "Image Files(*.JPEG;*.JPG;*.PNG;*.BMP)|*.JPEG;*.JPG;*.PNG;*.BMP";
            DialogResult res = path.ShowDialog();

            if (res == DialogResult.OK)
            {
                imgPath = path.FileName;
                bandImagePath.Text = imgPath;
            }

        }

        private void btnAlbumPhoto_Click(object sender, EventArgs e)
        {

            var path = new OpenFileDialog();
            path.Filter = "Image Files(*.JPEG;*.JPG;*.PNG;*.BMP)|*.JPEG;*.JPG;*.PNG;*.BMP";
            var res = path.ShowDialog();

            if (res != DialogResult.OK) return;
            var imgPath = path.FileName;
            albumCoverImgPath.Text = imgPath;
        }

        private void okBtn_Click(object sender, EventArgs e)
        {
            if (bname.Text.Length <= 0 ||
                aName.Text.Length <= 0 ||
                bandImagePath.Text.Length <= 0 ||
                albumCoverImgPath.Text.Length <= 0)
            {
                MessageBox.Show("Add the fields", "error");
                return;
            }

            nameOfBand = bname.Text;
            imagePath = bandImagePath.Text;
            var getExtImgPath = imagePath.Split('.');
            var extImagePath = getExtImgPath[getExtImgPath.Length - 1];

            nameOfAlbum = aName.Text;
            pathOfAlbumCover = albumCoverImgPath.Text;
            var getExtAlbumPhotoPath = pathOfAlbumCover.Split('.');
            var extAlbumPath = getExtAlbumPhotoPath[getExtAlbumPhotoPath.Length - 1];

            var editNameOfBand = nameOfBand.Replace(" ", "-");
            var editNameOfAlbum = nameOfAlbum
                .Replace(" ", "-")
                .Replace(",", "")
                .Replace(":", "");


            var s3Client = new AmazonS3Client(new StoredProfileAWSCredentials("Music Uploader"), RegionEndpoint.USEast1);

            //Console.WriteLine(editNameOfBand.ToLower());
            var BUCKETNAME = "markojudas-music";

            UploadFileAsync(s3Client, BUCKETNAME, editNameOfBand.ToLower(), editNameOfAlbum.ToLower(), extImagePath, extAlbumPath);
            UploadToDb(nameOfBand, nameOfAlbum, extImagePath, extAlbumPath);

            this.Close();
        }

        private static void UploadToDb(
            string bandName,
            string albumName,
            string extBand,
            string extAlbum
        )
        {
            if (!okToProceedToDB)
            {
                return;
            }

            var editBandName = bandName.ToLower().Replace(" ", "-");
            var editAlbumName = albumName.ToLower()
                .Replace(" ", "-")
                .Replace(",", "")
                .Replace(":", "");

            var S3BUCKETPATH = Secret.S3Bucket;

            var bandPath = S3BUCKETPATH +
                           editBandName +
                           "/band-photo/" +
                           editBandName + "." +
                           extBand;
            var albumPath = S3BUCKETPATH +
                            editBandName +
                            "/albums/" +
                            editAlbumName + "." +
                            extAlbum;

            var DBSTRING = Secret.ConnectionString;

            var client = new MongoClient(DBSTRING);
            var db = client.GetDatabase("music-library");
            var collection = db.GetCollection<BsonDocument>("bands");

            var albumsArr = new BsonArray
                {
                    new BsonDocument
                    {
                        { "albumName", albumName },
                        { "albumCoverPath", albumPath }
                    }
                };

            var document = new BsonDocument
            {
                    { "name", bandName },
                    { "imagePath", bandPath },
                    { "albums", albumsArr }
            };

            var documents = collection.Find(new BsonDocument()).ToList();
            Console.WriteLine(documents.ToString());

            try
            {
                collection.InsertOne(document);
            }
            catch (MongoWriteException e)
            {
                MessageBox.Show("Error Uploading Band: " + e, "Error");
                return;
            }

            MessageBox.Show("Uploaded Band!", "Success!");
        }

        private static void UploadFileAsync(
            IAmazonS3 client,
            string bucketName,
            string bandObjectName,
            string albumObjectName,
            string bandExt,
            string albumExt)
        {

            if (ExistsFile(client, bucketName, bandObjectName))
            {
                MessageBox.Show("Band Already Here!", "ERROR");
                okToProceedToDB = false;
                return;
            }

            okToProceedToDB = true;

            //**** ADDING BAND PHOTO****
            var bandPhotoPathS3 = "Band/" + bandObjectName + "/band-photo/" + bandObjectName + "." + bandExt;
            var bandPhotoFile = new FileInfo(imagePath);


            var request = new PutObjectRequest
            {
                InputStream = bandPhotoFile.OpenRead(),
                BucketName = bucketName,
                Key = bandPhotoPathS3
            };

            var response = client.PutObject(request);
            if (response.HttpStatusCode == System.Net.HttpStatusCode.OK)
            {
                Console.WriteLine("Added Band Photo");
            }
            else
            {
                Console.WriteLine("Error uploading photos");
                return;
            }
            //**************************

            //**** ADDING ALBUM PHOTO*****
            var albumPhotoPathS3 = "Band/" + bandObjectName + "/albums/" + albumObjectName + "." + albumExt;
            var albumPhotoFile = new FileInfo(pathOfAlbumCover);

            var request2 = new PutObjectRequest
            {
                InputStream = albumPhotoFile.OpenRead(),
                BucketName = bucketName,
                Key = albumPhotoPathS3
            };

            var response2 = client.PutObject(request2);
            if (response2.HttpStatusCode == System.Net.HttpStatusCode.OK)
            {
                Console.WriteLine("Succesfully Added Album Cover");
            }
            else
            {
                Console.WriteLine("Error uploading photos");
            }
            //*****************************
        }

        public static bool ExistsFile(IAmazonS3 client, string bucketName, string bandName)
        {
            bool isFound = false;

            ListObjectsRequest request = new ListObjectsRequest
            {
                BucketName = bucketName,
                Prefix = "Band/" + bandName
            };
            do
            {
                ListObjectsResponse response = client.ListObjects(request);
                foreach (S3Object obj in response.S3Objects)
                {
                    if (obj.Key.Contains(bandName))
                    {
                        isFound = true;
                        break;
                    }
                }

                if (response.IsTruncated)
                {
                    request.Marker = response.NextMarker;
                }
                else
                {
                    request = null;
                }

            } while (request != null);

            return isFound;
        }

    }
}
