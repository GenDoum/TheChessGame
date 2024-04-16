using ChessLibrary;

namespace ChessUnitTest
{
    public class Tests
    {
        [Fact]
        public void CanMove_ValidMove_ReturnsTrue()
        {
            // Arrange
            var bishop = new Bishop(Color.White, 1);

            // Act
            var result = bishop.canMove(1, 1, 3, 3);

            // Assert
            Assert.True(result);
        }

        [Fact]
        public void CanMove_InvalidMove_ThrowsException()
        {
            // Arrange
            var bishop = new Bishop(Color.White, 1);

            // Act & Assert
            Assert.Throws<InvalidOperationException>(() => bishop.canMove(1, 1, 2, 3));
        }
        
        [Fact]
        public void CanMove_OneSquareAway_ReturnsTrue()
        {
            var king = new King(Color.White, 1);
            var result = king.canMove(1, 1, 2, 2);
            Assert.True(result);
        }

        [Fact]
        public void CanMove_SameSquare_ReturnsTrue()
        {
            var king = new King(Color.White, 1);
            var result = king.canMove(1, 1, 1, 1);
            Assert.True(result);
        }

        [Fact]
        public void CanMove_TwoSquaresAway_ThrowsException()
        {
            var king = new King(Color.White, 1);
            Assert.Throws<InvalidOperationException>(() => king.canMove(1, 1, 3, 3));
        }

        [Fact]
        public void CanMove_DiagonallyMoreThanOneSquare_ThrowsException()
        {
            var king = new King(Color.White, 1);
            Assert.Throws<InvalidOperationException>(() => king.canMove(1, 1, 3, 3));
        }
        using Xunit;
using ChessLibrary; // Assumez que c'est le namespace d'après le code fourni.

public class BishopTests
{
    [Fact]
    public void PossibleMoves_FromCenter_ReturnsAllDiagonalMoves()
    {
        // Initialisation
        var board = new Case[8, 8];
        for (int i = 0; i < 8; i++)
        {
            for (int j = 0; j < 8; j++)
            {
                board[i, j] = new Case(i, j); // Supposons que le constructeur initialise une case vide.
            }
        }
        var chessboard = new Chessboard(board);
        var bishop = new Bishop(Color.White, 1); // Supposons l'id '1' pour le fou.
        var centerCase = board[4, 4];
        board[4, 4].Piece = bishop; // Place le fou au centre.

        // Action
        var possibleMoves = bishop.PossibleMoves(centerCase, chessboard);

        // Vérification
        Assert.Contains(board[3, 3], possibleMoves);
        Assert.Contains(board[5, 5], possibleMoves);
        Assert.Contains(board[2, 6], possibleMoves);
        Assert.Contains(board[6, 2], possibleMoves);
        Assert.DoesNotContain(board[4, 4], possibleMoves); // La position actuelle du fou ne doit pas être incluse.
    }

    [Fact]
    public void PossibleMoves_EdgeCase_DoesNotIncludeOutOfBoundsMoves()
    {
        // Initialisation
        var board = new Case[8, 8];
        for (int i = 0; i < 8; i++)
        {
            for (int j = 0; j < 8; j++)
            {
                board[i, j] = new Case(i, j);
            }
        }
        var chessboard = new Chessboard(board);
        var bishop = new Bishop(Color.White, 2);
        var edgeCase = board[7, 7];
        board[7, 7].Piece = bishop;

        // Action
        var possibleMoves = bishop.PossibleMoves(edgeCase, chessboard);

        // Vérification
        Assert.DoesNotContain(board[8, 8], possibleMoves); // Les mouvements hors limites ne doivent pas être inclus.
    }

    [Fact]
    public void PossibleMoves_WithObstacles_StopsAtObstacles()
    {
        // Initialisation
        var board = new Case[8, 8];
        for (int i = 0; i < 8; i++)
        {
            for (int j = 0; j < 8; j++)
            {
                board[i, j] = new Case(i, j);
            }
        }
        var chessboard = new Chessboard(board);
        var bishop = new Bishop(Color.White, 3);
        var startCase = board[4, 4];
        board[4, 4].Piece = bishop;
        board[6, 6].Piece = new Pawn(Color.Black, 4); // Obstacle au 6, 6.

        // Action
        var possibleMoves = bishop.PossibleMoves(startCase, chessboard);

        // Vérification
        Assert.Contains(board[6, 6], possibleMoves); // Le fou peut capturer au 6, 6.
        Assert.DoesNotContain(board[7, 7], possibleMoves); // Les mouvements au-delà du pion ne sont pas inclus.
    }
}

    }
}