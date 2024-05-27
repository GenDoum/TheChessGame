using System.Globalization;

namespace Chess.Converter;

public class EnumToStringConverter : IValueConverter
{
    public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        if (value is ChessLibrary.Color color)
        {
            return color switch
            {
                ChessLibrary.Color.Black => "Black",
                ChessLibrary.Color.White => "White",
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
