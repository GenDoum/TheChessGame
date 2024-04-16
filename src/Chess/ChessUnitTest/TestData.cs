namespace ChessUnitTest;

public class TestData
{
    public static IEnumerable<object[]> ValidBishopPositionsData()
    {
        yield return new object[] { 1, 1, 2, 2 };
        yield return new object[] { 3, 3, 1, 5 };
        yield return new object[] { 3, 3, 4, 4 };
        yield return new object[] { 3, 3, 5, 1 };
    }

    public static IEnumerable<object[]> InvalidBishopPositionsData()
    {
        
        yield return new object[] { 1, 1, 1, 2 }; // Vertical move
        yield return new object[] { 3, 3, 5, 3 }; // Non-diagonal move
        
    }

    public static IEnumerable<object[]> ValidKingPositionsData()
    {
        yield return new object[] { 1, 1, 1, 2 }; // Move up one square
        yield return new object[] { 3, 3, 4, 3 }; // Move one square to the right
        
    }

    public static IEnumerable<object[]> InvalidKingPositionsData()
    {
        yield return new object[] { 1, 1, 3, 3 }; // Move two squares
        yield return new object[] { 3, 3, 1, 5 }; // Move more than one square diagonally
        
    }

    public static IEnumerable<object[]> ValidRookPositionsData()
    {
        yield return new object[] { 1, 1, 1, 2 }; // Move up one square
        yield return new object[] { 3, 3, 3, 4 }; // Move one square to the right
    }

    public static IEnumerable<object[]> InvalidRookPositionsData()
    {
        yield return new object[] { 1, 1, 2, 2 }; // Diagonal move
        yield return new object[] { 3, 3, 4, 5 }; // Non-linear move
    }

    public static IEnumerable<object[]> ValidQueenPositionsData()
    {
        yield return new object[] { 1, 1, 1, 5 }; // Horizontal move
        yield return new object[] { 1, 1, 5, 1 }; // Vertical move
        yield return new object[] { 1, 1, 3, 3 }; // Diagonal move
    }

    public static IEnumerable<object[]> InvalidQueenPositionsData()
    {
        yield return new object[] { 1, 1, 3, 4 }; // Non-linear move
    }

    public static IEnumerable<object[]> ValidKnightPositionsData()
    {
        yield return new object[] { 1, 1, 2, 3 }; // L-shaped move
        yield return new object[] { 3, 3, 1, 2 }; // L-shaped move
    }

    public static IEnumerable<object[]> InvalidKnightPositionsData()
    {
        yield return new object[] { 1, 1, 1, 2 }; // Vertical move
        yield return new object[] { 3, 3, 4, 4 }; // Diagonal move
    }

    public static IEnumerable<object[]> ValidPawnPositionsData()
    {
        yield return new object[] { 1, 1, 1, 2 }; // Move up one square
    }

    public static IEnumerable<object[]> InvalidPawnPositionsData()
    {
        yield return new object[] { 1, 1, 1, 3 }; // Move two squares
        yield return new object[] { 1, 1, 2, 1 }; // Move sideways
    }
}