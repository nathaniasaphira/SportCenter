using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace SportCenter.Utils;

public class VisibilityToBlurMultiConverter : IMultiValueConverter
{
    public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
    {
        var blurStrength = Application.Current.Resources["BlurStrength"];
        return blurStrength is double strength
            ? values.OfType<Visibility>().Any(v => v == Visibility.Visible) ? strength : 0.0
            : 0.0;
    }

    public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}