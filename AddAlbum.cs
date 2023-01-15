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
        private static string _nameOfBand;
        private static string _titleOfAlbum;
        private static string _albumCover;
        private static Secret _secret;
        private static bool _bandFound;
        public NewAlbum()
        {
            _bandFound = false;
            _secret = new Secret();
            InitializeComponent();
        }

        private void OkBtn_Click(object sender, EventArgs e)
        {
            if (bName.Text.Length <= 0 ||
                aName.Text.Length <= 0 ||
                photoPath.Text.Length <= 0)
            {
                MessageBox.Show(@"Need to complete all fields!", @"error");
                return;
            }

            _nameOfBand = bName.Text;
            _titleOfAlbum = aName.Text;
            _albumCover = photoPath.Text;

            UploadObject();
            UpdateDb();

            this.Close();
        }

        private static void UpdateDb()
        {
            if (!_bandFound)
            {
                return;
            }

            var editedBandName = _nameOfBand.ToLower().Replace(" ", "-");
            var editedAlbumName = _titleOfAlbum.ToLower().Replace(" ", "-");

            var getExt = _albumCover.Split('.');
            var albumExt = getExt[1];

            var s3Bucketpath = Secret.S3Bucket;

            var dbstring = Secret.ConnectionString;

            if (s3Bucketpath == null || dbstring == null)
            {
                MessageBox.Show(@"Please Check the .env File", @"Error Parsing Secrets");
                return;
            }

            var aCoverPath = s3Bucketpath +
                             editedBandName +
                             "/albums/" +
                             editedAlbumName +
                             "." +
                             albumExt;


            var client = new MongoClient(dbstring);
            var db = client.GetDatabase("music-library");
            var collection = db.GetCollection<BsonDocument>("bands");
            var filter = Builders<BsonDocument>.Filter.Eq("name", _nameOfBand);

            var albumDoc = new BsonDocument
            {
                { "albumName", _titleOfAlbum },
                { "albumCoverPath", aCoverPath }
            };
            var update = Builders<BsonDocument>.Update.Push("albums", albumDoc);

            var updateResult = collection.UpdateOne(filter, update);

            Console.WriteLine(updateResult.MatchedCount <= 0 ? @"Error updating DB" : @"Successfully added to DB");
        }

        private static void UploadObject()
        {
            var s3Client = new AmazonS3Client(new StoredProfileAWSCredentials("Music Uploader"), RegionEndpoint.USEast1);
            var editedBandName = _nameOfBand.ToLower().Replace(" ", "-");
            var editedAlbumName = _titleOfAlbum.ToLower().Replace(" ", "-");

            var stringSplit = _albumCover.Split('.');
            var albumCoverExt = stringSplit[stringSplit.Length - 1];

            var s3Path = "Band/" + editedBandName + "/albums/" + editedAlbumName + "." + albumCoverExt;
            var albumCoverFile = new FileInfo(_albumCover);

            var bucketname = "markojudas-music";


            if (!ExistsFile(s3Client, bucketname, editedBandName, editedAlbumName, albumCoverExt))
            {
                MessageBox.Show(@"Band Not Found or Album Already Exists", @"Error");
                _bandFound = false;
                return;
            }

            _bandFound = true;

            var request = new PutObjectRequest
            {
                InputStream = albumCoverFile.OpenRead(),
                BucketName = bucketname,
                Key = s3Path
            };
            var response = s3Client.PutObject(request);
            if (response.HttpStatusCode == System.Net.HttpStatusCode.OK)
            {
                MessageBox.Show(@"New Album Added!", @"Success!");
            }
            else
            {
                MessageBox.Show(@"Error uploading new album", @"Error");
            }
        }

        private static bool ExistsFile(IAmazonS3 client, string bucketName, string bandName, string albumName, string ext)
        {
            var bandIsFound = false;
            var albumIsFound = false;
            var proceed = false;

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

        private void AddImage_Click(object sender, EventArgs e)
        {
            var path = new OpenFileDialog
            {
                Filter = @"Image Files(*.JPEG;*.JPG;*.PNG;*.BMP)|*.JPEG;*.JPG;*.PNG;*.BMP"
            };
            var res = path.ShowDialog();

            if (res != DialogResult.OK) return;
            var imgPath = path.FileName;
            photoPath.Text = imgPath;
        }
    }
}
