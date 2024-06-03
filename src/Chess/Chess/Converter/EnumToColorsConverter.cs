using System.Globalization;
using Microsoft.Maui.Graphics;

namespace Chess.Converter;

public class EnumToColorsConverter : IValueConverter
{
    public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        if (value is ChessLibrary.Color color)
        {
            return color switch
            {
                ChessLibrary.Color.Black => Colors.Black,
                ChessLibrary.Color.White => Colors.White,
                _ => Colors.Transparent
            };
        }

        return Colors.Transparent;
    }

    public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}
