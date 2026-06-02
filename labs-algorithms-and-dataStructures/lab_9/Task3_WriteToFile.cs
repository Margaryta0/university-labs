using System;
using System.IO;
using System.Collections.Generic;

class Task3_WriteToFile
{
    public static void Run()
    {
        Console.WriteLine("\n╔══════════════════════════════════════════════════════╗");
        Console.WriteLine("║  ЗАВДАННЯ 3: Запис розміщень у файл                 ║");
        Console.WriteLine("╚══════════════════════════════════════════════════════╝");

        if (Task1_Arrangements.AllArrangements.Count == 0)
        {
            Console.WriteLine("Спочатку потрібно виконати Завдання 1.");
            Console.WriteLine("Запускаю Завдання 1 автоматично...\n");
            Task1_Arrangements.Run();
        }

        List<int[]> arrangements = Task1_Arrangements.AllArrangements;

        Console.Write("\nВведіть ім'я файлу (за замовч. arrangements.txt): ");
        string inp = Console.ReadLine(); 
        string fileName = string.IsNullOrWhiteSpace(inp) ? "arrangements.txt" : inp;

        string[] positions = { "Голова", "Заст.", "Секр.", "Член1", "Член2" };

        using (StreamWriter writer = new StreamWriter(fileName, false, System.Text.Encoding.UTF8))
        {
            writer.WriteLine("============================================================");
            writer.WriteLine("  ЛАБОРАТОРНА РОБОТА 2.3 — Варіант 19");
            writer.WriteLine("  Завдання 1: Розміщення без повторень A(n, r)");
            writer.WriteLine("============================================================");

            int r = arrangements[0].Length;
            writer.WriteLine($"Задача: обрати склад комісії ({r} посад) з n викладачів");
            writer.WriteLine($"Тип вибірки: розміщення без повторень");
            writer.WriteLine($"Кількість варіантів: {arrangements.Count}");
            writer.WriteLine("------------------------------------------------------------");

            writer.Write($"{"№",-6}| "); 
            for (int j = 0; j < r; j++)
            {
                string pos = j < positions.Length ? positions[j] : $"Посада{j+1}";
                writer.Write($"{pos,-8}"); 
            }
            writer.WriteLine();
            writer.WriteLine(new string('-', 6 + r * 8 + 2)); 

            for (int i = 0; i < arrangements.Count; i++)
            {
                writer.Write($"{i+1,-6}| "); 
                for (int j = 0; j < arrangements[i].Length; j++)
                {
                    writer.Write($"В{arrangements[i][j],-7}");
                }
                writer.WriteLine(); 
            }

            writer.WriteLine(new string('=', 6 + r * 8 + 2));
            writer.WriteLine($"Всього: {arrangements.Count} варіантів");
        }

        Console.WriteLine($"✓ Файл «{fileName}» успішно створено.");
        Console.WriteLine($"  Записано {arrangements.Count} рядків.");

        Console.WriteLine("\nПерші рядки файлу:");
        Console.WriteLine(new string('─', 50));
        string[] preview = File.ReadAllLines(fileName); 
        int previewCount = Math.Min(15, preview.Length);
        for (int i = 0; i < previewCount; i++)
            Console.WriteLine(preview[i]); 
        if (preview.Length > previewCount)
            Console.WriteLine($"... (ще {preview.Length - previewCount} рядків у файлі)");
        Console.WriteLine(new string('─', 50));
    }
}
