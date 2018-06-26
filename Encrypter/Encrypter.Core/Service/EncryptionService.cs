using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace Encrypter.Core.Service
{
    public class EncryptionService : IEncryptionService
    {
        private EncryptionDTO _encryptionDTO;

        /// <summary>
        /// Initializes a new instance of the <see cref="EncryptionService"/> class.
        /// </summary>
        /// <param name="encryptionDTO">The encryption dto.</param>
        /// <exception cref="ArgumentNullException">
        /// encryptionDTO
        /// or
        /// UserName
        /// or
        /// Password
        /// or
        /// InputFileName
        /// or
        /// OutputFileName
        /// </exception>
        public EncryptionService(EncryptionDTO encryptionDTO)
        {
            _encryptionDTO = encryptionDTO ?? throw new ArgumentNullException(nameof(encryptionDTO));
            if (string.IsNullOrEmpty(encryptionDTO.UserName)) throw new ArgumentNullException(nameof(encryptionDTO.UserName));
            if (string.IsNullOrEmpty(encryptionDTO.Password)) throw new ArgumentNullException(nameof(encryptionDTO.Password));
            if (string.IsNullOrEmpty(encryptionDTO.InputFileName)) throw new ArgumentNullException(nameof(encryptionDTO.InputFileName));
            if (string.IsNullOrEmpty(encryptionDTO.OutputFileName)) throw new ArgumentNullException(nameof(encryptionDTO.OutputFileName));
        }

        /// <summary>
        /// Apply decryption to this instance Encryption DTO.
        /// </summary>
        /// <returns></returns>
        public bool Decrypt() {

            byte[] key = new UnicodeEncoding().GetBytes(_encryptionDTO.Password);

            using (FileStream fileStreamInput = new FileStream(_encryptionDTO.InputFileName, FileMode.Open))
            using (CryptoStream cryptoStream = new CryptoStream(fileStreamInput, new RijndaelManaged().CreateDecryptor(key, key), CryptoStreamMode.Read))
            using (FileStream fileStreamOutput = new FileStream(_encryptionDTO.OutputFileName, FileMode.Create))
            {
                int data;
                while ((data = cryptoStream.ReadByte()) != -1)
                    fileStreamOutput.WriteByte((byte)data);
            }
            return true;
        }

        /// <summary>
        /// Apply encryption to the file in this instance Encryption DTO.
        /// </summary>
        /// <returns></returns>
        public bool Encrypt()
        {
            byte[] key = new UnicodeEncoding().GetBytes(_encryptionDTO.Password);

            using (FileStream fileStreamInput = new FileStream(_encryptionDTO.OutputFileName, FileMode.Create))
            using (CryptoStream cryptoStream = new CryptoStream(fileStreamInput, new RijndaelManaged().CreateEncryptor(key, key), CryptoStreamMode.Write))
            using (FileStream fileStreamOutput = new FileStream(_encryptionDTO.InputFileName, FileMode.Open))
            {
                int data;
                while ((data = fileStreamOutput.ReadByte()) != -1)
                    cryptoStream.WriteByte((byte)data);
            }
            return true;
        }
    }
}
