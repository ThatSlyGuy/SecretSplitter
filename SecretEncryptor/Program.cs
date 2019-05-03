using Hope.Random;
using Hope.Random.Strings;
using Hope.Security.SymmetricEncryption.CrossPlatform;
using Org.BouncyCastle.Crypto.Digests;
using System;
using System.IO;
using System.Linq;

namespace SecretEncryptor
{
    public class Program
    {
        private static void Main(string[] args)
        {
            //ReadDrives();

            EncryptData();
            DecryptData();

            Console.ReadKey();
        }

        private static void DecryptData()
        {
            Console.WriteLine("Enter encryption key: ");

            string key = Console.ReadLine();

            using (SecureDataEncryptor dataEncryptor = new SecureDataEncryptor(new AdvancedSecureRandom(new Blake2bDigest(512), RandomString.Secure.Blake2.GetString(key, 64))))
            {
                for (int i = 0; i < 4; i++)
                {
                    using (StreamReader readtext = new StreamReader("secret_1_" + (i + 1) + ".txt"))
                    {
                        string ciphertext = readtext.ReadLine();
                        string decryptedSecret = dataEncryptor.Decrypt(ciphertext, RandomString.Secure.SHA3.GetString(key, 64));

                        Console.WriteLine("\nSecret #" + (i + 1) + ": " + decryptedSecret + "\n");
                    }
                }
            }
        }

        private static void EncryptData()
        {
            Console.WriteLine("Enter encryption key: ");

            string key = Console.ReadLine();

            using (SecureDataEncryptor dataEncryptor = new SecureDataEncryptor(new AdvancedSecureRandom(new Blake2bDigest(512), RandomString.Secure.Blake2.GetString(key, 64))))
            {
                for (int i = 0; i < 4; i++)
                {
                    Console.WriteLine("\nEnter secret #" + (i + 1) + ":");

                    string secret = Console.ReadLine();
                    string encryptedSecret = dataEncryptor.Encrypt(secret, RandomString.Secure.SHA3.GetString(key, 64));

                    Console.WriteLine("\nSecret #" + (i + 1) + " encrypted: " + encryptedSecret + "\n");

                    using (StreamWriter writer = new StreamWriter("secret_1_" + (i + 1) + ".txt"))
                    {
                        writer.Write(encryptedSecret);
                    }
                }
            }
        }

        private static void ReadDrives()
        {
            var drives = DriveInfo.GetDrives()
                                  .Where(drive => drive.IsReady);

            foreach (DriveInfo d in drives)
            {
                Console.WriteLine(d.Name + " => " + d.DriveType + " " + d.IsReady);

                var directories = Directory.GetDirectories(d.RootDirectory.FullName);

                foreach (var directory in directories)
                {
                    Console.WriteLine(directory);
                }
            }
        }
    }
}
