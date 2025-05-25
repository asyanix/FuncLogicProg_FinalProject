using System;
using System.Collections.Generic;
using System.Diagnostics;

class Program
{
    const int N = 1000043; 
    static List<int>[] g = new List<int>[N];  // Список смежности (граф) (g[i] хранит список вершин, смежных с вершиной i)
    static int[] compontent = new int[N];     // Номера компонентов связности вершин (изначально для всех i component[i]=0)
    static int[] siz = new int[N];            // Размеы компоненты связности с номером i
    static int n, m;                          // Количество пользователей, количество групп
    static int cc = 0;                        // Счётчик компонентов связности

    static int DFS(int x)
    {
        if (compontent[x] != 0)
            return 0;

        compontent[x] = cc;
        int ans = (x < n ? 1 : 0);            // ans — количество людей в компоненте

        foreach (var y in g[x])
        {
            ans += DFS(y);                    // Смотрим всех соседей y текущей вершины x
        }

        return ans;
    }

    static void Main()
    {
        string[] firstLine = Console.ReadLine().Split();
        n = int.Parse(firstLine[0]);
        m = int.Parse(firstLine[1]);

        // Инициализируем списки смежности
        for (int i = 0; i < n + m; i++)
        {
            g[i] = new List<int>();
        }

        for (int i = 0; i < m; i++)
        {
            string[] line = Console.ReadLine().Split();
            int k = int.Parse(line[0]);

            // Создаем связи
            for (int j = 0; j < k; j++)
            {
                int x = int.Parse(line[j + 1]) - 1;
                g[x].Add(i + n);              // x <-> группа
                g[i + n].Add(x);              // группа <-> x
            }
        }

        Stopwatch stopwatch = Stopwatch.StartNew();
        long memoryBefore = GC.GetTotalMemory(true);

        for (int i = 0; i < n; i++)
        {
            if (compontent[i] == 0)           // Если вершина не обработана, запускаем DFS
            {
                cc++;
                siz[cc] = DFS(i);
            }

            Console.Write(siz[compontent[i]] + " ");
        }
        
        long memoryAfter = GC.GetTotalMemory(true);
        stopwatch.Stop();

        double elapsedSeconds = stopwatch.Elapsed.TotalSeconds;
        double usedMB = (memoryAfter - memoryBefore) / (1024.0 * 1024.0);

        Console.WriteLine();
        Console.WriteLine($"Время выполнения: {elapsedSeconds:F3} сек");
        Console.WriteLine($"Использовано памяти: {usedMB:F2} МБ");
    }
}
