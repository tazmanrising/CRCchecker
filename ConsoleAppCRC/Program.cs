using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleAppCRC
{
    class Program
    {
        static void Main(string[] args)
        {

            var before = @"C:\Testing\Before\test.xlsx";
            //var after = @"C:\Testing\After\testbad.xlsx";
            var after = @"C:\Testing\After\test.xlsx";
            FileInfo file1 = new FileInfo(before);
            FileInfo file2 = new FileInfo(after);
                       

            Console.WriteLine("Start...");
            var result = FilesAreEqual(file1, file2);
            Console.WriteLine(result);
            Console.WriteLine("CRC Algorithm Check Complete");
            Console.ReadLine();

            
        }

        const int BYTES_TO_READ = sizeof(Int64);

        static bool FilesAreEqual(FileInfo first, FileInfo second)
        {
            if (first.Length != second.Length)
                return false;

            int iterations = (int)Math.Ceiling((double)first.Length / BYTES_TO_READ);

            using (FileStream fs1 = first.OpenRead())
            using (FileStream fs2 = second.OpenRead())
            {
                byte[] one = new byte[BYTES_TO_READ];
                byte[] two = new byte[BYTES_TO_READ];

                for (int i = 0; i < iterations; i++)
                {
                    fs1.Read(one, 0, BYTES_TO_READ);
                    fs2.Read(two, 0, BYTES_TO_READ);

                    if (BitConverter.ToInt64(one, 0) != BitConverter.ToInt64(two, 0))
                        return false;
                }
            }

            return true;
        }
    }
}
