using System;

class Program
{
    static void Main()
    {
        Console.OutputEncoding = System.Text.Encoding.UTF8;

        Console.WriteLine("╔══════════════════════════════════════════════════════╗");
        Console.WriteLine("║    ЛАБОРАТОРНА РОБОТА 2.3 — Варіант 19              ║");
        Console.WriteLine("║    Дослідження комбінаторних алгоритмів             ║");
        Console.WriteLine("╚══════════════════════════════════════════════════════╝");

        bool running = true;
        while (running)
        {
            Console.WriteLine("\n┌──────────────────────────────────────────────┐");
            Console.WriteLine("│                    МЕНЮ                      │");
            Console.WriteLine("│  1 — Завд.1: Розміщення без повторень        │");
            Console.WriteLine("│  2 — Завд.2: Перестановки діагоналі матриці  │");
            Console.WriteLine("│  3 — Завд.3: Записати розміщення у файл      │");
            Console.WriteLine("│  0 — Вихід                                   │");
            Console.WriteLine("└──────────────────────────────────────────────┘");
            Console.Write("Ваш вибір: ");

            string choice = Console.ReadLine(); 
            switch (choice)
            {
                case "1":
                    Task1_Arrangements.Run();  
                    break;
                case "2":
                    Task2_Permutations.Run();  
                    break;
                case "3":
                    Task3_WriteToFile.Run();  
                    break;
                case "0":
                    running = false;          
                    break;
                default:
                    Console.WriteLine("Невідома команда. Введіть число від 0 до 3.");
                    break;
            }
        }

        Console.WriteLine("\nДо побачення!"); // прощальне повідомлення
    }
}
