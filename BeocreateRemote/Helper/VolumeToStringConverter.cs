using System.Diagnostics;
using System.Globalization;

namespace BeocreateRemote.Helper
{
    public class VolumeToStringConverter : IValueConverter
    {
        public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
        {
            if (value == null) { return "🔇"; }
            return $"{(int)(double.Parse((string)value) * 100)} %";
        }

        public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
