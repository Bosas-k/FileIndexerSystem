using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.IO;
using System.IO.Pipes;
using System.Threading;

namespace ScannerB
{
    class Program
    {
        static ConcurrentQueue<string> eilesDuomenys = new ConcurrentQueue<string>();

        static void Main(string[] args)
        {
            // Aplankas TestData šalia projekto
            string folderPath = Path.Combine(Directory.GetCurrentDirectory(), "TestData");

            if (!Directory.Exists(folderPath))
            {
                Console.WriteLine("Aplankas TestData nerastas.");
                return;
            }

            Thread skaitytojas = new Thread(() => NuskaitykIrRikiuok(folderPath));
            Thread siuntejas = new Thread(SiustiIMaster);

            skaitytojas.Start();
            siuntejas.Start();

            skaitytojas.Join();
            siuntejas.Join();

            Console.WriteLine("Duomenys išsiųsti. Programa baigta.");
        }

        static void NuskaitykIrRikiuok(string folderPath)
        {
            foreach (var file in Directory.GetFiles(folderPath, "*.txt"))
            {
                var tekstas = File.ReadAllText(file);
                var zodziai = tekstas.Split(new[] { ' ', '\r', '\n', ',', '.', ';', ':', '-', '!', '?' }, StringSplitOptions.RemoveEmptyEntries);
                var skaitiklis = new Dictionary<string, int>();

                foreach (var zodis in zodziai)
                {
                    if (!skaitiklis.ContainsKey(zodis))
                        skaitiklis[zodis] = 0;
                    skaitiklis[zodis]++;
                }

                foreach (var pora in skaitiklis)
                {
                    eilesDuomenys.Enqueue($"{Path.GetFileName(file)}:{pora.Key}:{pora.Value}");
                }
            }

            eilesDuomenys.Enqueue("BAIGTA"); // žymeklis, kad viskas išsiųsta
        }

        static void SiustiIMaster()
        {
            using (var pipe = new NamedPipeClientStream(".", "agent2", PipeDirection.Out)) // ← PATAISYTA
            using (var writer = new StreamWriter(pipe))
            {
                Console.WriteLine("Jungiamasi prie Master...");
                pipe.Connect();
                Console.WriteLine("Prisijungta!");

                writer.AutoFlush = true;

                while (true)
                {
                    if (eilesDuomenys.TryDequeue(out string eilute))
                    {
                        writer.WriteLine(eilute);
                        if (eilute == "BAIGTA") break;
                    }
                    else
                    {
                        Thread.Sleep(100);
                    }
                }
            }
        }
    }
}
