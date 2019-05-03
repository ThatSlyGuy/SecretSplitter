using System;
using System.IO;
using System.Linq;

namespace SecretEncryptor
{
    public class Program
    {
        private static void Main(string[] args)
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

            Console.ReadKey();
        }
    }
}
