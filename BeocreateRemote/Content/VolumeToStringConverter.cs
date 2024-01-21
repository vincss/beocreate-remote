using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeocreateRemote.Content
{
    public class VolumeToStringConverter : IValueConverter
    {
        public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
        {
            Debug.WriteLine("VolumeToStringConverter", value);

            if (value == null) { return "🔇"; }
            return $"{(int)(Double.Parse((string)value) * 100)} %";
        }

        public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
