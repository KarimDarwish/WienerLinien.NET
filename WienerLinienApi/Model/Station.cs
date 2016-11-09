using System.Collections.Generic;

namespace WienerLinienApi.Model
{
    public class Station
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
        public List<Platform> Platforms { get; set; }
        public struct Platform
        {
            public string Line { get; set; }
            public bool Realtime { get; set; }
            public string MeansOfTransport { get; set; }
            public int RblNumber { get; set; }
            public string Area { get; set; }
            public string Direction { get; set; }
            public string Order { get; set; }
            public string PlatformName { get; set; }
            public double PlatformWgs84Lat { get; set; }
            public double PlatformWgs84Lon { get; set; }
            public int StationId { get; set; }
        }
    }
}
