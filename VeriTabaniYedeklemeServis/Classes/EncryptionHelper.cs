using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using VeriTabaniYedeklemeServis.Classes;

namespace AsyenVeriTabaniYedeklemeServis.Classes
{
    internal class EncryptionHelper
    {
        private static readonly byte[] Key = Encoding.UTF8.GetBytes("YourSecureKey123YourSecureKey123"); // 32 byte (AES için uygun)
        private static readonly byte[] IV = Encoding.UTF8.GetBytes("YourSecureIV1234"); // 16 byte (AES için uygun)
        internal static string Decrypt(string encryptedText)
        {
            try
            {
                using (Aes aes = Aes.Create())
                {
                    aes.Key = Key;
                    aes.IV = IV;
                    using (MemoryStream memoryStream = new MemoryStream())
                    using (CryptoStream cryptoStream = new CryptoStream(memoryStream, aes.CreateDecryptor(), CryptoStreamMode.Write))
                    {
                        byte[] encryptedBytes = Convert.FromBase64String(encryptedText);
                        cryptoStream.Write(encryptedBytes, 0, encryptedBytes.Length);
                        cryptoStream.FlushFinalBlock();
                        return Encoding.UTF8.GetString(memoryStream.ToArray());
                    }
                }
            }
            catch (Exception ex)
            {
                TextLog.TextLogging(ex.Message,"");
            }
            return null;
        }
    }
}