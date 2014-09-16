using System;
using Nancy.Hosting.Self;

namespace WebApp
{
    public class Program
    {
        static void Main(string[] args)
        {
            var host = new NancyHost(new Uri("http://localhost:1337"));
            host.Start();
            
            Console.WriteLine("Press ANY key to exit");
            Console.ReadLine();
        }
    }
}
