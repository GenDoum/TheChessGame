using System.Globalization;
using Microsoft.Maui.Graphics;

namespace Chess.Converter;

public class EnumToStringConverter : IValueConverter
{
    public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        if (value is ChessLibrary.Color color)
        {
            return color switch
            {
                ChessLibrary.Color.Black => new Microsoft.Maui.Graphics.Color(0, 0, 0),
                ChessLibrary.Color.White => new Microsoft.Maui.Graphics.Color(1, 1, 1),
                _ => throw new NotImplementedException()
            };
        }

        return null;
    }

    public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}
