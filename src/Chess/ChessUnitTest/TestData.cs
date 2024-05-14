using ChessLibrary;
using System.Drawing;
using System.Net.NetworkInformation;

namespace ChessUnitTest;

public class TestData
{
    public static IEnumerable<object[]> ValidBishopPositionsData()
    {
        yield return new object[] { 0, 0, 1, 1 };
        yield return new object[] { 2, 2, 0, 4 };
        yield return new object[] { 2, 2, 3, 3 };
        yield return new object[] { 2, 2, 4, 0 };

        yield return new object[] { 1, 1, 4, 4 }; // Long diagonal move
        yield return new object[] { 5, 5, 1, 1 }; // Long diagonal move across the board
    }

    public static IEnumerable<object[]> InvalidBishopPositionsData()
    {
        yield return new object[] { 0, 0, 0, 1 }; // Vertical move
        yield return new object[] { 2, 2, 4, 2 }; // Non-diagonal move

        yield return new object[] { 3, 3, 3, 4 }; // Horizontal move
        yield return new object[] { 3, 3, 5, 2 }; // Jump over pieces (assuming pieces are blocking)
        yield return new object[] { 7, 7, 8, 8 }; // Move out of bounds
    }

    public static IEnumerable<object[]> ValidKingPositionsData()
    {
        yield return new object[] { 0, 0, 0, 1 }; // Move up one square
        yield return new object[] { 2, 2, 3, 2 }; // Move one square to the right
        yield return new object[] { 7, 7, 6, 6 }; // Move diagonally down-left

        yield return new object[] { 0, 0, 1, 0 }; // Move right one square
        yield return new object[] { 7, 7, 7, 6 }; // Move down one square
    }

    public static IEnumerable<object[]> InvalidKingPositionsData()
    {
        yield return new object[] { 0, 0, 2, 2 }; // Move two squares
        yield return new object[] { 2, 2, 0, 4 }; // Move more than one square diagonally

        // Invalid King Moves
        yield return new object[] { 4, 4, 4, 7 }; // Move three squares down
        yield return new object[] { 1, 1, 1, 4 }; // Move three squares right

        // King moves near the edges
        yield return new object[] { 0, 0, 0, -1 }; // Move outside the board (up)
        yield return new object[] { 7, 7, 7, 8 }; // Move outside the board (down)
        yield return new object[] { 0, 0, -1, 0 }; // Move outside the board (left)
        yield return new object[] { 7, 7, 8, 7 }; // Move outside the board (right)
    }

    public static IEnumerable<object[]> ValidRookPositionsData()
    {
        yield return new object[] { 0, 0, 0, 1 }; // Move up one square
        yield return new object[] { 2, 2, 2, 3 }; // Move one square to the right

        yield return new object[] { 3, 3, 3, 7 }; // Move to the end of the row
        yield return new object[] { 2, 2, 7, 2 }; // Move down to the bottom of the column

        yield return new object[] { 7, 7, 7, 0 }; // Move to the top of the column
        yield return new object[] { 0, 7, 7, 7 }; // Move to the bottom of the column
    }

    public static IEnumerable<object[]> InvalidRookPositionsData()
    {
        yield return new object[] { 0, 0, 1, 1 }; // Diagonal move
        yield return new object[] { 2, 2, 3, 4 }; // Non-linear move

        yield return new object[] { 1, 1, 2, 2 }; // Diagonal move
        yield return new object[] { 6, 6, 4, 5 }; // Non-linear move

        yield return new object[] { 0, 0, -1, 0 }; // Move outside the board (left)
        yield return new object[] { 0, 0, 0, -1 }; // Move outside the board (up)
        yield return new object[] { 7, 7, 8, 7 }; // Move outside the board (right)
        yield return new object[] { 7, 7, 7, 8 }; // Move outside the board (down)
    }

    public static IEnumerable<object[]> ValidQueenPositionsData()
    {
        yield return new object[] { 0, 0, 0, 4 }; // Horizontal move
        yield return new object[] { 0, 0, 4, 0 }; // Vertical move
        yield return new object[] { 0, 0, 2, 2 }; // Diagonal move

        yield return new object[] { 4, 4, 4, 0 }; // Move horizontally across the board
        yield return new object[] { 0, 0, 7, 7 }; // Move diagonally across the board
    }

