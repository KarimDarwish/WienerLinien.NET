using System.Collections.Generic;
using System.Threading.Tasks;
using WienerLinienApi.Samples.WPF_Proper.Model;

namespace WienerLinienApi.Samples.WPF_Proper.View
{
    internal class AutocomFile
    {
        public AutocomFile()
        {
            Task.Run(async () => { TestItems = await NewFavoriteStop.GetStaionNames("ptBusCity"); }).Wait();
        }

        public string TestText { get; set; }
        public List<string> TestItems { get; set; }
        public List<string> LineNameColl { get; set; }
        public string SelectedLine { get; set; }
    }
}