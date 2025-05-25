using System;
using System.Collections.Generic;
using System.Linq;

class Program
{
    static int n, m;
    static int[] cats;
    static List<int>[] g;

    static int DFS(int v, int prev, int cnt)
    {
        if (cnt > m)
            return 0;

        if (g[v].Count == 1 && v != 1)
            return 1;

        int res = 0;
        foreach (int to in g[v])
        {
            if (to != prev)
            {
                int c = (cats[to] == 0) ? 0 : cnt + 1;
                res += DFS(to, v, c);
            }
        }

        return res;
    }

    static void Main()
    {
        var nm = Console.ReadLine().Split().Select(int.Parse).ToArray();
        n = nm[0];
        m = nm[1];

        cats = new int[n + 1];

        var inputCats = Console.ReadLine().Split().Select(int.Parse).ToArray();
        for (int i = 0; i < n; i++)
        {
            cats[i + 1] = inputCats[i];
        }

        g = new List<int>[n + 1];
        for (int i = 0; i <= n; i++)
        {
            g[i] = new List<int>();
        }

        for (int i = 0; i < n - 1; i++)
        {
            var edge = Console.ReadLine().Split().Select(int.Parse).ToArray();
            int a = edge[0], b = edge[1];
            g[a].Add(b);
            g[b].Add(a);
        }

        Console.WriteLine(DFS(1, -1, cats[1]));
    }
}
