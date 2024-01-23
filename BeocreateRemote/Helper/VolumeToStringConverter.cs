using System.Diagnostics;
using System.Globalization;

namespace BeocreateRemote.Helper
{
    public class VolumeToStringConverter : IValueConverter
    {
        public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
        {
            if (value == null) { return "🔇"; }
            Debug.WriteLine("Converter: " + value);
            return $"{value} %";
        }

        public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }

        public static string Convert(double volume)
        {
            return ((int)(volume * 100)).ToString();
        }

        public static double ConvertBack(string volume)
        {
            return double.Parse(volume) / 100;
        }
    }
}
