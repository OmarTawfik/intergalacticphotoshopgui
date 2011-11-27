namespace IntergalacticControls
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
        /// <summary>
        /// Converts the given array of bytes to a SolidColorBrush
        /// </summary>
        /// <param name="values">Given objects</param>
        /// <param name="targetType">Target type</param>
        /// <param name="parameter">Parameter object</param>
        /// <param name="culture">Culture info</param>
        /// <returns>The SolidColorBrush</returns>
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            return new SolidColorBrush(
                Color.FromArgb(
                255,
                System.Convert.ToByte(values[0]),
                    System.Convert.ToByte(values[1]),
                    System.Convert.ToByte(values[2])));
        }

        /// <summary>
        /// Converts the SolidColorBrush back to RGB
        /// </summary>
        /// <param name="value">Given SolidColorBrush</param>
        /// <param name="targetTypes">Target types</param>
        /// <param name="parameter">Parameter object</param>
        /// <param name="culture">Culture infos</param>
        /// <returns>RGB values</returns>
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
