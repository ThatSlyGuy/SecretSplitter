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

            DoEncryption();
            DoDecryption();

            Console.ReadKey();
        }

        private static void DoDecryption()
        {
            Console.WriteLine("Enter secret name/type:");

            string name = Console.ReadLine();

            Console.WriteLine("\nEnter encryption key: ");

            string key = Console.ReadLine();

            for (int i = 0; i < 4; i++)
            {
                using (StreamReader readtext = new StreamReader(name + "_secret_" + (i + 1) + ".txt"))
                {
                    string ciphertext = readtext.ReadLine();
                    string decryptedSecret = Decrypt(ciphertext, key);

                    Console.WriteLine("\nSecret #" + (i + 1) + ": " + decryptedSecret + "\n");
                }
            }
        }

        private static void DoEncryption()
        {
            Console.WriteLine("Enter secret name/type:");

            string name = Console.ReadLine();

            Console.WriteLine("\nEnter encryption key: ");

            string key = Console.ReadLine();

            for (int i = 0; i < 4; i++)
            {
                Console.WriteLine("\nEnter secret #" + (i + 1) + ":");

                string secret = Console.ReadLine();
                string encryptedSecret = Encrypt(secret, key);

                Console.WriteLine("\nSecret #" + (i + 1) + " encrypted: " + encryptedSecret + "\n");

                using (StreamWriter writer = new StreamWriter(name + "_secret_" + (i + 1) + ".txt"))
                {
                    writer.Write(encryptedSecret);
                }
            }
        }

        private static string Encrypt(string secret, string key)
        {
            using (SecureDataEncryptor dataEncryptor = new SecureDataEncryptor(new AdvancedSecureRandom(new Blake2bDigest(512), RandomString.Secure.Blake2.GetString(key, 64))))
            {
                return dataEncryptor.Encrypt(secret, RandomString.Secure.SHA3.GetString(key, 64));
            }
        }

        private static string Decrypt(string ciphertext, string key)
        {
            using (SecureDataEncryptor dataEncryptor = new SecureDataEncryptor(new AdvancedSecureRandom(new Blake2bDigest(512), RandomString.Secure.Blake2.GetString(key, 64))))
            {
                return dataEncryptor.Decrypt(ciphertext, RandomString.Secure.SHA3.GetString(key, 64));
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
