using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using WienerLinienApi.Information;
using WienerLinienApi.Model;

namespace WienerLinienApi.Samples.WPF.Model
{
    class NewFavoriteStop
    {
        public string StopName { get; set; }
        public string Line { get; set; }
        public string StopType { get; set; }
        public int Platforms { get; set; }
        public int[] TimesToWait { get; set; }
        public List<string> BusStops { get; set; }
        private List<Station> stations;
        public NewFavoriteStop(string type)
        {

        }

        public async Task<List<string>> GetStaionNames(string type)
        {
            var context = new WienerLinienContext("O56IE8eH7Kf5R5aQ");
            stations = await Stations.GetAllStationsAsync();

            var listOfStations = (from v in stations
                                  from p in v.Platforms
                                  where p.MeansOfTransport == "4"

                                  select v.Name).Distinct().ToList();
            return listOfStations;
        }

        public async Task<List<string>> GetLinesFromStation(string station, string type)
        {
            if (stations == null)
            {
                stations = await Stations.GetAllStationsAsync();
            }
            var lines = (from v in stations
                         where v.Name == station
                         from p in v.Platforms
                         where p.MeansOfTransport == "4"
                         select p.Name).ToList();
            return lines;
            
        }
    }
}
