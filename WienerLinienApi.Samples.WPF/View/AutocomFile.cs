using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WienerLinienApi.Samples.WPF.Model;

namespace WienerLinienApi.Samples.WPF.View
{

    class AutocomFile
    {
        public string TestText { get; set; }

        public IEnumerable<string> TestItems { get; set; }

        public AutocomFile()
        {
            var choices = new NewFavoriteStop("bus");
            TestItems = choices.BusStops;
        }
    }
}
