using System;
using System.Diagnostics;
using System.Threading;

namespace RibbonPerformanceComparison
{
    internal class Program
    {
        static int attemptCount = 10;
        static string[] versions = { "21_1", "20_2" };
        public static void Main(string[] args)
        {
            Console.WriteLine("Products (1 - RichEdit, 2 - Spreadsheet, 3 - both, 0 - exit):");
            int num = -1;
            do
            {
                num = Console.ReadKey().Key - ConsoleKey.D0;
            } while (num < 0 || num > 3);
            Console.WriteLine();
            switch(num)
            {
                case 0:
                    return;
                case 1:
                    StartTestForProduct("RichEdit");
                    break;
                case 2:
                    StartTestForProduct("Spreadsheet");
                    break;
                case 3:
                    StartTestForProduct("RichEdit");
                    StartTestForProduct("Spreadsheet");
                    break;
            }
            Console.WriteLine("Press any key to exit");
            Console.ReadKey();
        }
        static void StartTestForProduct(string product)
        {
            foreach (var version in versions)
            {
                string fileName = string.Format("{0}{1}.exe", product, version);
                for (int i = 0; i < attemptCount; ++i)
                {
                    var processWithoutDocument = Process.Start(fileName, $"{i + 1} {false}");
                    if (processWithoutDocument == null)
                    {
                        Console.WriteLine("Process creating error");
                        return;
                    }
                    while (!processWithoutDocument.HasExited)
                        Thread.Sleep(1000);
                    var processWithDocument = Process.Start(fileName, $"{i + 1} {true}");
                    if (processWithDocument == null)
                    {
                        Console.WriteLine("Process creating error");
                        return;
                    }
                    while (!processWithDocument.HasExited)
                        Thread.Sleep(1000);
                }
                Console.WriteLine($"Task {fileName} completed.");
            }
        }
    }
}