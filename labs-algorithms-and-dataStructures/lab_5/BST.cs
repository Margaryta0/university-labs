using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lab_5
{
    class BSTNode
    {
        public Student Data { get; set; }
        public BSTNode Left { get; set; }
        public BSTNode Right { get; set; }
        public int Size { get; set; }  

        public BSTNode(Student d) { Data = d; Left = null; Right = null; Size = 1; }
    }

    class BST
    {
        protected BSTNode _root;
        protected Random _rng = new Random(42);

        protected BSTNode RotateLeft(BSTNode node)
        {
            BSTNode right = node.Right;
            node.Right       = right.Left;
            right.Left       = node;
            right.Size       = node.Size;
            node.Size        = 1 + GetSize(node.Left) + GetSize(node.Right);
            return right;
        }

        protected BSTNode RotateRight(BSTNode node)
        {
            BSTNode left = node.Left;
            node.Left        = left.Right;
            left.Right       = node;
            left.Size        = node.Size;
            node.Size        = 1 + GetSize(node.Left) + GetSize(node.Right);
            return left;
        }

        protected int GetSize(BSTNode n) => n == null ? 0 : n.Size;

        public virtual void Insert(Student s)
        {
            _root = InsertAtRoot(_root, s);
        }

        protected BSTNode InsertAtRoot(BSTNode node, Student s)
        {
            if (node == null) return new BSTNode(s);

            int cmp = string.Compare(s.LastName, node.Data.LastName,
                          StringComparison.OrdinalIgnoreCase);

            if (cmp < 0)
            {
                node.Left  = InsertAtRoot(node.Left, s);
                node       = RotateRight(node);   
            }
            else if (cmp > 0)
            {
                node.Right = InsertAtRoot(node.Right, s);
                node       = RotateLeft(node);      
            }
            return node;
        }

        public Student Search(string lastName)
        {
            BSTNode cur = _root;
            while (cur != null)
            {
                int cmp = string.Compare(lastName, cur.Data.LastName,
                              StringComparison.OrdinalIgnoreCase);
                if (cmp == 0) return cur.Data;
                else if (cmp <  0) cur = cur.Left;
                else cur = cur.Right;
            }
            return null;
        }

        public void PrintBFS()
        {
            if (_root == null) { Console.WriteLine("Дерево порожнє."); return; }

            var queue = new Queue<BSTNode>();
            queue.Enqueue(_root);
            int level = 0;

            while (queue.Count > 0)
            {
                int count = queue.Count;
                Console.Write($"  Рівень {level}: ");
                for (int i = 0; i < count; i++)
                {
                    var node = queue.Dequeue();
                    Console.Write($"[{node.Data.LastName}] ");
                    if (node.Left  != null) queue.Enqueue(node.Left);
                    if (node.Right != null) queue.Enqueue(node.Right);
                }
                Console.WriteLine();
                level++;
            }
        }

        public void Run()
        {
            Console.WriteLine("=== РІВЕНЬ 2: BST з вставкою в корінь (ключ: Прізвище) ===\n");

            Student[] students = {
                new Student("Іваненко",  "Олег",    1234567890u, "денна"),
                new Student("Петренко",  "Анна",    2876543210u, "заочна"),
                new Student("Бондар",    "Ірина",   3012345678u, "заочна"),
                new Student("Шевченко",  "Олена",   2109876543u, "заочна"),
                new Student("Коваль",    "Тетяна",  1987654321u, "заочна"),
                new Student("Мороз",     "Дмитро",  2345678901u, "денна"),
                new Student("Лисенко",   "Сергій",  1654321098u, "денна"),
            };

            foreach (var s in students)
            {
                Insert(s);
                Console.WriteLine($"Додано: {s.LastName}. Дерево (BFS):");
                PrintBFS();
                Console.WriteLine();
            }

            // Пошук
            string[] keys = { "Коваль", "Мороз", "Назаренко" };
            Console.WriteLine("--- Пошук ---");
            foreach (var key in keys)
            {
                var found = Search(key);
                if (found != null)
                    Console.WriteLine($"  Знайдено '{key}': {found}");
                else
                    Console.WriteLine($"  '{key}' — не знайдено.");
            }
        }
    }

    class RandomizedBST : BST
    {
        public override void Insert(Student s)
        {
            _root = InsertRandom(_root, s);
        }

        BSTNode InsertRandom(BSTNode node, Student s)
        {
            if (node == null) return new BSTNode(s);

            if (_rng.Next(0, GetSize(node) + 1) == 0)
                return InsertAtRoot(node, s);

            int cmp = string.Compare(s.LastName, node.Data.LastName,
                          StringComparison.OrdinalIgnoreCase);

            if (cmp < 0)
                node.Left  = InsertRandom(node.Left, s);
            else if (cmp > 0)
                node.Right = InsertRandom(node.Right, s);

            node.Size = 1 + GetSize(node.Left) + GetSize(node.Right);
            return node;
        }

        public void Run()
        {
            Console.WriteLine("\n=== РІВЕНЬ 3: Рандомізований BST (балансування рандомізацією) ===\n");

            Student[] students = {
                new Student("Іваненко",  "Олег",    1234567890u, "денна"),
                new Student("Петренко",  "Анна",    2876543210u, "заочна"),
                new Student("Бондар",    "Ірина",   3012345678u, "заочна"),
                new Student("Шевченко",  "Олена",   2109876543u, "заочна"),
                new Student("Коваль",    "Тетяна",  1987654321u, "заочна"),
                new Student("Мороз",     "Дмитро",  2345678901u, "денна"),
                new Student("Лисенко",   "Сергій",  1654321098u, "денна"),
            };

            foreach (var s in students)
            {
                Insert(s);
                Console.WriteLine($"Додано: {s.LastName}. Дерево (BFS):");
                PrintBFS();
                Console.WriteLine();
            }

            // Пошук
            string[] keys = { "Бондар", "Іваненко", "Гриценко" };
            Console.WriteLine("--- Пошук ---");
            foreach (var key in keys)
            {
                var found = Search(key);
                if (found != null)
                    Console.WriteLine($"  Знайдено '{key}': {found}");
                else
                    Console.WriteLine($"  '{key}' — не знайдено.");
            }
        }
    }
}
