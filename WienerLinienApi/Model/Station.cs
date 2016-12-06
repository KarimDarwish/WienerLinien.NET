using System.Collections.Generic;

namespace WienerLinienApi.Model
{
    public class Station
    {
        /// <summary>
        /// Id of station
        /// e.g 214461177
        /// </summary>
        public int StationId { get; set; }
        /// <summary>
        /// Type of station
        /// e.g stop
        /// </summary>
        public string Typ { get; set; }
        /// <summary>
        /// DIVA of station (Station id of electronical traffic information)
        /// 
        /// </summary>
        public string Diva { get; set; }
        /// <summary>
        /// Name of station
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// Municipality  the station is located in
        /// </summary>
        public string Municipality { get; set; }
        /// <summary>
        /// Id of Municipality the station is located in
        /// </summary>
        public int MunicipalityId { get; set; }
        /// <summary>
        /// Latitude in WGS84 format of the station
        /// </summary>
        public double Wgs84Lat { get; set; }
        /// <summary>
        /// Longitude in WGS84 format of the station
        /// </summary>
        public double Wgs84Lon { get; set; }
        /// <summary>
        /// Stand of the station
        /// null unless error
        /// </summary>
        public string Stand { get; set; }
        /// <summary>
        /// List of all platforms the station has
        /// </summary>
        public List<Platform> Platforms { get; set; }
        public struct Platform
        {
            /// <summary>
            /// ID of the line
            /// </summary>
            public int LineId { get; set; }
            /// <summary>
            /// name of the line
            /// </summary>
            public string Name { get; set; }
            /// <summary>
            /// true when the line supports realtime
            /// </summary>
            public bool Realtime { get; set; }
            /// <summary>
            /// returns the meansoftransport of the line
            /// </summary>
            public string MeansOfTransport { get; set; }
            /// <summary>
            /// The RBL number of the platform
            /// </summary>
            public int RblNumber { get; set; }
            /// <summary>
            /// The area of the platform
            /// </summary>
            public string Area { get; set; }
            /// <summary>
            /// The direction of the platform (H,R)
            /// </summary>
            public string Direction { get; set; }
            /// <summary>
            /// Name of line + direction
            /// </summary>
            public string Order { get; set; }
            /// <summary>
            /// Name of the platform
            /// </summary>
            public string PlatformName { get; set; }
            /// <summary>
            /// Latitude in WGS84 format of the platform
            /// </summary>
            public double PlatformWgs84Lat { get; set; }
            /// <summary>
            /// Longitude in WGS84 format of the platform
            /// </summary>
            public double PlatformWgs84Lon { get; set; }
            /// <summary>
            /// Station Id of the station the platform is on
            /// </summary>
            public int StationId { get; set; }
        }
    }
}
