﻿using System;
using System.IO;
using System.IO.Pipes;
using System.Threading;
using System.Diagnostics;

namespace Master
{
    class Program
    {
        static void Main(string[] args)
        {
            // Priskiriam šita programa tik pirmam CPU branduoliui (branduolys 0)
            Process.GetCurrentProcess().ProcessorAffinity = (IntPtr)0x1;

            // Sukuriamos dvi gijos agentams
            Thread gija1 = new Thread(() => PriimtiDuomenis("agent1"));
            Thread gija2 = new Thread(() => PriimtiDuomenis("agent2"));

            gija1.Start();
            gija2.Start();

            gija1.Join();
            gija2.Join();

            Console.WriteLine("Visi duomenys iš abiejų scannerių priimti. Programa baigta.");
        }

        static void PriimtiDuomenis(string kanaloPavadinimas)
        {
            using (var pipe = new NamedPipeServerStream(kanaloPavadinimas, PipeDirection.In))
            using (var reader = new StreamReader(pipe))
            {
                Console.WriteLine($"Laukiama prisijungimo: {kanaloPavadinimas}...");
                pipe.WaitForConnection();
                Console.WriteLine($"{kanaloPavadinimas} prisijungė!");

                string eilute;
                while ((eilute = reader.ReadLine()) != null)
                {
                    if (eilute == "BAIGTA")
                    {
                        Console.WriteLine($"{kanaloPavadinimas} baigė siųsti duomenis.");
                        break;
                    }

                    Console.WriteLine($"[{kanaloPavadinimas}] Gauta: {eilute}");
                }
            }
        }
    }
}
