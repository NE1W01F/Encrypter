using Encrypter.Core;
using Encrypter.Core.Service;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.IO;
using System.Linq;

namespace Enctypter.Test
{
    [TestClass]
    public class ServiceTests
    {
        private static EncryptionDTO _dto;

        [ClassInitialize]
        public static void ClassInitialize(TestContext context)
        {
            RemoveGeneratedTestFiles();

            _dto = new EncryptionDTO() {
                UserName = "FooBar",
                Password = "12345678",
                InputFileName = @"..\..\Files\TestFile.txt",
                OutputFileName = @"..\..\Files\TestFile.txt.cry"
            };
        }

        [ClassCleanup]
        public static void ClassCleanup()
        {
            RemoveGeneratedTestFiles();
        }

        /// <summary>
        /// Removes the generated test files.
        /// </summary>
        private static void RemoveGeneratedTestFiles()
        {
            foreach (var file in Directory.GetFiles(@"..\..\Files\"))
            {
                if (!new bool[]{
                    Path.GetFileName(file).Equals("TestFile.txt", StringComparison.OrdinalIgnoreCase),
                    Path.GetFileName(file).Equals("TestFile1.txt.cry", StringComparison.OrdinalIgnoreCase)
                }.Any(x => x.Equals(true))
                )
                {
                    File.Delete(file);
                }
            }
        }

        [TestMethod]
        public void EncryptionService_Encrypt()
        {
            bool result = new EncryptionService(_dto).Encrypt();
            Assert.IsTrue(File.Exists(_dto.OutputFileName));
            File.Delete(_dto.OutputFileName);
        }

        [TestMethod]
        public void EncryptionService_Decrypt()
        {
            bool encryptResult = new EncryptionService(_dto).Encrypt();

            Assert.IsTrue(File.Exists(_dto.OutputFileName));
            Assert.IsTrue(encryptResult);

            var inputString = File.ReadAllText(_dto.InputFileName);
            var encryptString = File.ReadAllText(_dto.OutputFileName);

            _dto.InputFileName = @"..\..\Files\TestFile.txt.cry";
            _dto.OutputFileName = @"..\..\Files\TestFile.txt";

            bool decryptResult = new EncryptionService(_dto).Decrypt();

            Assert.IsTrue(File.Exists(_dto.OutputFileName));
            Assert.IsTrue(decryptResult);

            var decryptString = File.ReadAllText(_dto.OutputFileName);

            Assert.AreEqual(
                expected: inputString,
                actual: decryptString);
        }

        [DataTestMethod]
        [DataRow("", "", "", "")]
        [DataRow("Foo", "", "", "")]
        [DataRow("Foo", "Bar", "FooBar", "")]
        [DataRow("", "Bar", "FooBar", "BarFoo")]
        [DataRow("", "", "FooBar", "BarFoo")]
        [DataRow("", "", "", "BarFoo")]
        [DataRow(null, null, null, null)]
        [DataRow("Foo", null, null, null)]
        [DataRow("Foo", "Bar", "FooBar", null)]
        [DataRow(null, "Bar", "FooBar", "BarFoo")]
        [DataRow(null, null, "FooBar", "BarFoo")]
        [DataRow(null, null, null, "BarFoo")]
        [ExpectedException(typeof(ArgumentNullException))]
        public void EncryptionService_constructor_guards_return_exception(string userName, string password, string inputFile, string outputFile)
        {
            var service = new EncryptionService(new EncryptionDTO() {
                UserName = userName,
                Password = password,
                InputFileName = inputFile,
                OutputFileName = outputFile
            });
        }

    }
}
