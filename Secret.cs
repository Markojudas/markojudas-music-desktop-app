using System;
using System.IO;
using System.Windows.Forms;

namespace markojudas_music
{
    internal class Secret
    {
        public static string ConnectionString { get; private set; }
        private static string DbUser { get; set; }
        private static string DbPw { get; set; }
        private static string DbCluster { get; set; }
        public static string S3Bucket { get; private set; }

        public Secret()
        {
            SetSecrets();

            if (DbUser != null && DbPw != null && DbCluster != null)
            {
                ConnectionString = "mongodb+srv://" + DbUser + ":" + DbPw + "@" + DbCluster +
                                   "/?retryWrites=true&w=majority";
            }
        }

        private static void SetSecrets()
        {
            var getEnvFile = Path.GetFullPath(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "..\\..\\.env"));

            if (!File.Exists(getEnvFile))
            {
                MessageBox.Show("Missing .env file with Secrets", "Error");
                return;
            }

            var lines = File.ReadAllLines(getEnvFile);

            if (lines.Length == 0)
            {
                MessageBox.Show("File is Empty", "Error");
                return;
            }

            foreach (var line in lines)
            {
                var parts = line.Split('=');

                if (parts.Length < 2) continue;

                switch (parts[0])
                {
                    case "dbUser":
                        DbUser = parts[1];
                        break;
                    case "dbPW":
                        if (parts.Length > 2)
                        {
                            DbPw = "";
                            for (var i = 1; i < parts.Length; i++)
                            {
                                if (i != parts.Length - 1)
                                {
                                    DbPw += parts[i] + "=";
                                }
                                else
                                {
                                    DbPw += parts[i];
                                }
                            }
                        }
                        break;
                    case "dbCluster":
                        DbCluster = parts[1];
                        break;
                    case "s3BucketPath":
                        S3Bucket = parts[1];
                        break;
                }
            }
        }
    }
}
