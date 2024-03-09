using System.Security.Cryptography;
using System.Text;

namespace TextAdventure.Infra
{
    public class FileEncryptorDecryptor
    {
        private readonly byte[] key = { 0x3F, 0x30, 0xC3, 0xE9, 0x92, 0xA6, 0xBB, 0xD9, 0xD7, 0xCE, 0xC4, 0xEF, 0xE7, 0x9F, 0x8B, 0xEB };
        private readonly byte[] iv = { 0x21, 0xA3, 0xB3, 0x44, 0xD9, 0xE7, 0xA0, 0xC7, 0x21, 0xA3, 0xB3, 0x44, 0xD9, 0xE7, 0xA0, 0xC7 }; // IV size matches AES block size

        public void EncryptFile(string inputFile, string outputFile)
        {
            if (File.Exists(outputFile))
                File.Delete(outputFile);

            using (FileStream fsInput = new FileStream(inputFile, FileMode.Open))
            using (FileStream fsOutput = new FileStream(outputFile, FileMode.Create))
            {
                using (Aes aes = Aes.Create())
                {
                    aes.Key = key;
                    aes.IV = iv;
                    ICryptoTransform encryptor = aes.CreateEncryptor(aes.Key, aes.IV);

                    using (CryptoStream cs = new CryptoStream(fsOutput, encryptor, CryptoStreamMode.Write))
                    {
                        fsInput.CopyTo(cs);
                    }
                }
            }           
        }

        public void EncryptContent(string inputFile, string content)
        {
            if (File.Exists(inputFile))
                File.Delete(inputFile);

            byte[] bytesToEncrypt = Encoding.UTF8.GetBytes(content);

            using (FileStream fsOutput = new FileStream(inputFile, FileMode.Create))
            {
                using (Aes aes = Aes.Create())
                {
                    aes.Key = key;
                    aes.IV = iv;
                    ICryptoTransform encryptor = aes.CreateEncryptor(aes.Key, aes.IV);

                    using (CryptoStream cs = new CryptoStream(fsOutput, encryptor, CryptoStreamMode.Write))
                    {
                        cs.Write(bytesToEncrypt, 0, bytesToEncrypt.Length);
                    }
                }
            }
        }

        public string DecryptFile(string inputFile)
        {
            if (!File.Exists(inputFile))
                return "";

            using (FileStream fsInput = new FileStream(inputFile, FileMode.Open))
            using (MemoryStream msOutput = new MemoryStream())
            {
                using (Aes aes = Aes.Create())
                {
                    aes.Key = key;
                    aes.IV = iv;
                    ICryptoTransform decryptor = aes.CreateDecryptor(aes.Key, aes.IV);

                    using (CryptoStream cs = new CryptoStream(fsInput, decryptor, CryptoStreamMode.Read))
                    {
                        cs.CopyTo(msOutput);
                    }
                }
                return Encoding.UTF8.GetString(msOutput.ToArray());
            }
        }
    }
}
