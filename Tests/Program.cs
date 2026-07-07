using System;
using System.Reflection;
using Vosiz.Utils;

namespace Tests
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Randomizer.Init();

            TestRunner.RunAll(Assembly.GetExecutingAssembly());

            Console.ReadLine();
        }
    }
}
