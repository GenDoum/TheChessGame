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
}