// -----------------------------------------------------------------------
// <copyright file="BrushConverter.cs" company="">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

namespace IPUI
{
    using System;
    using System.Collections.Generic;
    using System.Globalization;
    using System.Linq;
    using System.Text;
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Data;
    using System.Windows.Documents;
    using System.Windows.Input;
    using System.Windows.Media;
    using System.Windows.Media.Imaging;
    using System.Windows.Navigation;
    using System.Windows.Shapes;

    /// <summary>
    /// Converts a Color to a SolidColorBrush
    /// </summary>
    public class BrushConverter : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            return new SolidColorBrush(
                Color.FromArgb(255, System.Convert.ToByte(values[0]),
                    System.Convert.ToByte(values[1]), System.Convert.ToByte(values[2])));
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            object[] result = new object[4];
            result[0] = (byte)255;
            result[1] = ((SolidColorBrush)value).Color.R;
            result[2] = ((SolidColorBrush)value).Color.G;
            result[3] = ((SolidColorBrush)value).Color.B;

            return result;
        }
    }
}
