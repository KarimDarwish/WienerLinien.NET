using System.Collections.Generic;

namespace WienerLinienApi.Model
{

    public class News
    {
        public Data DataObj { get; set; }
        public class Data
        {
            public List<Pois> PoisObj { get; set; }
            public List<Poicategory> PoiCategories { get; set; }
            public List<Poicategorygroup> PoiCategoryGroups { get; set; }
        }

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
    }

   

}
