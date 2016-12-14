﻿using System;
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
            
        }

        public async Task<List<string>> GetStaionNames(string type)
        {
            var context = new WienerLinienContext("O56IE8eH7Kf5R5aQ");
            var allStations = await Stations.GetAllStationsAsync();

            var listOfStations = (from v in allStations
                                  from p in  v.Platforms
                                  where p.MeansOfTransport.Contains("type")
                                  select v.Name).ToList();
            return listOfStations;
        }
    }
}
