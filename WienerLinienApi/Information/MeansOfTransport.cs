namespace WienerLinienApi.Information
{
    public class MeansOfTransportWrapper
    {
        public const string Metro = "ptmetro";
        public const string Tram = "pttram";
        public const string TramWlb = "pttramwlb";
        public const string Bus = "ptbuscity";
        public const string NightBus = "ptbusnight";
        public const string TrainS = "pttrains";
        public static MeansOfTransport GetMeansOfTransportFromString(string input)
        {
            var returnMot = MeansOfTransport.Unknown;
            switch (input.ToLowerInvariant())
            {
               
                case Metro:
                    returnMot = MeansOfTransport.Metro;
                    break;
                case Tram:
                    returnMot = MeansOfTransport.Tram;
                    break;
                case TramWlb:
                    returnMot = MeansOfTransport.TramWlb;
                    break;
                case Bus:
                    returnMot = MeansOfTransport.Bus;
                    break;
                case TrainS:
                    returnMot = MeansOfTransport.SBahn;
                    break;
                case NightBus:
                    returnMot = MeansOfTransport.NightBus;
                    break;
                case "other":
                    returnMot = MeansOfTransport.Other;
                    break;
            }

            return returnMot;
        }

        public static string GetStringFromMeansOfTransport(MeansOfTransport input)
        {
            var returnString = "";

            switch (input)
            {
                case MeansOfTransport.Metro:
                    returnString = Metro;
                    break;
                case MeansOfTransport.Tram:
                    returnString = Tram;
                    break;
                case MeansOfTransport.TramWlb:
                    returnString = TramWlb;
                    break;
                case MeansOfTransport.Bus:
                    returnString = Bus;
                    break;
                case MeansOfTransport.NightBus:
                    returnString = NightBus;
                    break;
            }

            return returnString;
        }
    }
    public enum MeansOfTransport
    {
        Metro = 1,
        Tram,
        TramWlb,
        Bus,
        SBahn,
        Other,
        NightBus,
        Unknown
    }
}
