using System.Collections.Generic;
using Newtonsoft.Json;
using WienerLinienApi.RealtimeData;

namespace WienerLinienApi.Model
{

    public class News
    {
        [JsonProperty("Data")]
        public Data DataObj { get; set; }
        [JsonProperty("Message")]
        public Message MessageObj { get; set; }

        public class Pois
        {
            public int RefPoiCategoryId { get; set; }
            public string Title { get; set; }
            public string Name { get; set; }
            public Attributes Attributes { get; set; }
            public long Start { get; set; }
            public long End { get; set; }
            public string Description { get; set; }
        }

        public class Attributes
        {
            public string Empty { get; set; }
            public string[] RelatedLines { get; set; }
            public List<int> RelatedStops { get; set; }
            [JsonProperty("Location")]
            public string LocationObj { get; set; }
            public string Station { get; set; }
            public string Status { get; set; }
            public List<int> Rbls { get; set; }
            public string UnavailableFrom { get; set; }
            public string UnavailableTo { get; set; }
            public string Towards { get; set; }
        }

        public class Poicategory
        {
            public int Id { get; set; }
            public int RefPoiCategoryGroupId { get; set; }
            public string Title { get; set; }
            public string Name { get; set; }
        }

        public class Poicategorygroup
        {
            public int Id { get; set; }
            public string Name { get; set; }
        }


        public class Properties
        {
            public string Type { get; set; }
            public string CoordName { get; set; }
        }

        public class Location
        {
            public string Type { get; set; }
            public Properties Properties { get; set; }
        }
        public class Data
        {
            [JsonProperty("pois")]
            public List<Pois> PoisObj { get; set; }
            public List<Poicategory> PoiCategories { get; set; }
            public List<Poicategorygroup> PoiCategoryGroups { get; set; }
            public bool IsNull()
            {
                return !((PoisObj != null && PoisObj.Count != 0) && (PoiCategories != null && PoiCategories.Count != 0) && (PoiCategoryGroups != null && PoiCategoryGroups.Count != 0));
            }
        }
        public class Message
        {
            public string Value { get; set; }
            public RealtimeErrorCode MessageCode { get; set; }
            public string ServerTime { get; set; }
        }

    }



}
