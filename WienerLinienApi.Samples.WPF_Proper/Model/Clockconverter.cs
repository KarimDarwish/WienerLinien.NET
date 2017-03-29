using System;
using System.Windows;
using System.Windows.Data;

namespace WienerLinienApi.Samples.WPF_Proper.Model
{
    public class ClockConverter : IValueConverter
    {
        #region IValueConverter Member

        public object  Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
 	        String zeigertyp = parameter as String;
            if (zeigertyp == null) zeigertyp = "Sekunde";
            

            if (value is DateTime)
            {
                var zeit = (DateTime) value;
                switch (zeigertyp.Substring(0,1).ToLower())
                {
                    case "h" :
                        return (zeit.Hour * 30) + (zeit.Minute * 0.5)+ (zeit.Second * (1 / 120));
                    case "m" :
                        return zeit.Minute * 6;
                    default :
                        return zeit.Second * 6;
                }                
            }
            else return 0;
        }

        public object  ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
 	        throw new NotImplementedException();
        }
        #endregion
    }
}
