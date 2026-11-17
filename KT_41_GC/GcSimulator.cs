using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace KT_41_GC
{
    public class GcSimulator
    {
        private static void ShowStats(string title, int g0Before, int g1Before, int g2Before, long ms)
        {
            int g0After = GC.CollectionCount(0);
            int g1After = GC.CollectionCount(1);
            int g2After = GC.CollectionCount(2);

            Console.WriteLine($"\n--- Результаты: {title} ---");
            Console.WriteLine($"Время выполнения: {ms} мс");
            Console.WriteLine($"Gen0 сборок: +{g0After - g0Before} (всего: {g0After})");
            Console.WriteLine($"Gen1 сборок: +{g1After - g1Before} (всего: {g1After})");
            Console.WriteLine($"Gen2 сборок: +{g2After - g2Before} (всего: {g2After})");
        }

        public static void ShortLived()
        {
            int g0 = GC.CollectionCount(0);
            int g1 = GC.CollectionCount(1);
            int g2 = GC.CollectionCount(2);

            Stopwatch sw = Stopwatch.StartNew();
            for (int i = 0; i < 300_000; i++)
            {
                byte[] temp = new byte[200];
            }
            sw.Stop();

            ShowStats("ShortLived (короткоживущие)", g0, g1, g2, sw.ElapsedMilliseconds);
        }

        public static void LongLived(List<byte[]> storage)
        {
            int g0 = GC.CollectionCount(0);
            int g1 = GC.CollectionCount(1);
            int g2 = GC.CollectionCount(2);

            Stopwatch sw = Stopwatch.StartNew();
            for (int i = 0; i < 5_000; i++)
            {
                storage.Add(new byte[512]);
            }
            sw.Stop();

            ShowStats("LongLived (долгоживущие)", g0, g1, g2, sw.ElapsedMilliseconds);
        }

        public static void HeavyLoh(List<byte[]> storage)
        {
            int g0 = GC.CollectionCount(0);
            int g1 = GC.CollectionCount(1);
            int g2 = GC.CollectionCount(2);

            Stopwatch sw = Stopwatch.StartNew();
            for (int i = 0; i < 20; i++)
            {
                storage.Add(new byte[100_000]);
            }
            sw.Stop();

            ShowStats("HeavyLoh (крупные объекты LOH)", g0, g1, g2, sw.ElapsedMilliseconds);
        }

        public static void ForcedBlockingGc()
        {
            int g0 = GC.CollectionCount(0);
            int g1 = GC.CollectionCount(1);
            int g2 = GC.CollectionCount(2);

            Stopwatch sw = Stopwatch.StartNew();
            GC.Collect(2, GCCollectionMode.Forced, blocking: true);
            sw.Stop();

            ShowStats("Forced Blocking GC.Collect(2)", g0, g1, g2, sw.ElapsedMilliseconds);
        }
    }
}