    public static IEnumerable<object[]> InvalidQueenPositionsData()
    {
        yield return new object[] { 0, 0, 2, 3 }; // Non-linear move

        yield return new object[] { 1, 1, 3, 4 }; // Irregular move
        yield return new object[] { 2, 2, 4, 5 }; // Irregular move
    }

    public static IEnumerable<object[]> ValidKnightPositionsData()
    {
        yield return new object[] { 0, 0, 1, 2 }; // L-shaped move
        yield return new object[] { 2, 2, 0, 1 }; // L-shaped move
        yield return new object[] { 4, 4, 5, 6 }; // L-shaped move
        yield return new object[] { 1, 1, 2, 3 }; // L-shaped move

        // Additional valid knight moves
        yield return new object[] { 7, 7, 5, 6 }; // L-shaped move near the edge (up-right)
        yield return new object[] { 0, 0, 2, 1 }; // L-shaped move near the edge (down-left)
    }

    public static IEnumerable<object[]> InvalidKnightPositionsData()
    {
        yield return new object[] { 0, 0, 0, 1 }; // Vertical move
        yield return new object[] { 2, 2, 3, 3 }; // Diagonal move
        yield return new object[] { 3, 3, 3, 5 }; // Horizontal move
        yield return new object[] { 2, 2, 4, 4 }; // Diagonal move

        // Invalid knight moves near the edges
        yield return new object[] { 0, 0, 2, -1 }; // Move outside the board (up)
        yield return new object[] { 7, 7, 5, 8 }; // Move outside the board (down)
        yield return new object[] { 0, 0, -1, 2 }; // Move outside the board (left)
        yield return new object[] { 7, 7, 8, 5 }; // Move outside the board (right)
    }

    public static IEnumerable<object[]> ValidPawnPositionsData()
    {
        yield return new object[] { 0, 0, 0, 1 }; // Move up one square
        yield return new object[] { 4, 4, 4, 5 }; // Move forward one square
        yield return new object[] { 5, 1, 6, 2 }; // Diagonal capture

        // Additional valid pawn moves
        yield return new object[] { 3, 5, 3, 6 }; // Move up one square (black pawn)
        yield return new object[] { 0, 5, 0, 6 }; // Move up one square (white pawn, promotion)
    }

    public static IEnumerable<object[]> InvalidPawnPositionsData()
    {
        yield return new object[] { 0, 0, 0, 3 }; // Move two squares (valid initial move)
        yield return new object[] { 0, 0, 1, 0 }; // Move sideways

        yield return new object[] { 3, 3, 3, 5 }; // Move two squares not from initial position

        yield return new object[] { 0, 0, 0, -1 }; // Move outside the board (up)
        yield return new object[] { 7, 7, 7, 8 }; // Move outside the board (down)
        yield return new object[] { 0, 0, -1, 0 }; // Move outside the board (left)
        yield return new object[] { 7, 7, 8, 7 }; // Move outside the board (right)
    }

    public static IEnumerable<object[]> ValidUserPseudo()
    {
        yield return new object[] { "pseudo", "password", ChessLibrary.Color.White }; // Good Pseudo with Color.White
        yield return new object[] { "pseudo", "password", ChessLibrary.Color.Black }; // Good Pseudo with Color.Black

        yield return new object[] { "pseudo", "password", ChessLibrary.Color.White }; // Good Pseudo with password null and Color.White
        yield return new object[] { "pseudo", "password", ChessLibrary.Color.Black }; // Good Pseudo with password null and Color.Black
    }

    public static IEnumerable<object[]> InvalidUserPseudo()
    {
        yield return new object[] { "", "password", ChessLibrary.Color.White }; // Empty pseudo with Color.White
        yield return new object[] { "", "password", ChessLibrary.Color.Black }; // Empty pseudo with Color.Black

        yield return new object[] { " ", "password", ChessLibrary.Color.White }; // Pseudo with white space and Color.White
        yield return new object[] { " ", "password", ChessLibrary.Color.Black }; // Pseudo with white space and Color.Black

        yield return new object[] { null, "password", ChessLibrary.Color.White }; // Pseudo null with Color.White
        yield return new object[] { null, "password", ChessLibrary.Color.Black }; // Pseudo null with Color.Black
    }

