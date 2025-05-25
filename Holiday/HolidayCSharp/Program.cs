using System;
using System.Diagnostics;

class Program
{
    static void Main()
    {
        int n = int.Parse(Console.ReadLine());
        int[] p = new int[n + 1];
        string[] input = Console.ReadLine().Split();

        Stopwatch stopwatch = Stopwatch.StartNew();
        long memoryBefore = GC.GetTotalMemory(true);

        for (int i = 1; i <= n; i++)
            p[i] = int.Parse(input[i - 1]);

        int ans = 0;

        for (int i = 1; i <= n; i++)
        {
            int t = p[i];
            int c = 1;
            while (t != -1)
            {
                c++;
                t = p[t];
            }
            ans = Math.Max(ans, c);
        }

        Console.WriteLine(ans);
        
        long memoryAfter = GC.GetTotalMemory(true);
        stopwatch.Stop();

        double elapsedSeconds = stopwatch.Elapsed.TotalSeconds;
        double usedMB = (memoryAfter - memoryBefore) / (1024.0 * 1024.0);

        Console.WriteLine();
        Console.WriteLine($"Время выполнения: {elapsedSeconds:F3} сек");
        Console.WriteLine($"Использовано памяти: {usedMB:F2} МБ");
    }
}
