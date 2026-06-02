using System;
using System.Collections.Generic;

class Task1_Arrangements
{
    public static List<int[]> AllArrangements = new List<int[]>();

    public static long Factorial(int n)
    {
        long result = 1;            
        for (int i = 2; i <= n; i++) 
            result *= i;
        return result;
    }

    public static long CountArrangements(int n, int r)
    {
        if (r > n) return 0;         
        long result = 1;
        for (int i = n; i > n - r; i--) 
            result *= i;
        return result;
    }

    public static void GenerateArrangements(int n, int r, List<int> current, bool[] used)
    {
        if (current.Count == r)
        {
            AllArrangements.Add(current.ToArray()); 
            return; 
        }

        for (int i = 1; i <= n; i++)
        {
            if (used[i]) continue; 

            used[i] = true;       
            current.Add(i);     

            GenerateArrangements(n, r, current, used);

            current.RemoveAt(current.Count - 1); 
            used[i] = false;      
        }
    }

    public static void Run()
    {
        Console.WriteLine("\n╔══════════════════════════════════════════════════╗");
        Console.WriteLine("║  ЗАВДАННЯ 1: Розміщення без повторень           ║");
        Console.WriteLine("╚══════════════════════════════════════════════════╝");
        Console.WriteLine("Задача: n викладачів, обрати голову, заступника,");
        Console.WriteLine("        секретаря та 2 членів комісії (r=5 посад).");
        Console.WriteLine("Тип вибірки: розміщення без повторень A(n, r)\n");

        Console.Write("Введіть кількість викладачів n (за замовч. 11): ");
        string inp = Console.ReadLine(); 
        int n = string.IsNullOrWhiteSpace(inp) ? 11 : int.Parse(inp);

        Console.Write("Введіть кількість посад r (за замовч. 5): ");
        inp = Console.ReadLine();
        int r = string.IsNullOrWhiteSpace(inp) ? 5 : int.Parse(inp);

        if (r > n)
        {
            Console.WriteLine("Помилка: r не може бути більше за n!");
            return;
        }

        long count = CountArrangements(n, r);

        Console.WriteLine($"\nФормула: A({n}, {r}) = {n}! / ({n}-{r})! = {n}! / {n-r}!");
        Console.Write($"Обчислення: ");
        for (int i = n; i > n - r; i--)
            Console.Write(i < n ? $" × {i}" : $"{i}");
        Console.WriteLine($" = {count}");
        Console.WriteLine($"\nВідповідь: існує {count} можливих варіантів складу комісії.");

        AllArrangements.Clear(); 
        bool[] used = new bool[n + 1]; 
        GenerateArrangements(n, r, new List<int>(), used);

        int showCount = Math.Min(10, AllArrangements.Count);
        Console.WriteLine($"\nПерші {showCount} варіантів (з {AllArrangements.Count}):");
        string[] positions = { "Голова", "Заст.", "Секр.", "Член1", "Член2" };
        for (int i = 0; i < showCount; i++)
        {
            Console.Write($"  {i+1,3}. [");
            for (int j = 0; j < AllArrangements[i].Length; j++)
            {
                string pos = j < positions.Length ? positions[j] : $"П{j+1}";
                Console.Write($"{pos}:В{AllArrangements[i][j]}"); 
                if (j < AllArrangements[i].Length - 1) Console.Write(", ");
            }
            Console.WriteLine("]");
        }
        if (AllArrangements.Count > showCount)
            Console.WriteLine($"  ... та ще {AllArrangements.Count - showCount} варіантів");
    }
}
