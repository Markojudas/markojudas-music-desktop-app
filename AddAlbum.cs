using Amazon;
using Amazon.Runtime;
using Amazon.S3;
using System;
using System.IO;
using System.Windows.Forms;
using Amazon.S3.Model;
using MongoDB.Bson;
using MongoDB.Driver;

namespace markojudas_music
{
    public partial class NewAlbum : Form
    {
        private static string nameOfBand;
        private static string titleOfAlbum;
        private static string albumCover;
        private static Secret secret;
        private static bool bandFound;
        public NewAlbum()
        {
            bandFound = false;
            secret = new Secret();
            InitializeComponent();
        }

        private void okBtn_Click(object sender, EventArgs e)
        {
            if (bName.Text.Length <= 0 ||
                aName.Text.Length <= 0 ||
                photoPath.Text.Length <= 0)
            {
                MessageBox.Show("Need to complete all fields!", "error");
                return;
            }

            nameOfBand = bName.Text;
            titleOfAlbum = aName.Text;
            albumCover = photoPath.Text;

            UploadObject();
            UpdateDb();

            this.Close();
        }

        private static void UpdateDb()
        {
            if (!bandFound)
            {
                return;
            }

            var editedBandName = nameOfBand.ToLower().Replace(" ", "-");
            var editedAlbumName = titleOfAlbum.ToLower().Replace(" ", "-");

            var getExt = albumCover.Split('.');
            var albumExt = getExt[1];

            var S3BUCKETPATH = Secret.S3Bucket;

            var aCoverPath = S3BUCKETPATH +
                             editedBandName +
                             "/albums/" +
                             editedAlbumName +
                             "." +
                             albumExt;

            var DBSTRING = Secret.ConnectionString;

            var client = new MongoClient(DBSTRING);
            var db = client.GetDatabase("music-library");
            var collection = db.GetCollection<BsonDocument>("bands");
            var filter = Builders<BsonDocument>.Filter.Eq("name", nameOfBand);

            var albumDoc = new BsonDocument
            {
                { "albumName", titleOfAlbum },
                { "albumCoverPath", aCoverPath }
            };
            var update = Builders<BsonDocument>.Update.Push("albums", albumDoc);

            var updateResult = collection.UpdateOne(filter, update);

            if (updateResult.MatchedCount <= 0)
            {
                Console.WriteLine("Error updating DB");
            }
            else
            {
                Console.WriteLine("Successfully added to DB");
            }
        }

        private static void UploadObject()
        {
            var s3Client = new AmazonS3Client(new StoredProfileAWSCredentials("Music Uploader"), RegionEndpoint.USEast1);
            var editedBandName = nameOfBand.ToLower().Replace(" ", "-");
            var editedAlbumName = titleOfAlbum.ToLower().Replace(" ", "-");

            var stringSplit = albumCover.Split('.');
            var albumCoverExt = stringSplit[stringSplit.Length - 1];

            var s3Path = "Band/" + editedBandName + "/albums/" + editedAlbumName + "." + albumCoverExt;
            var albumCoverFile = new FileInfo(albumCover);

            var BUCKETNAME = "markojudas-music";


            if (!ExistsFile(s3Client, BUCKETNAME, editedBandName, editedAlbumName, albumCoverExt))
            {
                MessageBox.Show("Band Not Found or Album Already Exists", "Error");
                bandFound = false;
                return;
            }

            bandFound = true;

            var request = new PutObjectRequest
            {
                InputStream = albumCoverFile.OpenRead(),
                BucketName = BUCKETNAME,
                Key = s3Path
            };
            var response = s3Client.PutObject(request);
            if (response.HttpStatusCode == System.Net.HttpStatusCode.OK)
            {
                MessageBox.Show("New Album Added!", "Success!");
            }
            else
            {
                MessageBox.Show("Error uploading new album", "Error");
            }
        }

        public static bool ExistsFile(IAmazonS3 client, string bucketName, string bandName, string albumName, string ext)
        {
            bool bandIsFound = false;
            bool albumIsFound = false;
            bool proceed = false;

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
                        bandIsFound = true;
                    }
                    if (obj.Key == "Band/" + bandName + "/albums/" + albumName + "." + ext)
                    {
                        albumIsFound = true;
                    }
                }

                if (bandIsFound && !albumIsFound)
                {
                    proceed = true;
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

            return proceed;
        }

        private void addImage_Click(object sender, EventArgs e)
        {
            var path = new OpenFileDialog();
            path.Filter = "Image Files(*.JPEG;*.JPG;*.PNG;*.BMP)|*.JPEG;*.JPG;*.PNG;*.BMP";
            var res = path.ShowDialog();

            if (res != DialogResult.OK) return;
            var imgPath = path.FileName;
            photoPath.Text = imgPath;
        }
    }
}
