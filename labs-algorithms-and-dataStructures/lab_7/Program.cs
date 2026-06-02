using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lab_7
{
    class Program
    {
        static void Main()
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;

            Console.WriteLine("\n╔══════════════════════════════════╗");
            Console.WriteLine("║  ЗАВДАННЯ 1: Числове інтегрування ║");
            Console.WriteLine("╚══════════════════════════════════╝");
            Console.WriteLine("Функція: f(x) = (1 + sqrt(x)) / x^2");

            Console.Write("Введіть a (початок інтервалу, за замовч. 1): ");
            string inA = Console.ReadLine();
            double a1 = string.IsNullOrWhiteSpace(inA) ? 1.0 : double.Parse(inA);

            Console.Write("Введіть b (кінець інтервалу, за замовч. 4): ");
            string inB = Console.ReadLine();
            double b1 = string.IsNullOrWhiteSpace(inB) ? 4.0 : double.Parse(inB);

            Console.Write("Введіть крок h (за замовч. 0.5): ");
            string inH = Console.ReadLine();
            double h1 = string.IsNullOrWhiteSpace(inH) ? 0.5 : double.Parse(inH);

            double rect = NumericalIntegration.RectangleMethod(a1, b1, h1);
            double trap = NumericalIntegration.TrapezoidMethod(a1, b1, h1);
            double simpson = NumericalIntegration.SimpsonMethod(a1, b1, h1);

            Console.WriteLine($"\nМетод прямокутників : {rect:F8}");
            Console.WriteLine($"Метод трапецій      : {trap:F8}");
            Console.WriteLine($"Метод Сімпсона      : {simpson:F8}");

            double exact = (-1.0 / b1 - 2.0 / Math.Sqrt(b1))
                         - (-1.0 / a1 - 2.0 / Math.Sqrt(a1));
            Console.WriteLine($"Точне значення       : {exact:F8}");

            Console.WriteLine("\n╔══════════════════════════════════════════╗");
            Console.WriteLine("║  ЗАВДАННЯ 2: Корені алгебричного рівняння ║");
            Console.WriteLine("╚══════════════════════════════════════════╝");
            Console.WriteLine("Рівняння: x^5 + 18*x^3 - 34 = 0");

            Console.Write("Введіть a (початок інтервалу, за замовч. 0): ");
            string inA2 = Console.ReadLine();
            double a2 = string.IsNullOrWhiteSpace(inA2) ? 0.0 : double.Parse(inA2);

            Console.Write("Введіть b (кінець інтервалу, за замовч. 2): ");
            string inB2 = Console.ReadLine();
            double b2 = string.IsNullOrWhiteSpace(inB2) ? 2.0 : double.Parse(inB2);

            Console.WriteLine($"\ng(a) = {RootFinding.G(a2):F4},  g(b) = {RootFinding.G(b2):F4}");

            RootFinding.BisectionMethod(a2, b2);
            RootFinding.NewtonMethod(a2, b2);
            RootFinding.ChordMethod(a2, b2);

            Console.WriteLine("\n╔══════════════════════════════════════╗");
            Console.WriteLine("║  ЗАВДАННЯ 3: Диференціальне рівняння ║");
            Console.WriteLine("╚══════════════════════════════════════╝");
            Console.WriteLine("dy/dx = -2y / (y^2 - 6x)");

            Console.Write("Введіть x0 (початкове x, за замовч. 0): ");
            string inX0 = Console.ReadLine();
            double x0 = string.IsNullOrWhiteSpace(inX0) ? 0.0 : double.Parse(inX0);

            Console.Write("Введіть y0 (початкове y, за замовч. 1): ");
            string inY0 = Console.ReadLine();
            double y0 = string.IsNullOrWhiteSpace(inY0) ? 1.0 : double.Parse(inY0);

            Console.Write("Введіть xEnd (кінцеве x, за замовч. 2): ");
            string inXEnd = Console.ReadLine();
            double xEnd = string.IsNullOrWhiteSpace(inXEnd) ? 2.0 : double.Parse(inXEnd);

            Console.Write("Введіть крок h (за замовч. 0.1): ");
            string inH3 = Console.ReadLine();
            double h3 = string.IsNullOrWhiteSpace(inH3) ? 0.1 : double.Parse(inH3);

            DifferentialEquation.RungeKutta4(x0, y0, xEnd, h3);

            Console.WriteLine("\n========================================");
            Console.WriteLine("  Роботу виконано. Натисніть Enter...");
            Console.ReadLine();
        }
    }
}
