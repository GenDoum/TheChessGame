using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ChessLibrary;

namespace Chess.Converter
{
    public class ChessboardColorConverter : IValueConverter
    {
        public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
        {
            var caseColor = value as Case;
            if (caseColor == null)
            {
                return Colors.Transparent;
            }

            return (caseColor.Line + caseColor.Column) % 2 == 0 ? Colors.DarkGrey : Colors.PaleGoldenrod;
        }

        public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
