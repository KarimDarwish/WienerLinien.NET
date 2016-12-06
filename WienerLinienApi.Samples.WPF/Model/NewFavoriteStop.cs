using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WienerLinienApi.Samples.WPF.Model
{
    class NewFavoriteStop
    {
        public string StopName { get; set; }
        public string Line { get; set; }
        public string StopType { get; set; }
        public int Platforms { get; set; }
        public int [] TimesToWait { get; set; }

        public NewFavoriteStop()
        {

        }
    }
}
