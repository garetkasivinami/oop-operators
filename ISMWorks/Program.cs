using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Work;

namespace ISMWorks
{
    class Program
    {
        static void Main(string[] args)
        {
            Fraction a = new Fraction(5);
            Console.WriteLine($"a = {a}");
            Fraction b = new Fraction(-6, 7);
            Console.WriteLine($"b = {b.ToString()}");
            Console.WriteLine("Print fraction number (Example 5/7)");
            Fraction c = new Fraction(Console.ReadLine());
            Console.WriteLine($"a + c = {a + c}");
            Console.WriteLine($"a - c = {a - c}");
            Console.WriteLine($"a * c = {a * c}");
            Console.WriteLine($"a / c = {(a / c).ToString()}");
            Console.WriteLine($"-b = {(-b).ToString()}");
            Console.WriteLine($"!c = {(!c).ToString()}");
            Console.WriteLine($"b > c = {b > c}");
            Console.WriteLine($"a <= c = {a <= c}");
            Console.WriteLine($"9/7 == 90/70 = {new Fraction(9,7)== new Fraction(90,70)}");
            Console.WriteLine($"b(double) = {(double)b}");
            Console.ReadKey();
        }
    }
}
