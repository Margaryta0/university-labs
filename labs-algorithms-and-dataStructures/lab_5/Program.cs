using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lab_5
{
    class Program
    {
        static void Main()
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;

            Level1.Run();

            Console.WriteLine();
            var bst = new BST();
            bst.Run();

            var rbst = new RandomizedBST();
            rbst.Run();

            Console.WriteLine("\nГотово! Натисніть Enter...");
            Console.ReadLine();
        }
    }
}
