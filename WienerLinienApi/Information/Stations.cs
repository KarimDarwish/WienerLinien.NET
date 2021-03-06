﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;
using WienerLinienApi.Model;

namespace WienerLinienApi.Information
{
    public class Stations
    {
        /// <summary>
        /// Downloads all currently available stations and parses them into a Station file
        /// where you can access the information.
        /// </summary>
        /// <returns>List of Station objects</returns>
        public static async Task<List<Station>> GetAllStationsAsync()
        {
            var json = await new JsonGenerator.JsonGenerator().GetJsonAsync();
           var stationList = JsonConvert.DeserializeObject<List<Station>>(json);
            return stationList;
        }

    }
}
