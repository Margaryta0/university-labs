using System;
using System.Collections.Generic;

class Task2_Permutations
{
    private static int[,] matrix = {
        { 6, 1, 2, 4, 5 },
        { 2, 3, 4, 5, 6 }, 
        { 7, 8, 9, 0, 1 }, 
        { 3, 4, 5, 6, 7 }, 
        { 3, 5, 5, 3, 3 }  
    };

    private static List<int[]> allPermutations = new List<int[]>();

    private static long Factorial(int n)
    {
        long result = 1;             
        for (int i = 2; i <= n; i++) 
            result *= i;
        return result;
    }

    private static long CountPermutations(int[] elements)
    {
        int n = elements.Length;   
        long numerator = Factorial(n);

        Dictionary<int, int> counts = new Dictionary<int, int>();
        foreach (int el in elements)
        {
            if (counts.ContainsKey(el)) counts[el]++;
            else counts[el] = 1;
        }

        long denominator = 1;
        foreach (var pair in counts)
        {
            denominator *= Factorial(pair.Value); 
            if (pair.Value > 1) 
                Console.WriteLine($"  Елемент {pair.Key} повторюється {pair.Value} рази → {pair.Value}! = {Factorial(pair.Value)}");
        }

        return numerator / denominator; 
    }

    private static void GeneratePermutations(int[] arr, int[] current, bool[] used, int depth)
    {
        if (depth == arr.Length)
        {
            allPermutations.Add((int[])current.Clone()); 
            return;
        }

        for (int i = 0; i < arr.Length; i++)
        {
            if (used[i]) continue;
            if (i > 0 && arr[i] == arr[i - 1] && !used[i - 1]) continue;

            used[i] = true;         
            current[depth] = arr[i];

            GeneratePermutations(arr, current, used, depth + 1);

            used[i] = false; 
        }
    }

    private static void PrintMatrix(int[,] m, string title)
    {
        int rows = m.GetLength(0); 
        int cols = m.GetLength(1); 
        if (title != "") Console.WriteLine(title);
        for (int i = 0; i < rows; i++)
        {
            Console.Write("  [ ");
            for (int j = 0; j < cols; j++)
            {
                if (i == j) Console.Write($"[{m[i, j]}]"); 
                else Console.Write($" {m[i, j]} "); 
                Console.Write(" ");
            }
            Console.WriteLine("]");
        }
    }

    public static void Run()
    {
        Console.WriteLine("\n╔══════════════════════════════════════════════════════╗");
        Console.WriteLine("║  ЗАВДАННЯ 2: Перестановки елементів діагоналі       ║");
        Console.WriteLine("╚══════════════════════════════════════════════════════╝");
        Console.WriteLine("Тип вибірки: перестановки з повтореннями P(n; k1,k2,...)");

        PrintMatrix(matrix, "\nВихідна матриця (діагональ у [ ]):");

        int size = matrix.GetLength(0);
        int[] diagonal = new int[size]; 
        Console.Write("\nГоловна діагональ: {");
        for (int i = 0; i < size; i++)
        {
            diagonal[i] = matrix[i, i];
            Console.Write(i < size - 1 ? $"{diagonal[i]}, " : $"{diagonal[i]}");
        }
        Console.WriteLine("}");

        Console.WriteLine("\nАналіз повторень:");
        long count = CountPermutations(diagonal);

        Console.WriteLine($"\nФормула: P(5; 2,2,1) = 5! / (2! × 2! × 1!)");
        Console.WriteLine($"= {Factorial(size)} / ({Factorial(2)} × {Factorial(2)} × {Factorial(1)})");
        Console.WriteLine($"= {Factorial(size)} / 4");
        Console.WriteLine($"= {count}");
        Console.WriteLine($"\nВідповідь: можна отримати {count} різних матриць.");

        allPermutations.Clear(); 

        int[] sortedDiag = (int[])diagonal.Clone(); 
        Array.Sort(sortedDiag);

        bool[] used = new bool[size]; 
        int[] current = new int[size]; 

        GeneratePermutations(sortedDiag, current, used, 0);

        Console.WriteLine($"\nПеревірка: згенеровано {allPermutations.Count} унікальних перестановок. (має бути {count})");

        int showCount = Math.Min(5, allPermutations.Count);
        Console.WriteLine($"\nПерші {showCount} варіантів матриць:");
        for (int p = 0; p < showCount; p++)
        {
            int[,] newMatrix = (int[,])matrix.Clone();
            for (int i = 0; i < size; i++)
                newMatrix[i, i] = allPermutations[p][i]; 

            Console.Write($"\n  Варіант {p+1}: діагональ = {{");
            for (int i = 0; i < size; i++)
                Console.Write(i < size-1 ? $"{allPermutations[p][i]}, " : $"{allPermutations[p][i]}");
            Console.WriteLine("}");
            PrintMatrix(newMatrix, "");
        }
        if (allPermutations.Count > showCount)
            Console.WriteLine($"  ... та ще {allPermutations.Count - showCount} варіантів");
    }
}
