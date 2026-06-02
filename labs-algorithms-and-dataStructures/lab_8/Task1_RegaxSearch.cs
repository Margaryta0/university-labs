using System;
using System.IO;
using System.Text.RegularExpressions;
using System.Collections.Generic;

class Task1_RegexSearch
{
    private const string Pattern = @"^0[0-9]*![0-9]*1$";

    public static void Run()
    {
        Console.WriteLine("\n╔══════════════════════════════════════════════╗");
        Console.WriteLine("║  ЗАВДАННЯ 1: Пошук слів регулярним виразом  ║");
        Console.WriteLine("╚══════════════════════════════════════════════╝");
        Console.WriteLine($"Регулярний вираз: {Pattern}");
        Console.WriteLine("Умова: починається на «0», закінчується на «1»,");
        Console.WriteLine("       дві частини з цифр, розділені символом «!»\n");

        Console.Write("Введіть ім'я файлу (за замовч. words.txt): ");
        string input = Console.ReadLine();
        string fileName = string.IsNullOrWhiteSpace(input) ? "words.txt" : input;

        if (!File.Exists(fileName))
        {
            Console.WriteLine($"Файл «{fileName}» не знайдено!");
            return;
        }

        Regex regex = new Regex(Pattern);

        List<string> matched = new List<string>(); 
        List<string> notMatched = new List<string>(); 
        int lineNumber = 0;

        foreach (string line in File.ReadLines(fileName))
        {
            lineNumber++;
            string word = line.Trim(); 
            if (string.IsNullOrEmpty(word)) continue;

            if (regex.IsMatch(word))
                matched.Add($"  рядок {lineNumber,2}: \"{word}\"  ✓");
            else
                notMatched.Add($"  рядок {lineNumber,2}: \"{word}\"  ✗");
        }

        Console.WriteLine($"Усього слів прочитано: {matched.Count + notMatched.Count}");

        Console.WriteLine($"\n--- Слова, що ВІДПОВІДАЮТЬ шаблону ({matched.Count} шт.) ---");
        if (matched.Count == 0)
            Console.WriteLine("  (немає)");
        else
            matched.ForEach(s => Console.WriteLine(s));

        Console.WriteLine($"\n--- Слова, що НЕ відповідають шаблону ({notMatched.Count} шт.) ---");
        if (notMatched.Count == 0)
            Console.WriteLine("  (немає)");
        else
            notMatched.ForEach(s => Console.WriteLine(s));
    }
}
