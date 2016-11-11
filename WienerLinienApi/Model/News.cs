using System.Collections.Generic;
using Newtonsoft.Json;

namespace WienerLinienApi.Model
{

    public class News
    {
        [JsonProperty("Data")]
        public Data DataObj { get; set; }


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
            public string Location { get; set; }
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

        //public class Pois
        //{
        //    public int refPoiCategoryId { get; set; }
        //    public string title { get; set; }
        //    public string description { get; set; }
        //    public string name { get; set; }
        //    public Attributes attributes { get; set; }
        //    public int? id { get; set; }
        //    public string teaser { get; set; }
        //    public List<string> names { get; set; }
        //    public Location location { get; set; }
        //    public long? start { get; set; }
        //    public long? end { get; set; }
        //}

        //public class PoiCategory
        //{
        //    public int id { get; set; }
        //    public int refPoiCategoryGroupId { get; set; }
        //    public string title { get; set; }
        //    public string name { get; set; }
        //}

        //public class PoiCategoryGroup
        //{
        //    public int id { get; set; }
        //    public string name { get; set; }
        //    public string title { get; set; }
        //}
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


    }



}
