using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using WienerLinienApi.Information;
using WienerLinienApi.Model;
using WienerLinienApi.News;
using WienerLinienApi.RealtimeData;

namespace WienerLinienApi.Test
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public async Task GetStations()
        {
            var stations = await Stations.GetAllStationsAsync();
            Assert.IsNotNull(stations);
        }
        public async Task GetMonitorData()
        {
            var wlc = new WienerLinienContext("VgIHscNiquj8LYbV");
            var stations = await Stations.GetAllStationsAsync();
            var firstFive = stations.Take(4);
            var rtd = new RealtimeData.RealtimeData(wlc);
            var listRbls = firstFive.Select(item => item.Platforms[0].RblNumber).ToList();
            var mp = new Parameters.MonitorParameters { Rbls = listRbls };
            var data = await rtd.GetMonitorDataAsync(mp);
            var a = data.Data.Monitors[0].Lines[0].Departures.Departure[0].DepartureTime.TimePlanned;
            Assert.IsFalse(data.Data.IsNull());

        }
        [TestMethod]
        public async Task GetTrafficData()
        {
            var wlc = new WienerLinienContext("VgIHscNiquj8LYbV");
            var rtd = new RealtimeData.RealtimeData(wlc);
            var currentTrafficInfo = await rtd.GetTrafficInfoDataAsync(new Parameters.TrafficInfoParameters());
            Assert.IsFalse(currentTrafficInfo.Data.IsNull());
        }
        

        [TestMethod]
        public async Task GetCurrentNews()
        {
            //takes long due to huge strings in result
            var wlc = new WienerLinienContext("VgIHscNiquj8LYbV");
            var news = new NewsWrapper(wlc);
            var data = await news.GetNewsInformationAsync(new Parameters.NewsParameters());
            var a = data.DataObj.PoisObj[0].Description;
            Assert.IsFalse(data.DataObj.IsNull());
        }
        [TestMethod]
        [ExpectedException(typeof(RealtimeError))]
        public async Task GetTrafficDataWithInvalidKey()
        {
            var wlc = new WienerLinienContext("VgIHscNiquj8asasasLYbV");
            var rtd = new RealtimeData.RealtimeData(wlc);
            var currentTrafficInfo = await rtd.GetTrafficInfoDataAsync(new Parameters.TrafficInfoParameters());
        }
    }
}
