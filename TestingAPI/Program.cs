using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using WienerLinienApi;
using WienerLinienApi.Information;
using WienerLinienApi.Model;
using WienerLinienApi.RealtimeData;

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
            Console.WriteLine("Start");

            var context = new WienerLinienContext("O56IE8eH7Kf5R5aQ");
            var allStations = await Stations.GetAllStationsAsync();

            //initialize the RealtimeData object using the created context
            var rtd = new RealtimeData(context);

            foreach (var v in allStations)
            {
                if (v.Name.Equals("Pilgramgasse"))
                {
                    Console.WriteLine("Name: "+v.Name);
                    Console.WriteLine("Typ: " + v.Typ);
                    Console.WriteLine("Stand: " + v.Stand);
                    foreach (var v2 in v.Platforms)
                    {
                        Console.WriteLine();
                        Console.WriteLine("Linie: " + v2.Line);
                        Console.WriteLine("MeansOfT: " + v2.MeansOfTransport);
                        Console.WriteLine("Dir: " + v2.Direction);
                        Console.WriteLine("Area: " + v2.Area);
                    }
                }
            }
            Console.ReadKey();

            //Create a List<int> of all RBL's we want to receive realtime data for
            var listRbls = new List<int>() { allStations[0].Platforms[0].RblNumber, allStations[0].Platforms[1].RblNumber };

            //Create a Parameters object to include the Rbls  and get Realtime Data for them
            var parameters = new Parameters.MonitorParameters() { Rbls = listRbls };

            //Get the monitor informatino asynchronous, and save them as MonitorData class
            var monitorInfo = await rtd.GetMonitorDataAsync(parameters);

            //Get the planned arrival time for the first line and the next vehicle arriving (index at Departure)
            var plannedTime = monitorInfo.Data.Monitors[0].Lines[0].Departures.Departure[0].DepartureTime.TimePlanned;

            Console.WriteLine(plannedTime);

            Console.WriteLine("Finished");
            Console.ReadKey();
        }
    }
}
