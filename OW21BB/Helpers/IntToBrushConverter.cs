using OW21BB.Models;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows.Media;

namespace OW21BB.Helpers
{
    class IntToBrushConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            //int -> brush

            var type = (EnumTypes)Enum.Parse(typeof(EnumTypes), value.ToString());
            switch (type)
            {
                case EnumTypes.CPU: return Brushes.LightYellow;
                case EnumTypes.RAM: return Brushes.LightGreen;
                case EnumTypes.MOTHERBOARD: return Brushes.LightPink;
                case EnumTypes.DRIVE: return Brushes.LightBlue;

                default:
                    return Brushes.LightBlue;                   
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
           return Binding.DoNothing;
        }
    }
}
