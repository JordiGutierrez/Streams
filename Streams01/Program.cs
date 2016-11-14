using System;
using System.IO;
using System.Threading.Tasks;
using static System.Console;

namespace Streams01
{
    class Program
    {
        static void Main(string[] args)
        {
            // Create a file called test.txt in the current directory:
            using (Stream s = new FileStream("test.txt", FileMode.Create))
            {
                WriteLine(s.CanRead);
                WriteLine(s.CanWrite);
                WriteLine(s.CanSeek);

                s.WriteByte(101);
                s.WriteByte(102);
                byte[] block = { 1, 2, 3, 4, 5 };
                s.Write(block, 0, block.Length);

                WriteLine(s.Length);
                WriteLine(s.Position);
                s.Position = 0;

                WriteLine(s.ReadByte());
                WriteLine(s.ReadByte());

                // Read from the stream back into the block array:
                WriteLine(s.Read(block, 0, block.Length));

                // Assuming the last Read returned 5, we'll be at
                // the end of the file, so Read will now return 0:
                WriteLine(s.Read(block, 0, block.Length));
            }

            Task t = AsyncDemo();
            t.Wait();
        }

        async static Task AsyncDemo()
        {
            using(Stream s = new FileStream("test.txt", FileMode.Create))
            {
                byte[] block = { 1, 2, 3, 4, 5 };
                await s.WriteAsync(block, 0, block.Length);

                s.Position = 0;

                // Read from the stream back into the block array:
                WriteLine(await s.ReadAsync(block, 0, block.Length));
            }
        }
    }
}
