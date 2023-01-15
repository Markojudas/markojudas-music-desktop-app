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
        static string _nameOfBand;
        static string _imagePath;
        static string _nameOfAlbum;
        static string _pathOfAlbumCover;
        private static Secret _secret;
        private static bool _okToProceedToDb;

        public AddBand()
        {
            _okToProceedToDb = true;
            _secret = new Secret();
            InitializeComponent();
        }

        private void btnBandPhoto_Click(object sender, EventArgs e)
        {
            var path = new OpenFileDialog
            {
                Filter = @"Image Files(*.JPEG;*.JPG;*.PNG;*.BMP)|*.JPEG;*.JPG;*.PNG;*.BMP"
            };
            var res = path.ShowDialog();

            if (res != DialogResult.OK) return;
            //Getting the band-photo
            var imgPath = path.FileName;
            bandImagePath.Text = imgPath;

        }

        private void BtnAlbumPhoto_Click(object sender, EventArgs e)
        {

            var path = new OpenFileDialog
            {
                Filter = @"Image Files(*.JPEG;*.JPG;*.PNG;*.BMP)|*.JPEG;*.JPG;*.PNG;*.BMP"
            };
            var res = path.ShowDialog();

            if (res != DialogResult.OK) return;
            var imgPath = path.FileName;
            albumCoverImgPath.Text = imgPath;
        }

        private void OkBtn_Click(object sender, EventArgs e)
        {
            if (bname.Text.Length <= 0 ||
                aName.Text.Length <= 0 ||
                bandImagePath.Text.Length <= 0 ||
                albumCoverImgPath.Text.Length <= 0)
            {
                MessageBox.Show(@"Add the fields", @"error");
                return;
            }

            _nameOfBand = bname.Text;
            _imagePath = bandImagePath.Text;
            var getExtImgPath = _imagePath.Split('.');
            var extImagePath = getExtImgPath[getExtImgPath.Length - 1];

            _nameOfAlbum = aName.Text;
            _pathOfAlbumCover = albumCoverImgPath.Text;
            var getExtAlbumPhotoPath = _pathOfAlbumCover.Split('.');
            var extAlbumPath = getExtAlbumPhotoPath[getExtAlbumPhotoPath.Length - 1];

            var editNameOfBand = _nameOfBand.Replace(" ", "-");
            var editNameOfAlbum = _nameOfAlbum
                .Replace(" ", "-")
                .Replace(",", "")
                .Replace(":", "");


            var s3Client = new AmazonS3Client(new StoredProfileAWSCredentials("Music Uploader"), RegionEndpoint.USEast1);

            //Console.WriteLine(editNameOfBand.ToLower());
            const string bucketname = "markojudas-music";

            UploadFileAsync(s3Client, bucketname, editNameOfBand.ToLower(), editNameOfAlbum.ToLower(), extImagePath, extAlbumPath);
            UploadToDb(_nameOfBand, _nameOfAlbum, extImagePath, extAlbumPath);

            this.Close();
        }

        private static void UploadToDb(
            string bandName,
            string albumName,
            string extBand,
            string extAlbum
        )
        {
            if (!_okToProceedToDb)
            {
                return;
            }

            var editBandName = bandName.ToLower().Replace(" ", "-");
            var editAlbumName = albumName.ToLower()
                .Replace(" ", "-")
                .Replace(",", "")
                .Replace(":", "");

            var s3Bucketpath = Secret.S3Bucket;

            var dbstring = Secret.ConnectionString;

            if (s3Bucketpath == null || dbstring == null)
            {
                MessageBox.Show(@"Please Check the .env File", @"Error Parsing Secrets");
                return;
            }

            var bandPath = s3Bucketpath +
                           editBandName +
                           "/band-photo/" +
                           editBandName + "." +
                           extBand;
            var albumPath = s3Bucketpath +
                            editBandName +
                            "/albums/" +
                            editAlbumName + "." +
                            extAlbum;

            var client = new MongoClient(dbstring);
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
                MessageBox.Show(@"Error Uploading Band: " + e, @"Error");
                return;
            }

            MessageBox.Show(@"Uploaded Band!", @"Success!");
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
                MessageBox.Show(@"Band Already Here!", @"ERROR");
                _okToProceedToDb = false;
                return;
            }

            _okToProceedToDb = true;

            //**** ADDING BAND PHOTO****
            var bandPhotoPathS3 = "Band/" + bandObjectName + "/band-photo/" + bandObjectName + "." + bandExt;
            var bandPhotoFile = new FileInfo(_imagePath);


            var request = new PutObjectRequest
            {
                InputStream = bandPhotoFile.OpenRead(),
                BucketName = bucketName,
                Key = bandPhotoPathS3
            };

            var response = client.PutObject(request);
            if (response.HttpStatusCode == System.Net.HttpStatusCode.OK)
            {
                Console.WriteLine(@"Added Band Photo");
            }
            else
            {
                Console.WriteLine(@"Error uploading photos");
                return;
            }
            //**************************

            //**** ADDING ALBUM PHOTO*****
            var albumPhotoPathS3 = "Band/" + bandObjectName + "/albums/" + albumObjectName + "." + albumExt;
            var albumPhotoFile = new FileInfo(_pathOfAlbumCover);

            var request2 = new PutObjectRequest
            {
                InputStream = albumPhotoFile.OpenRead(),
                BucketName = bucketName,
                Key = albumPhotoPathS3
            };

            var response2 = client.PutObject(request2);
            Console.WriteLine(response2.HttpStatusCode == System.Net.HttpStatusCode.OK
                ? @"Successfully Added Album Cover"
                : @"Error uploading photos");
            //*****************************
        }

        private static bool ExistsFile(IAmazonS3 client, string bucketName, string bandName)
        {
            var isFound = false;

            var request = new ListObjectsRequest
            {
                BucketName = bucketName,
                Prefix = "Band/" + bandName
            };
            do
            {
                var response = client.ListObjects(request);
                foreach (var obj in response.S3Objects)
                {
                    if (!obj.Key.Contains(bandName)) continue;
                    isFound = true;
                    break;
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
