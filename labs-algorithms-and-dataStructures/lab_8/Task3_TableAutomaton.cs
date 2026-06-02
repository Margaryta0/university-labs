using System;
using System.IO;
using System.Text.RegularExpressions;
using System.Collections.Generic;

class Task3_TableAutomaton
{
    private const int STATE_COUNT = 7;
    private const int S0 = 0, S1 = 1, S2 = 2, S3 = 3,
                      S4 = 4, S5 = 5, SE = 6;

    private const int CL_SIGN = 0; 
    private const int CL_04 = 1; 
    private const int CL_59 = 2; 
    private const int CL_AL = 3; 
    private const int CL_MINUS = 4; 
    private const int CL_OTHER = 5; 

    private static readonly int[,] Table = new int[STATE_COUNT, 6]
    {
        //          +/-   0-4   5-9  a/l    -    інше
        /* S0 */ {  S1,   SE,   SE,  SE,   S1,   SE  },
        /* S1 */ {  SE,   S2,   SE,  SE,   SE,   SE  },
        /* S2 */ {  SE,   S2,   S3,  S4,   S5,   SE  },
        /* S3 */ {  SE,   SE,   S3,  SE,   S5,   SE  },
        /* S4 */ {  SE,   SE,   SE,  S4,   S5,   SE  },
        /* S5 */ {  SE,   SE,   SE,  SE,   SE,   SE  },
        /* SE */ {  SE,   SE,   SE,  SE,   SE,   SE  },
    };

    private static bool IsAccepted(int state) => state == S5;

    private static int GetClass(char c, int currentState)
    {
        if (currentState == S0 && (c == '+' || c == '-')) return CL_SIGN;
        if (c >= '0' && c <= '4') return CL_04;
        if (c >= '5' && c <= '9') return CL_59;
        if (c == 'a' || c == 'l') return CL_AL;
        if (c == '-') return CL_MINUS; 
        return CL_OTHER;
    }


    public static bool Recognize(string word)
    {
        int state = S0;

        for (int i = 0; i < word.Length; i++)
        {
            int cls = GetClass(word[i], state); 
            state = Table[state, cls];           
            if (state == SE) return false;      
        }

        return IsAccepted(state);
    }

    public static void Run()
    {
        Console.WriteLine("\n╔══════════════════════════════════════════════════════╗");
        Console.WriteLine("║  ЗАВДАННЯ 3: Автомат на таблиці переходів (for)      ║");
        Console.WriteLine("╚══════════════════════════════════════════════════════╝");
        Console.WriteLine("Регулярний вираз: (\\+|-)[0-4]+([5-9]*|[al]*)-");
        Console.WriteLine("Роздільники у файлі: «!», «!!», «!!!»\n");

        Console.Write("Введіть ім'я файлу (за замовч. sentences.txt): ");
        string input = Console.ReadLine();
        string fileName = string.IsNullOrWhiteSpace(input) ? "sentences.txt" : input;

        if (!File.Exists(fileName))
        {
            Console.WriteLine($"Файл «{fileName}» не знайдено!");
            return;
        }

        string[] lines = File.ReadAllLines(fileName);

        Regex splitter = new Regex(@"!!!|!!|!");

        Console.WriteLine("Результати аналізу:");
        Console.WriteLine(new string('─', 50));

        int lineNum = 0;
        int totalWords = 0, validWords = 0;

        foreach (string line in lines)
        {
            lineNum++;
            if (string.IsNullOrWhiteSpace(line)) continue;

            Console.WriteLine($"\nРядок {lineNum}: {line}");

            string[] words = splitter.Split(line);

            foreach (string rawWord in words)
            {
                string word = rawWord.Trim();
                if (string.IsNullOrEmpty(word)) continue;

                totalWords++;
                bool valid = Recognize(word);
                if (valid) validWords++;

                Console.WriteLine(valid
                    ? $"  ✓  «{word}»"
                    : $"  ✗  «{word}»");
            }
        }

        Console.WriteLine(new string('─', 50));
        Console.WriteLine($"Всього слів: {totalWords},  правильних: {validWords},  неправильних: {totalWords - validWords}");

        PrintTransitionTable();
    }

    private static void PrintTransitionTable()
    {
        Console.WriteLine("\n── Таблиця переходів скінченного автомата ──");
        string[] stateNames = { "S0", "S1", "S2", "S3", "S4", "S5", "SE" };
        string[] classNames = { "+/-", "0-4", "5-9", "a/l", " - ", "інш" };

        Console.Write($"{"Стан",-5}│");
        foreach (string cn in classNames)
            Console.Write($" {cn,4} │");
        Console.WriteLine();
        Console.WriteLine(new string('─', 5 + 8 * 6));

        for (int r = 0; r < STATE_COUNT; r++)
        {
            Console.Write($"{stateNames[r],-5}│");
            for (int c = 0; c < 6; c++)
                Console.Write($" {stateNames[Table[r, c]],4} │");
            Console.WriteLine();
        }
    }
}
