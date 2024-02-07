using System.Globalization;

namespace BeocreateRemote.Helper
{
    public class VolumeToStringConverter : IValueConverter
    {
        private static readonly int VolumeMax = 100;
        private static readonly int VolumeMin = 0;

        public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
        {
            if (value == null) { return "🔇"; }
            //Debug.WriteLine("Converter: " + value);
            return $"{value} %";
        }

        public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }

        public static string Convert(int volume)
        {
            return volume.ToString();
        }

        public static int ConvertBack(string volume)
        {
            var value = double.Parse(volume, NumberStyles.Any);
            if (value > VolumeMax) { value = VolumeMax; } else if (value < VolumeMin) { value = VolumeMin; }
            return (int)value;
        }
    }
}
