using System.Globalization;

namespace PursuitTimer.Converters;

public class TimeSpanToStringConverter : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        return ((TimeSpan)value).ToString(parameter as string);
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        var secondsString = value as string;
        var seconds = double.Parse(secondsString.Replace(',','.'), CultureInfo.InvariantCulture);
        return TimeSpan.FromSeconds(seconds);
    }
}
