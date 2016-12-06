using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestingAPI
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Start");
            var wlContext = new WienerLinienContext("yourApiKey");
        }
    }
}
