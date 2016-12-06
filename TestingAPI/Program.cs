using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using WienerLinienApi;
using WienerLinienApi.Information;
using WienerLinienApi.Model;

namespace TestingAPI
{
    class Program
    {
        static void Main(string[] args)
        {
            Task.Run(async () =>
            {
                await test();
            }).Wait();

        }

        public static async Task test()
        {


            var allStations = await Stations.GetAllStationsAsync();
            foreach (var var1 in allStations)
            {
                Console.WriteLine(var1.Name);
            }

            Console.WriteLine("Finished");
            Console.ReadKey();
        }
    }
}
