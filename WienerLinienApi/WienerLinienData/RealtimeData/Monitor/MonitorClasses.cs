using System.Collections.Generic;

namespace WienerLinienApi.RealtimeData.Monitor
{

    public class MonitorData
    {
        public Data Data { get; set; }
        public Message Message { get; set; }

        public bool IsError()
        {
            return Data.IsNull() && Message.Value != "OK";
        }
    }
    public class Geometry
    {
        public string Type { get; set; }
        public List<double> Coordinates { get; set; }
    }

    public class Attributes
    {
        public int Rbl { get; set; }
    }

    public class Properties
    {
        public string Name { get; set; }
        public string Title { get; set; }
        public string Municipality { get; set; }
        public int MunicipalityId { get; set; }
        public string Type { get; set; }
        public string CoordName { get; set; }
        public string Gate { get; set; }
        public Attributes Attributes { get; set; }
    }

    public class LocationStop
    {
        public string Type { get; set; }
        public Geometry Geometry { get; set; }
        public Properties Properties { get; set; }
    }

    public class DepartureTime
    {
        public string TimePlanned { get; set; }
        public string TimeReal { get; set; }
        public int Countdown { get; set; }
    }

    public class Vehicle
    {
        public string Name { get; set; }
        public string Towards { get; set; }
        public string Direction { get; set; }
        public string RichtungsId { get; set; }
        public bool BarrierFree { get; set; }
        public bool RealtimeSupported { get; set; }
        public bool Trafficjam { get; set; }
        public string Type { get; set; }
        public int LinienId { get; set; }
    }

    public class Departure
    {
        public DepartureTime DepartureTime { get; set; }
        public Vehicle Vehicle { get; set; }
    }

    public class Departures
    {
        public List<Departure> Departure { get; set; }
    }

    public class Line
    {
        public string Name { get; set; }
        public string Towards { get; set; }
        public string Direction { get; set; }
        public string RichtungsId { get; set; }
        public bool BarrierFree { get; set; }
        public bool RealtimeSupported { get; set; }
        public bool Trafficjam { get; set; }
        public Departures Departures { get; set; }
        public string Type { get; set; }
        public int LineId { get; set; }
    }

    public class Monitor
    {
        public LocationStop LocationStop { get; set; }
        public List<Line> Lines { get; set; }
    }

    public class TrafficInfoCategory
    {
        public int Id { get; set; }
        public int RefTrafficInfoCategoryGroupId { get; set; }
        public string Name { get; set; }
        public string TrafficInfoNameList { get; set; }
        public string Title { get; set; }
    }

    public class TrafficInfoCategoryGroup
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }

    public class Data
    {
        public List<Monitor> Monitors { get; set; }
        public List<TrafficInfoCategory> TrafficInfoCategories { get; set; }
        public List<TrafficInfoCategoryGroup> TrafficInfoCategoryGroups { get; set; }

        public bool IsNull()
        {
            return Monitors != null && TrafficInfoCategories != null && TrafficInfoCategoryGroups != null;
        }

      
    }

    public class Message
    {
        public string Value { get; set; }
        public RealtimeErrorCode MessageCode { get; set; }
        public string ServerTime { get; set; }
    }



}
