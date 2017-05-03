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
            var inputL = input.ToLowerInvariant();
            if (inputL.Contains(Metro))
            {
                returnMot = MeansOfTransport.Metro;
            }
            else if (inputL.Contains(Tram))
            {
                returnMot = MeansOfTransport.Tram;
            }
            else if (inputL.Contains(TramWlb))
            {
                returnMot = MeansOfTransport.TramWlb;
            }
            else if (inputL.Contains(Bus))
            {
                returnMot = MeansOfTransport.Bus;
            }
            else if (inputL.Contains(NightBus))
            {
                returnMot = MeansOfTransport.NightBus;
            }
            else if (inputL.Contains(TrainS))
            {
                returnMot = MeansOfTransport.SBahn;
            }
            else
            {
                returnMot = MeansOfTransport.Unknown;
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
