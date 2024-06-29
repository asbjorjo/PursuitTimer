using System.Globalization;

namespace PursuitTimer.Converters;

public class DeltaTimeToStringConverter : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        return ((double)value).ToString(parameter as string);
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        var toleranceString = value as string;
        var tolerance = double.Parse(toleranceString.Replace(',', '.'), NumberStyles.AllowDecimalPoint, CultureInfo.InvariantCulture);
        return tolerance;
    }
}
