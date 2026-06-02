using System;

enum State
{
    S0,  // початковий стан
    S1,  // після знаку + або -
    S2,  // після обов'язкових цифр 0-4
    S3,  // після необов'язкових цифр 5-9
    S4,  // після необов'язкових символів a/l
    S5,  // прийнятий (після фінального «-»)
    SE   // помилковий стан
}

class Task2_SwitchAutomaton
{
    private static State Transition(State current, char c)
    {
        switch (current)
        {
            case State.S0:
                if (c == '+' || c == '-') return State.S1;
                return State.SE;

            case State.S1:
                if (c >= '0' && c <= '4') return State.S2;
                return State.SE;

            case State.S2:
                if (c >= '0' && c <= '4') return State.S2; 
                if (c >= '5' && c <= '9') return State.S3; 
                if (c == 'a' || c == 'l') return State.S4; 
                if (c == '-') return State.S5; 
                return State.SE;

            case State.S3:
                if (c >= '5' && c <= '9') return State.S3; 
                if (c == '-') return State.S5; 
                return State.SE;

            case State.S4:
                if (c == 'a' || c == 'l') return State.S4; 
                if (c == '-') return State.S5; 
                return State.SE;

            case State.S5:
                return State.SE;

            case State.SE:
                return State.SE;

            default:
                return State.SE;
        }
    }

    public static bool Recognize(string input)
    {
        State state = State.S0; 

        foreach (char c in input)
        {
            state = Transition(state, c);
            if (state == State.SE) return false; 
        }

        return state == State.S5;
    }

    public static void Run()
    {
        Console.WriteLine("\n╔══════════════════════════════════════════════════╗");
        Console.WriteLine("║  ЗАВДАННЯ 2: Скінченний автомат (switch)         ║");
        Console.WriteLine("╚══════════════════════════════════════════════════╝");
        Console.WriteLine("Регулярний вираз: (\\+|-)[0-4]+([5-9]*|[al]*)-");
        Console.WriteLine("Приклади правильних: +1234-  -04-  +0al-  -1aaa-");
        Console.WriteLine("Приклади неправильних: 1234-  +- +5-  +0a5-\n");

        while (true)
        {
            Console.Write("Введіть рядок для перевірки (або 'exit' для виходу): ");
            string input = Console.ReadLine();

            if (input == null || input.ToLower() == "exit") break;

            bool valid = Recognize(input);
            Console.WriteLine(valid
                ? $"  ✓ Рядок «{input}» — ПРАВИЛЬНИЙ"
                : $"  ✗ Рядок «{input}» — НЕПРАВИЛЬНИЙ");

            Console.Write("  Шлях станів: ");
            State s = State.S0;
            Console.Write(s);
            foreach (char c in input)
            {
                s = Transition(s, c);
                Console.Write($" -[{c}]→ {s}");
            }
            Console.WriteLine();
        }
    }
}
