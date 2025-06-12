using System;
using System.Collections.Generic;
using System.IO;

namespace ScannerA
{
    class Program
    {
        static void Main(string[] args)
        {
            // Automatinis kelias iki aplanko „TestData“
            string folderPath = Path.Combine(Directory.GetCurrentDirectory(), "TestData");

            if (!Directory.Exists(folderPath))
            {
                Console.WriteLine("Aplankas TestData nerastas.");
                return;
            }

            Console.WriteLine($"Skaitomi failai iš: {folderPath}");

            foreach (var file in Directory.GetFiles(folderPath, "*.txt"))
            {
                Console.WriteLine($"\n--- {Path.GetFileName(file)} ---");
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
                    Console.WriteLine($"{Path.GetFileName(file)}:{pora.Key}:{pora.Value}");
                }
            }

            Console.WriteLine("\nSkaitymas baigtas.");
        }
    }
}
