using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using WienerLinienApi.Information;
using WienerLinienApi.Model;

namespace WienerLinienApi.Samples.WPF.Model
{
    static class NewFavoriteStop
    {
        public static string StopName { get; set; }
        public static string Line { get; set; }
        public static string StopType { get; set; }
        public static int Platforms { get; set; }
        public static int[] TimesToWait { get; set; }
        public static List<string> BusStops { get; set; }
        public static WienerLinienContext context = new WienerLinienContext("O56IE8eH7Kf5R5aQ");
        private static List<Station> stations;

        public static async Task<List<string>> GetStaionNames(string type)
        {
            stations = await Stations.GetAllStationsAsync();

            var listOfStations = (from v in stations
                from p in v.Platforms
                where p.MeansOfTransport == "4"
                select v.Name).Distinct().ToList();
            return listOfStations;
        }

        public static async Task<List<string>> GetLinesFromStation(string station, string type)
        {
            if (stations == null)
            {
                stations = await Stations.GetAllStationsAsync();
            }
            var lines = (from v in stations
                where v.Name == station
                from p in v.Platforms
                where p.MeansOfTransport == "4"
                group p by p.Name
                into linesList
                select linesList.Key).ToList();
            return lines;

        }

        public static async Task<List<string>> GetDirections(string station, string line, string type)
        {
            if (stations == null)
            {
                stations = await Stations.GetAllStationsAsync();
            }
          
            var directions = (from v in stations
                where v.Name == station
                from p in v.Platforms
                where p.Name == line
                select p.RblNumber.ToString()).ToList();

           var rbls = directions.Select(int.Parse).ToList();

            var rtd = new RealtimeData.RealtimeData(context);

            var parameters = new Parameters.MonitorParameters() { Rbls = rbls };

            var monitorInfo = await rtd.GetMonitorDataAsync(parameters);
            var strings = new List<string>();
            var b =
                monitorInfo.Data.Monitors.Where(i => i.Lines[0].Direction == "R")
                    .Select(i => i.Lines[0].Towards)
                    .ToList()
                    .First();
            var c = monitorInfo.Data.Monitors.Where(i => i.Lines[0].Direction == "H")
                    .Select(i => i.Lines[0].Towards)
                    .ToList()
                    .First();
            strings.Add(b);
            strings.Add(c);
            return strings;


        }
    }
}
