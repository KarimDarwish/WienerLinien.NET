using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WienerLinienApi.WienerLinienData
{
   public class WienerLinienData
    {
        public string ApiKey { get; set; }
        public RealtimeData.RealtimeData RealtimeData { get; set; }

    }
}
