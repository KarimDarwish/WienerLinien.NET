using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;
using WienerLinienApi.Information;

namespace WienerLinienApi.JsonGenerator
{
    /// <summary>
    /// Generates a combined JSON file of the 3 csv files returned by the WienerLinien API
    /// </summary>
    internal class JsonGenerator
    {
        #region "Constants"

        private const string StationsLink = "http://data.wien.gv.at/csv/wienerlinien-ogd-haltestellen.csv";

        /// <summary>
        /// Static link for the lines-csv file
        /// </summary>
        private const string LinesLink = "http://data.wien.gv.at/csv/wienerlinien-ogd-linien.csv";

        /// <summary>
        /// Static link for the platforms-csv file
        /// </summary>
        private const string PlatformsLink = "http://data.wien.gv.at/csv/wienerlinien-ogd-steige.csv";

        private List<LinienModel> Lines { get; set; }
        private List<SteigModel> Platforms { get; set; }
        private List<HaltestellenModel> Haltetellen { get; set; }

        private HttpClient client;
        private Entities1 db
            = new Entities1();
       

        #endregion

        #region "Methods"

        public JsonGenerator()
        {
            client = new HttpClient();
            
        }


        public async Task<string> GetJsonAsync()
        {
            await DownloadFilesAsync();
            // DownloadFiles();
            var listPlatformsModel =
                from platform in Platforms
                join line in Lines on platform.FkLineId equals line.LineId
                select new WienerLinienModel.PlatformsModel
                {
                    Line = line.LineId,
                    Name = line.Description,
                    Realtime = Convert.ToBoolean(Convert.ToInt16(line.Realtime)),
                    MeansOfTransport = line.MeansOfTransport,
                    RblNumber = platform.RblNumber.TryParse(0),
                    Area = platform.Area,
                    Direction = platform.Direction,
                    Order = platform.Order,
                    PlatformName = platform.PlatformId,
                    PlatformWgs84Lat = platform.PlatformWgs84Lat.TryParse(0.0),
                    PlatformWgs84Lon = platform.PlatformWgs84Lon.TryParse(0.0),
                    StationId = platform.FkStationId.TryParse(0)
                };




            var listModel =
            from platforms in listPlatformsModel.ToList()
            join haltestelle in Haltetellen on platforms.StationId equals int.Parse(haltestelle.StationId)
            group platforms by haltestelle
            into grp
            select new WienerLinienModel
            {
                StationId = grp.Key.StationId.TryParse(0),
                // StationId = IntTryParse(grp.Key.StationId),

                Typ = grp.Key.Typ,
                Diva = grp.Key.Diva,
                Name = grp.Key.Name,
                Municipality = grp.Key.Municipality,
                MunicipalityId = grp.Key.MunicipalityId.TryParse(0),
                Wgs84Lat = grp.Key.Wgs84Lat.TryParse(0.0),
                Wgs84Lon = grp.Key.Wgs84Lon.TryParse(0.0),
                Stand = grp.Key.Stand,
                Platforms = grp.ToList()
            };
            var list = listModel.ToList();
            var json = JsonConvert.SerializeObject(list);
            return json;


        }

