using ChessLibrary;
using System.Drawing;
using System.Net.NetworkInformation;

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
        yield return new object[] { 8, 8, 9, 9 }; // Move out of bounds
    }

    public static IEnumerable<object[]> ValidKingPositionsData()
    {
        yield return new object[] { 1, 1, 1, 2 }; // Move up one square
        yield return new object[] { 3, 3, 4, 3 }; // Move one square to the right
        yield return new object[] { 8, 8, 7, 7 }; // Move diagonally down-left

        yield return new object[] { 1, 1, 2, 1 }; // Move right one square
        yield return new object[] { 8, 8, 8, 7 }; // Move down one square

    }

    public static IEnumerable<object[]> InvalidKingPositionsData()
    {
        yield return new object[] { 1, 1, 3, 3 }; // Move two squares
        yield return new object[] { 3, 3, 1, 5 }; // Move more than one square diagonally

        // Invalid King Moves
        yield return new object[] { 5, 5, 5, 8 }; // Move three squares down
        yield return new object[] { 2, 2, 2, 5 }; // Move three squares right

        // King moves near the edges
        yield return new object[] { 1, 1, 1, 0 }; // Move outside the board (up)
        yield return new object[] { 8, 8, 8, 9 }; // Move outside the board (down)
        yield return new object[] { 1, 1, 0, 1 }; // Move outside the board (left)
        yield return new object[] { 8, 8, 9, 8 }; // Move outside the board (right)

    }

    public static IEnumerable<object[]> ValidRookPositionsData()
    {
        yield return new object[] { 1, 1, 1, 2 }; // Move up one square
        yield return new object[] { 3, 3, 3, 4 }; // Move one square to the right

        yield return new object[] { 4, 4, 4, 8 }; // Move to the end of the row
        yield return new object[] { 3, 3, 8, 3 }; // Move down to the bottom of the column

        yield return new object[] { 8, 8, 8, 1 }; // Move to the top of the column
        yield return new object[] { 1, 8, 8, 8 }; // Move to the bottom of the column
    }

    public static IEnumerable<object[]> InvalidRookPositionsData()
    {
        yield return new object[] { 1, 1, 2, 2 }; // Diagonal move
        yield return new object[] { 3, 3, 4, 5 }; // Non-linear move

        yield return new object[] { 2, 2, 3, 3 }; // Diagonal move
        yield return new object[] { 7, 7, 5, 6 }; // Non-linear move

        yield return new object[] { 1, 1, 0, 1 }; // Move outside the board (left)
        yield return new object[] { 1, 1, 1, 0 }; // Move outside the board (up)
        yield return new object[] { 8, 8, 9, 8 }; // Move outside the board (right)
        yield return new object[] { 8, 8, 8, 9 }; // Move outside the board (down)
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

        // Additional valid knight moves
        yield return new object[] { 8, 8, 6, 7 }; // L-shaped move near the edge (up-right)
        yield return new object[] { 1, 1, 3, 2 }; // L-shaped move near the edge (down-left)
    }

    public static IEnumerable<object[]> InvalidKnightPositionsData()
    {
        yield return new object[] { 1, 1, 1, 2 }; // Vertical move
        yield return new object[] { 3, 3, 4, 4 }; // Diagonal move
        yield return new object[] { 4, 4, 4, 6 }; // Horizontal move
        yield return new object[] { 3, 3, 5, 5 }; // Diagonal move

        // Invalid knight moves near the edges
        yield return new object[] { 1, 1, 3, 0 }; // Move outside the board (up)
        yield return new object[] { 8, 8, 6, 9 }; // Move outside the board (down)
        yield return new object[] { 1, 1, 0, 3 }; // Move outside the board (left)
        yield return new object[] { 8, 8, 9, 6 }; // Move outside the board (right)
    }


    public static IEnumerable<object[]> ValidPawnPositionsData()
    {
        yield return new object[] { 1, 1, 1, 2 }; // Move up one square
        yield return new object[] { 5, 5, 5, 6 }; // Move forward one square
        yield return new object[] { 6, 2, 7, 3 }; // Diagonal capture

        // Additional valid pawn moves
        yield return new object[] { 4, 6, 4, 7 }; // Move up one square (black pawn)
        yield return new object[] { 1, 6, 1, 7 }; // Move up one square (white pawn, promotion)
    }

    public static IEnumerable<object[]> InvalidPawnPositionsData()
    {
        yield return new object[] { 1, 1, 1, 4 }; // Move two squares (valid initial move)
        yield return new object[] { 1, 1, 2, 1 }; // Move sideways

        yield return new object[] { 4, 4, 4, 6 }; // Move two squares not from initial position

        yield return new object[] { 1, 1, 1, 0 }; // Move outside the board (up)
        yield return new object[] { 8, 8, 8, 9 }; // Move outside the board (down)
        yield return new object[] { 1, 1, 0, 1 }; // Move outside the board (left)
        yield return new object[] { 8, 8, 9, 8 }; // Move outside the board (right)
    }


    public static IEnumerable<object[]> ValidUserPseudo()
    {
        var piecesRempli = new List<Piece> { new Rook (ChessLibrary.Color.Black, 1), new Queen (ChessLibrary.Color.Black, 1), new Pawn(ChessLibrary.Color.Black, 1), new Knight(ChessLibrary.Color.Black, 1), new King(ChessLibrary.Color.Black, 1)};

        yield return new object[] {"pseudo", "password", ChessLibrary.Color.White, true, new List<Piece>(),  0}; // Good Pseudo with Color.White and connected
        yield return new object[] {"pseudo", "password", ChessLibrary.Color.Black, false, new List<Piece>(), 0}; // Good Pseudo with Color.Black and not connected

        yield return new object[] {"pseudo", null, ChessLibrary.Color.White, true, piecesRempli, 0}; // Good Pseudo with password null and Color.White
        yield return new object[] {"pseudo", null, ChessLibrary.Color.Black, false, piecesRempli, 0}; // Good Pseudo with password null and Color.Black
    }

    public static IEnumerable<object[]> InvalidUserPseudo()
    {
        

        yield return new object[] {"", "password", ChessLibrary.Color.White, true, new List<Piece>(), 0}; // Empty pseudo with Color.White
        yield return new object[] {"", "password", ChessLibrary.Color.White, false, new List<Piece>(), 0}; // Empty pseudo with Color.White

        yield return new object[] {"", "password", ChessLibrary.Color.Black, true, new List<Piece>(), 0}; // Empty pseudo with Color.Black
        yield return new object[] {"", "password", ChessLibrary.Color.Black, false, new List<Piece>(), 0}; // Empty pseudo with Color.Black

        yield return new object[] {" ", "password", ChessLibrary.Color.White, true, new List<Piece>(), 0}; // Pseudo with white space and Color.White
        yield return new object[] {" ", "password", ChessLibrary.Color.White , false, new List<Piece>(), 0}; // Pseudo with white space and Color.White

        yield return new object[] {" ", "password", ChessLibrary.Color.Black, true, new List<Piece>(), 0}; // Pseudo with white space and Color.Black
        yield return new object[] {" ", "password", ChessLibrary.Color.Black, false, new List<Piece>(), 0}; // Pseudo with white space and Color.Black

        yield return new object[] {null, "password", ChessLibrary.Color.White, true, new List<Piece>(), 0}; // Pseudo null with Color.White
        yield return new object[] {null, "password", ChessLibrary.Color.White, false, new List<Piece>(), 0}; // Pseudo null with Color.White

        yield return new object[] {null, "password", ChessLibrary.Color.Black, true, new List<Piece>(), 0}; // Pseudo null with Color.Black
        yield return new object[] {null, "password", ChessLibrary.Color.Black , false, new List<Piece>(), 0}; // Pseudo null with Color.Black
    }

    public static IEnumerable<object[]> ValidUserPassword()
    {
        yield return new object[] {"pseudo", "password", ChessLibrary.Color.White, true, new List<Piece>(), 0}; // Good password with Color.White
        yield return new object[] {"pseudo", "password", ChessLibrary.Color.White, false, new List<Piece>(), 0}; // Good password with Color.White

        yield return new object[] {"pseudo", "password", ChessLibrary.Color.Black, true, new List<Piece>(), 0}; // Good password with Color.Black
        yield return new object[] {"pseudo", "password", ChessLibrary.Color.Black, false, new List<Piece>(), 0}; // Good password with Color.Black
        
        yield return new object[] {"pseudo", null, ChessLibrary.Color.White, true, new List<Piece>(), 0}; // Good password with Color.White
        yield return new object[] {"pseudo", null, ChessLibrary.Color.White, false, new List<Piece>(), 0}; // Good password with Color.White

        yield return new object[] {"pseudo", null, ChessLibrary.Color.Black, true, new List<Piece>(), 0}; // Good password with Color.Black
        yield return new object[] {"pseudo", null, ChessLibrary.Color.Black, false, new List<Piece>(), 0}; // Good password with Color.Black
    }

    public static IEnumerable<object[]> InvalidUserPassword()
    {
        yield return new object[] {"pseudo", "", ChessLibrary.Color.White, true, new List<Piece>(), 0}; // Empty password qui Color.White
        yield return new object[] {"pseudo", "", ChessLibrary.Color.White, false, new List<Piece>(), 0}; // Empty password qui Color.White

        yield return new object[] {"pseudo", "", ChessLibrary.Color.Black, true, new List<Piece>(), 0}; // Empty password qui Color.Black
        yield return new object[] {"pseudo", "", ChessLibrary.Color.Black, false, new List<Piece>(), 0}; // Empty password qui Color.Black

    }

    public static IEnumerable<object[]> ValidUserColor()
    {
        yield return new object[] {"pseudo", "password", ChessLibrary.Color.White, true, new List<Piece>(), 0}; // Good User with Color.White
        yield return new object[] {"pseudo", "password", ChessLibrary.Color.White, false, new List<Piece>(), 0}; // Good User with Color.White

        yield return new object[] {"pseudo", "password", ChessLibrary.Color.Black, true, new List<Piece>(), 0}; // Good User with Color.Black
        yield return new object[] {"pseudo", "password", ChessLibrary.Color.Black, false, new List<Piece>(), 0}; // Good User with Color.Black

        yield return new object[] {"pseudo", null, ChessLibrary.Color.White, true, new List<Piece>(), 0}; // Good User with null password and Color.White
        yield return new object[] {"pseudo", null, ChessLibrary.Color.Black, false, new List<Piece>(), 0}; // Good User with null password and Color.Black
    }
    
    public static IEnumerable<object[]> InvalidUserColor() 
    {
        yield return new object[] {"pseudo", "password", null, true, new List<Piece>(), 0 }; // Good User with color null
        yield return new object[] {"pseudo", "password", null, false, new List<Piece>(), 0 }; // Good User with color null

        yield return new object[] {"pseudo", null, null, true, new List<Piece>(), 0}; // Good User with wrong color null
        yield return new object[] {"pseudo", null, null, false, new List<Piece>(), 0}; // Good User with wrong color null
    }

    public static IEnumerable<object[]> ValidBoolConnected()
    {
        yield return new object[] {"pseudo", "password", ChessLibrary.Color.White, true, new List<Piece>(), 0 };
        yield return new object[] {"pseudo", "password", ChessLibrary.Color.White, false, new List<Piece>(), 0 };
        
        yield return new object[] {"pseudo", "password", ChessLibrary.Color.Black, true, new List<Piece>(), 0 };
        yield return new object[] {"pseudo", "password", ChessLibrary.Color.Black, false, new List<Piece>(), 0 };
        
        yield return new object[] {"pseudo", null, ChessLibrary.Color.White, true, new List<Piece>(), 0 };
        yield return new object[] {"pseudo", null, ChessLibrary.Color.White, false, new List<Piece>(), 0 };
        
        yield return new object[] {"pseudo", null, ChessLibrary.Color.Black, true, new List<Piece>(), 0 };
        yield return new object[] {"pseudo", null, ChessLibrary.Color.Black, false, new List<Piece>(), 0 };
    }

    public static IEnumerable<object[]> InvalidBoolConnected()
    {
        yield return new object[] {"pseudo", "password", ChessLibrary.Color.White, null, new List<Piece>(), 0};
        yield return new object[] {"pseudo", "password", ChessLibrary.Color.Black, null, new List<Piece>(), 0};
    }

    public static IEnumerable<object[]> ValidListPieces()
    {
        var piecesRempli = new List<Piece> { new Rook(ChessLibrary.Color.Black, 1), new Queen(ChessLibrary.Color.Black, 1), new Pawn(ChessLibrary.Color.Black, 1), new Knight(ChessLibrary.Color.Black, 1), new King(ChessLibrary.Color.Black, 1) };


        yield return new object[] {"pseudo", "password", ChessLibrary.Color.White, true, new List<Piece>(), 0};
        yield return new object[] {"pseudo", "password", ChessLibrary.Color.White, false,  new List<Piece>(), 0};

        yield return new object[] { "pseudo", "password", ChessLibrary.Color.Black, true, new List<Piece>(), 0};
        yield return new object[] { "pseudo", "password", ChessLibrary.Color.Black, false, new List<Piece>(), 0};
        
        yield return new object[] {"pseudo", "password", ChessLibrary.Color.White, true, piecesRempli, 0};
        yield return new object[] {"pseudo", "password", ChessLibrary.Color.White, false,  piecesRempli, 0};

        yield return new object[] { "pseudo", "password", ChessLibrary.Color.Black, true, piecesRempli, 0};
        yield return new object[] { "pseudo", "password", ChessLibrary.Color.Black, false, piecesRempli, 0};
    }

    public static IEnumerable<object[]> InvalidListPieces()
    {
        yield return new object[] { "pseudo", "password", ChessLibrary.Color.White, true, null, 0 };
        yield return new object[] { "pseudo", "password", ChessLibrary.Color.White, false, null, 0 };

        yield return new object[] { "pseudo", "password", ChessLibrary.Color.Black, true, null, 0 };
        yield return new object[] { "pseudo", "password", ChessLibrary.Color.Black, false, null, 0 };


        yield return new object[] { "pseudo", "password", ChessLibrary.Color.White, true, "null", 0 };
        yield return new object[] { "pseudo", "password", ChessLibrary.Color.White, false, "null", 0 };

        yield return new object[] { "pseudo", "password", ChessLibrary.Color.Black, true, "null", 0 };
        yield return new object[] { "pseudo", "password", ChessLibrary.Color.Black, false, "null", 0 };
        
        yield return new object[] { "pseudo", "password", ChessLibrary.Color.White, true, 0, 0 };
        yield return new object[] { "pseudo", "password", ChessLibrary.Color.White, false, 0, 0 };

        yield return new object[] { "pseudo", "password", ChessLibrary.Color.Black, true, 0, 0 };
        yield return new object[] { "pseudo", "password", ChessLibrary.Color.Black, false, 0, 0 };
    }

    public static IEnumerable<object[]> ValidUserScore()
    {
        yield return new object[] {"pseudo", "password", ChessLibrary.Color.White, true, new List<Piece>(), 0};
        yield return new object[] {"pseudo", "password", ChessLibrary.Color.White, true, new List<Piece>(), 0};

        yield return new object[] { "pseudo", "password", ChessLibrary.Color.White, false, new List<Piece>(), 1};
        yield return new object[] { "pseudo", "password", ChessLibrary.Color.White, false, new List<Piece>(), 1};

        yield return new object[] { "pseudo", "password", ChessLibrary.Color.Black, true, new List<Piece>(), 0};
        yield return new object[] { "pseudo", "password", ChessLibrary.Color.Black, true, new List<Piece>(), 0};

        yield return new object[] { "pseudo", "password", ChessLibrary.Color.Black, false, new List<Piece>(), 1};
        yield return new object[] { "pseudo", "password", ChessLibrary.Color.Black, false, new List<Piece>(), 1};
    }

    public static IEnumerable<object[]> InvalidUserScore()
    {
        yield return new object[] {"pseudo", "password", ChessLibrary.Color.White, true, new List<Piece>(), null};
        yield return new object[] {"pseudo", "password", ChessLibrary.Color.White, true, new List<Piece>(), null};

        yield return new object[] {"pseudo", "password", ChessLibrary.Color.White, false, new List<Piece>(), null};
        yield return new object[] {"pseudo", "password", ChessLibrary.Color.White, false, new List<Piece>(), null};

        yield return new object[] { "pseudo", "password", ChessLibrary.Color.Black, true, new List<Piece>(), null};
        yield return new object[] { "pseudo", "password", ChessLibrary.Color.Black, true, new List<Piece>(), null};

        yield return new object[] { "pseudo", "password", ChessLibrary.Color.Black, false, new List<Piece>(), null};
        yield return new object[] { "pseudo", "password", ChessLibrary.Color.Black , false, new List<Piece>(), null};

        yield return new object[] { "pseudo", "password", ChessLibrary.Color.White, true, new List<Piece>(), ""};
        yield return new object[] { "pseudo", "password", ChessLibrary.Color.White, true, new List<Piece>(), ""};

        yield return new object[] { "pseudo", "password", ChessLibrary.Color.White, false, new List<Piece>(), ""};
        yield return new object[] { "pseudo", "password", ChessLibrary.Color.White, false, new List<Piece>(), ""};

        yield return new object[] { "pseudo", "password", ChessLibrary.Color.Black, true, new List<Piece>(), ""};
        yield return new object[] { "pseudo", "password", ChessLibrary.Color.Black, true, new List<Piece>(), ""};

        yield return new object[] { "pseudo", "password", ChessLibrary.Color.Black, false, new List<Piece>(), ""};
        yield return new object[] { "pseudo", "password", ChessLibrary.Color.Black, false, new List<Piece>(), ""};
    }


}