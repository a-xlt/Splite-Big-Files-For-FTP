using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Splite_Big_Files_For_FTP
{
    internal class ENC
    {
        public static byte[] EncryptAES(byte[] plainBytes, byte[] key, byte[] iv)
        {
            AesManaged aes = new AesManaged();
            aes.Key = key;
            aes.IV = iv;
            MemoryStream memoryStream = new MemoryStream();
            CryptoStream cryptoStream = new CryptoStream(memoryStream, aes.CreateEncryptor(), CryptoStreamMode.Write);
            cryptoStream.Write(plainBytes, 0, plainBytes.Length);
            cryptoStream.FlushFinalBlock();
            return memoryStream.ToArray();

        }

        public static byte[] DecryptAES(byte[] encryptedBytes, byte[] key, byte[] iv)
        {
            AesManaged aes = new AesManaged();
            aes.Key = key;
            aes.IV = iv;
            MemoryStream memoryStream = new MemoryStream();
            CryptoStream cryptoStream = new CryptoStream(memoryStream, aes.CreateDecryptor(), CryptoStreamMode.Write);
            cryptoStream.Write(encryptedBytes, 0, encryptedBytes.Length);
            cryptoStream.FlushFinalBlock();
            return memoryStream.ToArray();
        }
    }
}
