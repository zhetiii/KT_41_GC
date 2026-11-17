using System;
using System.Collections.Generic;

namespace KT_41_GC
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("=== КТ 41: Симуляция нагрузки на GC ===\n");

            List<byte[]> persistentStorage = new List<byte[]>();

            Console.Write("Нажмите Enter, чтобы запустить ShortLived()...");
            Console.ReadLine();
            GcSimulator.ShortLived();

            Console.Write("\nНажмите Enter, чтобы запустить LongLived()...");
            Console.ReadLine();
            GcSimulator.LongLived(persistentStorage);

            Console.Write("\nНажмите Enter, чтобы запустить HeavyLoh()...");
            Console.ReadLine();
            GcSimulator.HeavyLoh(persistentStorage);

            Console.Write("\nНажмите Enter, чтобы измерить паузу блокирующего GC.Collect()...");
            Console.ReadLine();
            GcSimulator.ForcedBlockingGc();

            Console.WriteLine("\nВсе эксперименты успешно завершены. Нажмите Enter для выхода.");
            Console.ReadLine();
        }
    }
}