    public static IEnumerable<object[]> ValidUserPassword()
    {
        yield return new object[] { "pseudo", "password", ChessLibrary.Color.White }; // Good password with Color.White
        yield return new object[] { "pseudo", "password", ChessLibrary.Color.Black }; // Good password with Color.Black
        
        yield return new object[] { "pseudo", null, ChessLibrary.Color.White }; // Good password with Color.White
        yield return new object[] { "pseudo", null, ChessLibrary.Color.Black }; // Good password with Color.Black
    }

    public static IEnumerable<object[]> InvalidUserPassword()
    {
        yield return new object[] { "pseudo", "", ChessLibrary.Color.White }; // Empty password qui Color.White
        yield return new object[] { "pseudo", "", ChessLibrary.Color.Black }; // Empty password qui Color.Black

    }

    public static IEnumerable<object[]> ValidUserColor()
    {
        yield return new object[] { "pseudo", "password", ChessLibrary.Color.White }; // Good User with Color.White
        yield return new object[] { "pseudo", "password", ChessLibrary.Color.Black }; // Good User with Color.Black

        yield return new object[] { "pseudo", null, ChessLibrary.Color.White }; // Good User with null password and Color.White
        yield return new object[] { "pseudo", null, ChessLibrary.Color.Black }; // Good User with null password and Color.Black
    }
    
    public static IEnumerable<object[]> InvalidUserColor() 
    {
        yield return new object[] { "pseudo", "password", null }; // Good User with color null
        yield return new object[] { "pseudo", null, null }; // Good User with wrong color null
        //yield return new object[] { "pseudo", null, ChessLibrary.Color.Red }; // Good User with wrong color
    }

    public static IEnumerable<object[]> InvalidIsMoveValid()
    {
        yield return new object[] {  }; 
        yield return new object[] { "pseudo", null, null }; 
       
    }

    public static IEnumerable<object[]> ValidIsMoveValidData()
    {
        var chessboard = new Chessboard(new Case[8, 8], true); // Initialisation d'un échiquier vide pour les tests contrôlés

        // Ajout d'un fou blanc en position (2,0) et vérification d'un mouvement diagonal valide
        chessboard.Board[2, 0] = new Case(2, 0, new Bishop(ChessLibrary.Color.White, 1));
        yield return new object[] { chessboard, new List<Case> { new Case(3, 1, null) }, new Case(3, 1, null), true };

        // Ajout d'une tour noire en position (5,7) et vérification d'un mouvement vertical valide
        chessboard.Board[5, 7] = new Case(5, 7, new Rook(ChessLibrary.Color.Black, 2));
        yield return new object[] { chessboard, new List<Case> { new Case(5, 5, null) }, new Case(5, 5, null), true };

        // Ajout d'un cavalier blanc et vérification d'un mouvement en "L" valide
        chessboard.Board[1, 1] = new Case(1, 1, new Knight(ChessLibrary.Color.White, 3));
        yield return new object[] { chessboard, new List<Case> { new Case(2, 3, null) }, new Case(2, 3, null), true };
    }

    public static IEnumerable<object[]> InvalidIsMoveValidData()
    {
        var chessboard = new Chessboard(new Case[8, 8], true); // Initialisation d'un échiquier vide pour les tests contrôlés

        // Fou faisant un mouvement horizontal invalide
        chessboard.Board[4, 4] = new Case(4, 4, new Bishop(ChessLibrary.Color.White, 4));
        yield return new object[] { chessboard, new List<Case> { new Case(5, 4, null) }, new Case(5, 4, null), false };

        // Tour effectuant un mouvement diagonal
        chessboard.Board[7, 7] = new Case(7, 7, new Rook(ChessLibrary.Color.Black, 5));
        yield return new object[] { chessboard, new List<Case> { new Case(6, 6, null) }, new Case(6, 6, null), false };

        // Cavalier essayant de se déplacer hors du plateau
        chessboard.Board[0, 0] = new Case(0, 0, new Knight(ChessLibrary.Color.White, 6));
        yield return new object[] { chessboard, new List<Case> { new Case(-1, 2, null) }, new Case(-1, 2, null), false };
    }

}