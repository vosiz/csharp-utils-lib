using System;
using System.Reflection;

namespace Tests
{
    internal class Program
    {
        static void Main(string[] args)
        {
            TestRunner.RunAll(Assembly.GetExecutingAssembly());

            Console.ReadLine();
        }
    }
}
