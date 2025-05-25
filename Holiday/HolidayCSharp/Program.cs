using System;

class Program
{
    static void Main()
    {
        int n = int.Parse(Console.ReadLine());
        int[] p = new int[n + 1]; 
        string[] input = Console.ReadLine().Split();

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
    }
}
