using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.Remoting.Contexts;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using WienerLinienApi;
using WienerLinienApi.Information;
using WienerLinienApi.Model;
using WienerLinienApi.RealtimeData;
using WienerLinienApi.RealtimeData.Monitor;
using WienerLinienApi.Samples.WPF_Proper.Model;

namespace ConsoleApplicationWienerLinienAPI
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

            Console.WriteLine("Please enter the desired station (e.g. Pilgramgasse):");
            var station = Console.ReadLine();
            Console.WriteLine();

            // the desired station
            var SelectedStation = allStations.Find(x => station != null && x.Name.Contains(station));

            var listLines = new List<Station.Platform>();

            // gets every line once
            for (int i = 0; i < SelectedStation.Platforms.Count; i++){
                if(i%2==0) listLines.Add(SelectedStation.Platforms.ElementAt(i));
            }

            // lists all possible lines
            listLines.ForEach(x => Console.WriteLine("- " + x.Name));

            Console.WriteLine("\nPlease enter the desired plattform (e.g. 14A):");
            var plattform = Console.ReadLine();;

            var StationLines = SelectedStation.Platforms.FindAll(x => x.Name.Equals(plattform));
            foreach(var x in StationLines)
            listRbls.Add(x.RblNumber);

            //Create a Parameters object to include the Rbls  and get Realtime Data for them
            var parameters = new Parameters.MonitorParameters() { Rbls = listRbls };

            //Get the monitor informatino asynchronous, and save them as MonitorData class
            var monitorInfo = await rtd.GetMonitorDataAsync(parameters);

            foreach (var m in monitorInfo.Data.Monitors)
            {
                foreach (var lineIterate in m.Lines)
                    if (lineIterate.Name.Equals(plattform))
                    {
                        Console.WriteLine();
                        Console.WriteLine(lineIterate.Name + " " + lineIterate.Towards);
                        lineIterate.Departures.Departure.ForEach(x => Console.WriteLine(" " + x.DepartureTime.TimePlanned.Normalize().Substring(11,5)));
                    }
            }



            //Get the planned arrival time for the first line and the next vehicle arriving (index at Departure)
            //var plannedTime = monitorInfo.Data.Monitors[0].Lines[0].Departures.Departure[0].DepartureTime.TimePlanned;


            Console.WriteLine("Finished");
            Console.ReadKey();
        }

        private static string ReplaceString(string towards)
        {
            var a = Regex.Replace(towards, "([A-Z]+ )", i => "(" + i.Value.Trim() + ") ");
            return a;
        }

    }
}
