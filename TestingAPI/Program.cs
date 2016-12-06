using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using WienerLinienApi;
using WienerLinienApi.Information;

namespace TestingAPI
{
    class Program
    {
        static void Main(string[] args)
        {
            test();
        }

        public static async void test()
        {
            Console.WriteLine("Start");
            var wlContext = new WienerLinienContext("O56IE8eH7Kf5R5aQ");
            Console.WriteLine(wlContext.ApiKey);
            Thread.Sleep(1000);
            var allStations = await Stations.GetAllStationsAsync();
            Thread.Sleep(1000);
            foreach (var var1 in allStations)
            {
                Console.WriteLine(var1.Name);
            }

            Console.WriteLine("Finished");
            Console.ReadKey();
        }
    }
}