        /// <summary>
        /// Downloads a string asynchronous
        /// </summary>
        /// <param name="url">The url you want to download from</param>
        /// <returns>the downloaded string</returns>
        private async Task<string> DownloadAsync(string url)
        {
            return await client.GetStringAsync(url);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        private async Task DownloadFilesAsync()
        {
            await Task.Factory.StartNew(DownloadFiles);
        }
        /// <summary>
        /// Downloads the 3 static CSV files and parses them to their corresponding models
        /// </summary>
        private void DownloadFiles()
        {
            var stationsA = DownloadAsync(StationsLink).Result;
            var linesA = DownloadAsync(LinesLink).Result;
            var platformsA = DownloadAsync(PlatformsLink).Result;

            var listStations = stationsA.Remove(stationsA.LastIndexOf((Environment.NewLine), StringComparison.Ordinal))
                .Split(new[] { Environment.NewLine }, StringSplitOptions.None)
                .Skip(1)
                .Select(columns => columns.Split(';'))
                .Select(columns => new HaltestellenModel
                {
                    StationId = columns[0].Replace("\"", ""),
                    Typ = columns[1].Replace("\"", ""),
                    Diva = columns[2].Replace("\"", ""),
                    Name = (columns[3]).Replace("\"", ""),
                    Municipality = columns[4].Replace("\"", ""),
                    MunicipalityId = columns[5].Replace("\"", ""),
                    Wgs84Lat = columns[6].Replace("\"", ""),
                    Wgs84Lon = columns[7].Replace("\"", ""),
                }
                )
                ;
            Haltetellen = listStations.ToList();
            var listLines = linesA.Remove(linesA.LastIndexOf((Environment.NewLine), StringComparison.Ordinal))
                .Split(new[] { Environment.NewLine }, StringSplitOptions.None)

                .Skip(1)
                .Select(columns => columns.Split(';'))
                .Select(columns => new LinienModel
                {
                    LineId = columns[0].Replace("\"", ""),
                    Description = columns[1].Replace("\"", ""),
                    Order = columns[2].Replace("\"", ""),
                    Realtime = columns[3].Replace("\"", ""),
                    MeansOfTransport = MeansOfTransportWrapper.GetMeansOfTransportFromString(columns[4].Replace("\"", "")),
                    Stand = columns[5].Replace("\"", "")
                })
                ;
            Lines = listLines.ToList();
            var listPlatforms = platformsA.Remove(platformsA.LastIndexOf((Environment.NewLine), StringComparison.Ordinal))

                .Split(new[] { Environment.NewLine }, StringSplitOptions.None)

                .Skip(1)
                .Select(columns => columns.Split(';'))
                .Select(columns => new SteigModel
                {
                    PlatformId = columns[0].Replace("\"", ""),
                    FkLineId = columns[1].Replace("\"", ""),
                    FkStationId = columns[2].Replace("\"", ""),
                    Direction = columns[3].Replace("\"", ""),
                    Order = columns[4].Replace("\"", ""),
                    RblNumber = columns[5].Replace("\"", ""),
                    Area = columns[6].Replace("\"", ""),
                    Platform = columns[7].Replace("\"", ""),
                    PlatformWgs84Lat = columns[8].Replace("\"", ""),
                    PlatformWgs84Lon = columns[9].Replace("\"", ""),
                    Stand = columns[10].Replace("\"", "")
                })
                ;
            Platforms = listPlatforms.ToList();
            //  System.Diagnostics.Debug.WriteLine(Platforms[0].RblNumber);

        }

        #endregion

        #region Structs and Models


        public struct HaltestellenModel
        {
            public string StationId { get; set; }
            public string Typ { get; set; }
            public string Diva { get; set; }
            public string Name { get; set; }
            public string Municipality { get; set; }
            public string MunicipalityId { get; set; }
            public string Wgs84Lat { get; set; }
            public string Wgs84Lon { get; set; }
            public string Stand { get; set; }
        }

        public struct SteigModel
        {
            public string PlatformId { get; set; }
            public string FkLineId { get; set; }
            public string FkStationId { get; set; }
            public string Direction { get; set; }
            public string Order { get; set; }
            public string RblNumber { get; set; }
            public string Area { get; set; }
            public string Platform { get; set; }
            public string PlatformWgs84Lat { get; set; }
            public string PlatformWgs84Lon { get; set; }
            public string Stand { get; set; }
        }

        public struct LinienModel
        {
            public string LineId { get; set; }
            public string Description { get; set; }
            public string Order { get; set; }
            public string Realtime { get; set; }
            public MeansOfTransport MeansOfTransport { get; set; }
            public string Stand { get; set; }

        }

        public struct WienerLinienModel
        {
            public int StationId { get; set; }
            public string Typ { get; set; }
            public string Diva { get; set; }
            public string Name { get; set; }
            public string Municipality { get; set; }
            public int MunicipalityId { get; set; }
            public double Wgs84Lat { get; set; }
            public double Wgs84Lon { get; set; }
            public string Stand { get; set; }
            public List<PlatformsModel> Platforms { get; set; }
            public struct PlatformsModel
            {
                public string Line { get; set; }
                public bool Realtime { get; set; }
                public MeansOfTransport MeansOfTransport { get; set; }
                public int RblNumber { get; set; }
                public string Area { get; set; }
                public string Direction { get; set; }
                public string Order { get; set; }
                public string PlatformName { get; set; }
                public double PlatformWgs84Lat { get; set; }
                public double PlatformWgs84Lon { get; set; }
                public int StationId { get; set; }
                public string Name { get; internal set; }
            }
        }



        #endregion

    }
    public static class StringExtensions
    {
        /// <summary>
        /// Tries to parse a string into an integer
        /// </summary>
        /// <param name="input">The string you want to convert</param>
        /// <param name="valueIfNotConverted">The value the int should have if the input is not valid</param>
        /// <returns></returns>
        public static int TryParse(this string input, int valueIfNotConverted)
        {
            int value;
            return int.TryParse(input, out value) ? value : valueIfNotConverted;
        }
        /// <summary>
        /// Tries to parse a string into a double
        /// </summary>
        /// <param name="input">The string you want to convert</param>
        /// <param name="valueIfNotConverted">The value the double should have if the input is not valid</param>
        /// <returns></returns>
        public static double TryParse(this string input, double valueIfNotConverted)
        {
            double value;
            return double.TryParse(input, out value) ? value : valueIfNotConverted;
        }
    }
}