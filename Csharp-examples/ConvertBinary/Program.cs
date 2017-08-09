using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConvertBinary
{
    class Program
    {
        static void Main(string[] args)
        {
            Class1 a1 = new Class1();
            a1.a = 99;
            a1.b = "test1";
            a1.c.Add("test2");
            a1.c.Add("test3");
            a1.d = 1.5;
            a1.e = true;
            for (int i = 0; i < a1.f.Length; i++)
            {
                a1.f[i] = 1;
            }

            Console.WriteLine(a1.a);
            Console.WriteLine(a1.b);
            foreach (var c in a1.c)
            {
                Console.WriteLine(c);
            }
            Console.WriteLine(a1.d);
            Console.WriteLine(a1.e);
            foreach (var f in a1.f)
            {
                Console.WriteLine(f);
            }

            byte[] stream = a1.ToStream();

            Class1 a2 = new Class1();
            a2.FromStream(stream);

            Console.WriteLine(a2.a);
            Console.WriteLine(a2.b);
            foreach (var c in a2.c)
            {
                Console.WriteLine(c);
            }
            Console.WriteLine(a2.d);
            Console.WriteLine(a2.e);
            foreach (var f in a2.f)
            {
                Console.WriteLine(f);
            }
        }
    }
}
