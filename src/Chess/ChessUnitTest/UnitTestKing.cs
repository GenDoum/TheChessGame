namespace ChessUnitTest;
using ChessLibrary;

public class UnitTestKing
{
    [Theory]
    [MemberData(nameof(TestData.ValidKingPositionsData), MemberType = typeof(TestData))]
    public void CanMove_OneSquareAway_ReturnsTrue(int x1, int y1, int x2, int y2)
    {
        // Arrange
        var king = new King(Color.White, 1);
        
        // Act
        var result = king.CanMove(x1, y1, x2, y2);
        
        // Assert
        Assert.True(result);
    }

    [Theory]
    [MemberData(nameof(TestData.InvalidKingPositionsData), MemberType = typeof(TestData))]
    public void CanMove_TwoSquaresAway_ThrowsException(int x1, int y1, int x2, int y2)
    {
        // Arrange
        var king = new King(Color.White, 1);
        
        // Act and Assert
        Assert.Throws<InvalidMovementException>(() => king.CanMove(x1, y1, x2, y2));
    }
    
    [Fact]
    public void TestKingPossibleMoves_WithEnemyPiece()
    {
        // Arrange
        Chessboard chessboard = new Chessboard(new Case[8, 8], true);
        King king = new King(Color.White, 1);
        Case kingCase = new Case(0, 0, king);
        chessboard.AddPiece(king, 0, 0);
        Pawn enemyPawn = new Pawn(Color.Black, 2);
        chessboard.AddPiece(enemyPawn, 0, 1);

        // Act
        var possibleMoves = king.PossibleMoves(kingCase, chessboard);

        // Assert
        Assert.Contains(possibleMoves, c => c!.Column == 0 && c.Line == 1);
    }

    [Fact]
    public void TestKingPossibleMoves_WithNullChessboard()
    {
        // Arrange
        King king = new King(Color.White, 1);
        Case kingCase = new Case(0, 0, king);

        // Act & Assert
        Assert.Throws<ArgumentNullException>(() => king.PossibleMoves(kingCase, null!));
    }
    
    [Fact]
    public void KingCanCaptureOppositeColorPiece()
    {
        // Arrange
        var chessboard = new Chessboard(new Case[8, 8], true);
        var king = new King(Color.White, 1);
        var pawn = new Pawn(Color.Black, 2);
        chessboard.Board[4, 4] = new Case(4, 4, king);
        chessboard.Board[5, 5] = new Case(5, 5, pawn);

        // Act
        var result = king.PossibleMoves(chessboard.Board[4, 4], chessboard);

        // Assert
        Assert.Contains(result, c => c!.Column == 5 && c.Line == 5);
    }
    
    [Fact]
    public void CanEat_ValidMoves_ReturnsCorrectCases()
    {
        // Arrange
        var chessboard = new Chessboard(new Case[8, 8], true);
        var king = new King(Color.White, 1);
        chessboard.Board[4, 4] = new Case(4, 4, king);

        // Add some enemy pieces around the king
        chessboard.Board[3, 3] = new Case(3, 3, new Pawn(Color.Black, 2));
        chessboard.Board[4, 3] = new Case(4, 3, new Pawn(Color.Black, 3));
        chessboard.Board[5, 3] = new Case(5, 3, new Pawn(Color.Black, 4));

        // Act
        var result = king.CanEat(chessboard.Board[4, 4], chessboard);

        // Assert
        Assert.Equal(8, result.Count); // King should be able to eat in 8 directions
        Assert.Contains(result, c => c!.Column == 3 && c.Line == 3); // King should be able to eat the pawn at (3,3)
        Assert.Contains(result, c => c!.Column == 4 && c.Line == 3); // King should be able to eat the pawn at (4,3)
        Assert.Contains(result, c => c!.Column == 5 && c.Line == 3); // King should be able to eat the pawn at (5,3)
    }
}