using System.Collections.Generic;

namespace WienerLinienApi.RealtimeData.TrafficInfo
{
    public class TrafficInfoCategory
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int RefTrafficInfoCategoryGroupId { get; set; }
        public string Title { get; set; }
    }

    public class TrafficInfoCategoryGroup
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }

    public class Time
    {
        public string End { get; set; }
        public string Resume { get; set; }
        public string Start { get; set; }
    }

    public class TrafficInfo
    {
        public string Description { get; set; }
        public string Name { get; set; }
        public string Owner { get; set; }
        public string Priority { get; set; }
        public int RefTrafficInfoCategoryId { get; set; }
        public List<string> RelatedLines { get; set; }
        public List<int> RelatedStops { get; set; }
        public Time Time { get; set; }
        public string Title { get; set; }
    }

    public class Data
    {
        public List<TrafficInfoCategory> TrafficInfoCategories { get; set; }
        public List<TrafficInfoCategoryGroup> TrafficInfoCategoryGroups { get; set; }
        public List<TrafficInfo> TrafficInfos { get; set; }
        public bool IsNull()
        {
            return TrafficInfos == null && TrafficInfoCategories == null && TrafficInfoCategoryGroups == null;
        }
    }

    public class TrafficInfoData
    {
        public Data Data { get; set; }
        public Message Message { get; set; }
    }
    public class Message
    {
        public string Value { get; set; }
        public RealtimeErrorCode MessageCode { get; set; }
        public string ServerTime { get; set; }
    }
}