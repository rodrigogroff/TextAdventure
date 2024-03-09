using System.IO.Compression;

namespace TA_Update
{
    public class GameUpdater
    {
        public string dir = AppDomain.CurrentDomain.BaseDirectory;

        public bool CheckForUpdate()
        {
            var file = dir + "/ta_upgrade.zip";

            return File.Exists(file);
        }

        public bool Update()
        {
            var file = dir + "/ta_upgrade.zip";

            if (File.Exists(file))
            {
                Extract(file);
                File.Delete(file);
                return true;
            }

            return false;
        }

        public void Extract(string zipFile)
        {
            using (var ZipArchive = ZipFile.OpenRead(zipFile))
            {
                foreach (ZipArchiveEntry entry in ZipArchive.Entries)
                {
                    var entryFullname = Path.Combine(dir, entry.FullName);
                    var entryPath = Path.GetDirectoryName(entryFullname);

                    if (!Directory.Exists(entryPath))
                        Directory.CreateDirectory(entryPath);

                    var entryFn = Path.GetFileName(entryFullname);

                    if (!string.IsNullOrEmpty(entryFn))

                    {
                        if (File.Exists(entryFullname))
                            File.Delete(entryFullname);

                        entry.ExtractToFile(entryFullname, true);
                    }
                }
            }
        }

    }
}
