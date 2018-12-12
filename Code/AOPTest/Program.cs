using System;
using System.Linq;

namespace AOPTest
{
    class Program
    {
        static void Main(string[] args)
        {
            string[] a = new string[] { "2","3"};
            Console.WriteLine("Hello World!");
        }

        public void PrintText(string Text)
        {
            Console.WriteLine(Text);
        }


    }
}
