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

            var listRbls = new List<int>();

            foreach (var v in allStations)
            {
                if (v.Name.Equals("Pilgramgasse"))
                {
                    foreach (var v2 in v.Platforms)
                    {
                        if (v2.Name.Equals("12A"))
                        {
                            listRbls.Add(v2.RblNumber);
                        }

                    }
                    break;
                }
            }

            //Create a Parameters object to include the Rbls  and get Realtime Data for them
            var parameters = new Parameters.MonitorParameters() { Rbls = listRbls };

            //Get the monitor informatino asynchronous, and save them as MonitorData class
            var monitorInfo = await rtd.GetMonitorDataAsync(parameters);

            foreach (var v in monitorInfo.Data.Monitors)
            {
                foreach (var v2 in v.Lines)
                {
                    Console.WriteLine();
                    Console.WriteLine(v2.Name);
                    foreach (var v3 in v2.Departures.Departure)
                    {
                        Console.WriteLine(v3.DepartureTime.Countdown);
                    }
                    Console.WriteLine();
                }
            }
            
            //Get the planned arrival time for the first line and the next vehicle arriving (index at Departure)
            //var plannedTime = monitorInfo.Data.Monitors[0].Lines[0].Departures.Departure[0].DepartureTime.TimePlanned;

           
            Console.WriteLine("Finished");
            Console.ReadKey();
        }
    }
}
