namespace ChessUnitTest;

public class TestData
{
    public static IEnumerable<object[]> ValidBishopPositionsData()
    {
        yield return new object[] { 1, 1, 2, 2 };
        yield return new object[] { 3, 3, 1, 5 };
        yield return new object[] { 3, 3, 4, 4 };
        yield return new object[] { 3, 3, 5, 1 };

        yield return new object[] { 2, 2, 5, 5 }; // Long diagonal move
        yield return new object[] { 6, 6, 2, 2 }; // Long diagonal move across the board
    }

    public static IEnumerable<object[]> InvalidBishopPositionsData()
    {
        
        yield return new object[] { 1, 1, 1, 2 }; // Vertical move
        yield return new object[] { 3, 3, 5, 3 }; // Non-diagonal move

        yield return new object[] { 4, 4, 4, 5 }; // Horizontal move
        yield return new object[] { 4, 4, 6, 3 }; // Jump over pieces (assuming pieces are blocking)
    }

    public static IEnumerable<object[]> ValidKingPositionsData()
    {
        yield return new object[] { 1, 1, 1, 2 }; // Move up one square
        yield return new object[] { 3, 3, 4, 3 }; // Move one square to the right

        yield return new object[] { 1, 1, 2, 1 }; // Move right one square
        yield return new object[] { 8, 8, 7, 7 }; // Move diagonally down-left

    }

    public static IEnumerable<object[]> InvalidKingPositionsData()
    {
        yield return new object[] { 1, 1, 3, 3 }; // Move two squares
        yield return new object[] { 3, 3, 1, 5 }; // Move more than one square diagonally

        // Invalid King Moves
        yield return new object[] { 5, 5, 5, 8 }; // Move three squares down
        yield return new object[] { 2, 2, 2, 5 }; // Move three squares right

    }

    public static IEnumerable<object[]> ValidRookPositionsData()
    {
        yield return new object[] { 1, 1, 1, 2 }; // Move up one square
        yield return new object[] { 3, 3, 3, 4 }; // Move one square to the right

        yield return new object[] { 4, 4, 4, 8 }; // Move to the end of the row
        yield return new object[] { 3, 3, 8, 3 }; // Move down to the bottom of the column
    }

    public static IEnumerable<object[]> InvalidRookPositionsData()
    {
        yield return new object[] { 1, 1, 2, 2 }; // Diagonal move
        yield return new object[] { 3, 3, 4, 5 }; // Non-linear move

        yield return new object[] { 2, 2, 3, 3 }; // Diagonal move
        yield return new object[] { 7, 7, 5, 6 }; // Non-linear move
    }

    public static IEnumerable<object[]> ValidQueenPositionsData()
    {
        yield return new object[] { 1, 1, 1, 5 }; // Horizontal move
        yield return new object[] { 1, 1, 5, 1 }; // Vertical move
        yield return new object[] { 1, 1, 3, 3 }; // Diagonal move

        yield return new object[] { 5, 5, 5, 1 }; // Move horizontally across the board
        yield return new object[] { 1, 1, 8, 8 }; // Move diagonally across the board
    }

    public static IEnumerable<object[]> InvalidQueenPositionsData()
    {
        yield return new object[] { 1, 1, 3, 4 }; // Non-linear move

        yield return new object[] { 2, 2, 4, 5 }; // Irregular move
        yield return new object[] { 3, 3, 5, 6 }; // Irregular move
    }

    public static IEnumerable<object[]> ValidKnightPositionsData()
    {
        yield return new object[] { 1, 1, 2, 3 }; // L-shaped move
        yield return new object[] { 3, 3, 1, 2 }; // L-shaped move

        yield return new object[] { 5, 5, 6, 7 }; // L-shaped move
        yield return new object[] { 2, 2, 3, 4 }; // L-shaped move
    }

    public static IEnumerable<object[]> InvalidKnightPositionsData()
    {
        yield return new object[] { 1, 1, 1, 2 }; // Vertical move
        yield return new object[] { 3, 3, 4, 4 }; // Diagonal move

        yield return new object[] { 4, 4, 4, 6 }; // Horizontal move
        yield return new object[] { 3, 3, 5, 5 }; // Diagonal move
    }

    public static IEnumerable<object[]> ValidPawnPositionsData()
    {
        yield return new object[] { 1, 1, 1, 2 }; // Move up one square
        yield return new object[] { 2, 2, 2, 4 }; // Initial move of two squares
        yield return new object[] { 5, 5, 5, 6 }; // Move forward one square
        yield return new object[] { 6, 2, 7, 3 }; // Diagonal capture
    }

    public static IEnumerable<object[]> InvalidPawnPositionsData()
    {
        yield return new object[] { 1, 1, 1, 3 }; // Move two squares
        yield return new object[] { 1, 1, 2, 1 }; // Move sideways

        yield return new object[] { 4, 4, 4, 6 }; // Move two squares not from initial position
        yield return new object[] { 7, 7, 8, 8 }; // Diagonal move without capture
    }
}