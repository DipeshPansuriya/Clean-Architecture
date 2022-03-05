using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace Application_Common
{
    public static class Encrypt_Decrypt
    {
        public static string DecryptString(string cipherText, string key, string ivkey)
        {
            try
            {
                byte[] keybytes = Encoding.UTF8.GetBytes(key);
                byte[] iv = Encoding.UTF8.GetBytes(ivkey);

                byte[] encryptedBytes = Convert.FromBase64String(cipherText);
                AesCryptoServiceProvider aes = new AesCryptoServiceProvider
                {
                    //aes.BlockSize = 128; Not Required
                    //aes.KeySize = 256; Not Required
                    Mode = CipherMode.ECB,
                    Padding = PaddingMode.PKCS7,
                    Key = keybytes,
                    IV = iv
                };
                ICryptoTransform crypto = aes.CreateDecryptor(aes.Key, aes.IV);
                byte[] secret = crypto.TransformFinalBlock(encryptedBytes, 0, encryptedBytes.Length);
                crypto.Dispose();
                return System.Text.ASCIIEncoding.ASCII.GetString(secret);
            }
            catch (Exception)
            {
                return null;
            }
        }

        public static string EncryptString(string cipherText, string key, string ivkey)
        {
            try
            {
                byte[] encrypted;
                byte[] keybytes = Encoding.UTF8.GetBytes(key);
                byte[] iv = Encoding.UTF8.GetBytes(ivkey);

                AesCryptoServiceProvider aes = new AesCryptoServiceProvider
                {
                    //aes.BlockSize = 128; Not Required
                    //aes.KeySize = 256; Not Required
                    Mode = CipherMode.ECB,
                    Padding = PaddingMode.PKCS7,
                    Key = keybytes,
                    IV = iv
                };
                ICryptoTransform crypto = aes.CreateEncryptor(aes.Key, aes.IV);
                using (MemoryStream msEncrypt = new MemoryStream())
                {
                    using (CryptoStream csEncrypt = new CryptoStream(msEncrypt, crypto, CryptoStreamMode.Write))
                    {
                        using (StreamWriter swEncrypt = new StreamWriter(csEncrypt))
                        {
                            //Write all data to the stream.
                            swEncrypt.Write(cipherText);
                        }
                        encrypted = msEncrypt.ToArray();
                    }
                }

                return Convert.ToBase64String(encrypted);
            }
            catch (Exception)
            {
                return null;
            }
        }
    }
}