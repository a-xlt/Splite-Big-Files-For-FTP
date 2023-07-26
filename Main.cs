using System;
using System.Collections.Generic;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace Splite_Big_Files_For_FTP
{
    class Program
    {
        static void Main()
        {
            string filePath = "D:\\RAR FILE\\ubuntu-20.04.2-live-server-amd64.iso";
            FileStream fileStream = File.OpenRead(filePath);
            int bufferSize = 10240000;
            byte[] buffer = new byte[bufferSize];
            int bytesRead;
            int i = 0;
            ///////////////////////////////////////////////////////////////
            byte[] key = new byte[32];
            byte[] iv = new byte[16];
            var rng = new RNGCryptoServiceProvider();
            rng.GetBytes(key);
            rng.GetBytes(iv);
            HashAlgorithm hash = new SHA256Managed();
            ///////////////////////////////////////////////////////////////
            FileStream fileStream2 = new FileStream("C:\\Users\\Abbas J\\Desktop\\a.iso", FileMode.Append, FileAccess.Write);
            while ((bytesRead = fileStream.Read(buffer, 0, buffer.Length)) > 0)
            {
                byte[] encryptedBlock = ENC.EncryptAES(buffer, key, iv);
                File.WriteAllBytes("C:\\Users\\Abbas J\\Desktop\\test\\" + i.ToString(), encryptedBlock);
                File.WriteAllBytes("C:\\Users\\Abbas J\\Desktop\\test\\" + i.ToString() + "-sha256", hash.ComputeHash(encryptedBlock));
                Console.WriteLine(ftp2.putfile("C:\\Users\\Abbas J\\Desktop\\test\\" + i.ToString(), i.ToString()));
                Console.WriteLine("done");
                i++;
            }
            Console.WriteLine("File splitting complete.");
            string[] fileNames = Directory.GetFiles("C:\\Users\\Abbas J\\Desktop\\test\\");
            List<byte> bytesfile = new List<byte>();
            for (int j = 0; j < (fileNames.Length / 2); j++)
            {
                byte[] temp = File.ReadAllBytes("C:\\Users\\Abbas J\\Desktop\\test\\" + j.ToString());
                temp = ENC.DecryptAES(temp, key, iv);
                fileStream2.Write(temp, 0, temp.Length);
                Console.WriteLine("Done");

            }
            Console.WriteLine("File rebulding complete.");

            Console.ReadLine();
        }
    }
}
