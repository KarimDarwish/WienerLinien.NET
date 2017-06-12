WienerLinien.NET 
=================

**WienerLinien.NET** is an easy-to-use .NET Wrapper for the Wiener Linien API using OpenData. 

It provides you with all features from the official API and all you need is an API-Key, which you can request [here](https://www.wien.gv.at/formularserver2/user/formular.aspx?pid=3b49a23de1ff43efbc45ae85faee31db&pn=B0718725a79fb40f4bb4b7e0d2d49f1d1)


Installation
-------------

The library is available via NuGet: 

    PM> Install-Package WienerLinien.NET
    
or you can download the binaries [here](https://github.com/KarimDarwish/Wiener-Linien-.NET/releases)


Documentation
-------------------

**Detailed information:**

 - [Project Wiki](https://github.com/KarimDarwish/Wiener-Linien-.NET/wiki)
 - [Sample projects](https://github.com/KarimDarwish/Wiener-Linien-.NET)

**Get started:**

First you need to create a WienerLinienContext classs where your API Key is stored.
```cs
    var wlContext = new WienerLinienContext("yourApiKey");
```
Then you can already start 

As Wiener Linien don't provide all stations in their API, we had to grab them from their csv files and combine/parse them to JSON ourselves (see JsonGenerator folder).

The stations are used to get the RBL Id for them, which is required to retrieve Realtime Data.

To get all stations:
```cs
    var allStations = await Stations.GetAllStationsAsync();
```
This will return a `List<Station>`, where you can get the RBL Id's using the Platform attribute.

Now you can get realtime monitor information:
```cs
      //initialize the RealtimeData object using the created context
    var rtd = new RealtimeData(context);
    
    //Create a List<int> of all RBL's we want to receive realtime data for
    var listRbls = new List<int>(){allStations[0].Platforms[0].RblNumber, allstations[0].Platforms[1].Rblnumber};
    
    //Create a Parameters object to include the Rbls  and get Realtime Data for them
    var parameters = new MonitorParameters(){Rbls= listRbls};
    
    //Get the monitor informatino asynchronous, and save them as MonitorData class
    var monitorInfo = await rtd.GetMonitorDataAsync(parameters);
    
    //Get the planned arrival time for the first line and the next vehicle arriving (index at Departure)
    var plannedTime = monitorInfo.Result.Data.Monitors[0].Lines[0].Departures.Departure[0].DepartureTime.TimePlanned;
```
To receive current TrafficInformation (current failures like elevator and other interruptions), you need to use the `GetTrafficInformationAsync` method.

As parameter you can choose either related lines or related stops as well as the type of failure.

Sample code:

```cs
    //initialize the RealtimeData object using the created context
    var rtd = new RealtimeData.RealtimeData(context);
    
    var relatedLines = new List<string>() { "U6", "U1", "18", "U4" };
    
    //Create a Parameters object to include lines I want information to
    var parameters = new Parameters.TrafficInfoParameters() { RelatedLines = relatedLines};
    
    //Get the traffic information asynchronous, and save them as TrafficInfoData class
    var trafficInfo = await rtd.GetTrafficInfoDataAsync(parameters);
    
    //Get the first traffic warning related to your selected lines
    var firstNotice = monitorInfo.Data.TrafficInfos[0].Description;
```
Then there is the option to get current news related to the Wiener Linien:

```cs
    //creating a new NewsWrapper object with your context
    var news = new NewsWrapper(context);
    
    //Retrives current news information, gets all available due to no Parameters set
    var newsInformation = await news.GetNewsInformationAsync(new Parameters.NewsParameters());
    
    /gets the first news item
    var firstNewsItem = newsInformation.DataObj.PoisObj[0].Description;
```





Credits
-------------

 - [Json.NET](http://www.newtonsoft.com/json) 



Contributors
--------------------
 - [Karim Darwish](https://github.com/KarimDarwish)
 - [Johannes Mayerl](https://github.com/johannesMayerl)
 - [Ali Sheikh](https://github.com/alaeschaik)
 

Of course it is possible to contribute yourself, to do so please:

 - Fork it
 - Create your feature branch: `git checkout -b my-feature`
 - Commit your changes: `git commit -am 'New feature'` 
 - Push to the branch: `git push origin my-feature` 
 - Submit a pull request

 

License
--------------

MIT License

Copyright (c) 2016 Karim Darwish

Permission is hereby granted, free of charge, to any person obtaining a copy of this software and associated documentation files (the "Software"), to deal in the Software without restriction, including without limitation the rights to use, copy, modify, merge, publish, distribute, sublicense, and/or sell copies of the Software, and to permit persons to whom the Software is furnished to do so, subject to the following conditions:

The above copyright notice and this permission notice shall be included in all copies or substantial portions of the Software.

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.
