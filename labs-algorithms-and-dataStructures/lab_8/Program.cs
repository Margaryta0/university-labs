using System;

class Program
{
    static void Main()
    {
        Console.OutputEncoding = System.Text.Encoding.UTF8;

        Console.WriteLine("╔══════════════════════════════════════════════════════╗");
        Console.WriteLine("║    ЛАБОРАТОРНА РОБОТА 2.2 — Варіант 19              ║");
        Console.WriteLine("║    Дослідження алгоритмів ідентифікації             ║");
        Console.WriteLine("╚══════════════════════════════════════════════════════╝");

        bool running = true;
        while (running)
        {
            Console.WriteLine("\n┌──────────────────────────────────┐");
            Console.WriteLine("│            МЕНЮ                  │");
            Console.WriteLine("│  1 — Завдання 1 (regex пошук)    │");
            Console.WriteLine("│  2 — Завдання 2 (switch автомат) │");
            Console.WriteLine("│  3 — Завдання 3 (таблиця + for)  │");
            Console.WriteLine("│  0 — Вихід                       │");
            Console.WriteLine("└──────────────────────────────────┘");
            Console.Write("Ваш вибір: ");

            string choice = Console.ReadLine();
            switch (choice)
            {
                case "1": Task1_RegexSearch.Run(); break;
                case "2": Task2_SwitchAutomaton.Run(); break;
                case "3": Task3_TableAutomaton.Run(); break;
                case "0": running = false; break;
                default: Console.WriteLine("Невідома команда. Спробуйте 0-3."); break;
            }
        }

        Console.WriteLine("\nДо побачення!");
    }
}
