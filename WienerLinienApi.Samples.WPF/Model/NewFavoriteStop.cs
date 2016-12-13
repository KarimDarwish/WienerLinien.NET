using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using WienerLinienApi.Information;

namespace WienerLinienApi.Samples.WPF.Model
{
    class NewFavoriteStop
    {
        public string StopName { get; set; }
        public string Line { get; set; }
        public string StopType { get; set; }
        public int Platforms { get; set; }
        public int [] TimesToWait { get; set; }
        public List<string> BusStops { get; set; }

        public NewFavoriteStop(string type)
        {
            Task.Run(async () =>
            {
                BusStops = (await Start(type));
            }).Wait();

        }

        public async Task<List<string>> Start(string type)
        {
            var context = new WienerLinienContext("O56IE8eH7Kf5R5aQ");
            var allStations = await Stations.GetAllStationsAsync();

            var listOfStations = new List<String>();

            foreach (var v in allStations)
            {
                foreach (var v2 in v.Platforms)
                {
                    if (v2.MeansOfTransport.Contains("bus"))
                    {
                        listOfStations.Add(v.Name);
                        break;
                    }
                }
            }

            return listOfStations;
        }
    }
}
