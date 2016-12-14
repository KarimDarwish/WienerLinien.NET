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

        public IEnumerable<string> LineNameColl { get; set; }

        public AutocomFile()
        {
            var init = new NewFavoriteStop("bus");
            Task.Run(async () =>
            {
                TestItems = (await init.GetStaionNames("bus"));
            }).Wait();

            
            
        }
    }
}
