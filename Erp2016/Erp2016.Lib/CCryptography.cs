using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Erp2016.Lib
{
    public class CCryptography
    {
        private const string SecurityKey = "Erp2016";

        /// <summary>
        /// This method is used to convert the plain text to Encrypted/Un-Readable Text format.
        /// </summary>
        /// <param name="plainText">Plain Text to Encrypt before transferring over the network.</param>
        /// <returns>Cipher Text</returns>
        public static string EncryptPlainTextToCipherText(string plainText)
        {
            //Getting the bytes of Input String.
            var toEncryptedArray = Encoding.UTF8.GetBytes(plainText);

            var objMd5CryptoService = new MD5CryptoServiceProvider();

            //Gettting the bytes from the Security Key and Passing it to compute the Corresponding Hash Value.
            var securityKeyArray = objMd5CryptoService.ComputeHash(Encoding.UTF8.GetBytes(SecurityKey));

            //De-allocatinng the memory after doing the Job.
            objMd5CryptoService.Clear();

            var objTripleDesCryptoService = new TripleDESCryptoServiceProvider
            {
                Key = securityKeyArray,
                Mode = CipherMode.ECB,
                Padding = PaddingMode.PKCS7
            };

            var objCrytpoTransform = objTripleDesCryptoService.CreateEncryptor();

            //Transform the bytes array to resultArray
            var resultArray = objCrytpoTransform.TransformFinalBlock(toEncryptedArray, 0, toEncryptedArray.Length);

            //Releasing the Memory Occupied by TripleDES Service Provider for Encryption.
            objTripleDesCryptoService.Clear();

            //Convert and return the encrypted data/byte into string format.
            return Convert.ToBase64String(resultArray, 0, resultArray.Length);
        }


        /// <summary>
        /// This method is used to convert the Cipher/Encypted text to Plain Text.
        /// </summary>
        /// <param name="cipherText">Encrypted Text</param>
        /// <returns>Plain/Decrypted Text</returns>
        public static string DecryptCipherTextToPlainText(string cipherText)
        {
            try
            {
                var toEncryptArray = Convert.FromBase64String(cipherText);

                var objMd5CryptoService = new MD5CryptoServiceProvider();

                //Gettting the bytes from the Security Key and Passing it to compute the Corresponding Hash Value.
                var securityKeyArray = objMd5CryptoService.ComputeHash(Encoding.UTF8.GetBytes(SecurityKey));

                //De-allocatinng the memory after doing the Job.
                objMd5CryptoService.Clear();

                var objTripleDesCryptoService = new TripleDESCryptoServiceProvider
                {
                    Key = securityKeyArray,
                    Mode = CipherMode.ECB,
                    Padding = PaddingMode.PKCS7
                };

                var objCrytpoTransform = objTripleDesCryptoService.CreateDecryptor();

                //Transform the bytes array to resultArray
                var resultArray = objCrytpoTransform.TransformFinalBlock(toEncryptArray, 0, toEncryptArray.Length);

                //Releasing the Memory Occupied by TripleDES Service Provider for Decryption.          
                objTripleDesCryptoService.Clear();

                //Convert and return the decrypted data/byte into string format.
                return Encoding.UTF8.GetString(resultArray);
            }
            catch (Exception ex)
            {
                Debug.Print(ex.Message);
            }

            return string.Empty;
        }
    }
}
