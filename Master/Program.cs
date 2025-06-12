using System;
using System.IO;
using System.IO.Pipes;

namespace Master
{
    class Program
    {
        static void Main(string[] args)
        {
            using (var pipe = new NamedPipeServerStream("agent1", PipeDirection.In))
            using (var reader = new StreamReader(pipe))
            {
                Console.WriteLine("Laukiama prisijungimo...");
                pipe.WaitForConnection();
                Console.WriteLine("Scanneris prisijungė!");

                string eilute;
                while ((eilute = reader.ReadLine()) != null)
                {
                    if (eilute == "BAIGTA") break;
                    Console.WriteLine($"Gauta: {eilute}");
                }

                Console.WriteLine("Duomenys priimti. Programa baigta.");
            }
        }
    }
}
