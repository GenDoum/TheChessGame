namespace ChessUnitTest;
using ChessLibrary;

public class UnitTestPawn
{
    [Theory]
    [MemberData(nameof(TestData.ValidPawnPositionsData), MemberType = typeof(TestData))]
    public void CanMove_ValidMove_ReturnsTrue(int x1, int y1, int x2, int y2)
    {
        var pawn = new Pawn(Color.White, 1);
        var result = pawn.canMove(x1, y1, x2, y2);
        Assert.True(result);
    }

    [Theory]
    [MemberData(nameof(TestData.InvalidPawnPositionsData), MemberType = typeof(TestData))]
    public void CanMove_InvalidMove_ThrowsException(int x1, int y1, int x2, int y2)
    {
        var pawn = new Pawn(Color.White, 1);
        Assert.Throws<InvalidOperationException>(() => pawn.canMove(x1, y1, x2, y2));
    }
    
    [Fact]
    public void PossibleMoves_EmptyBoard_ReturnsCorrectMoves()
    {
        // Arrange
        var pawn = new Pawn(Color.White, 1);
        var chessboard = new Chessboard(new Case[8, 8],true);
        var caseInitial = new Case(4, 4, pawn);

        // Act
        var result = pawn.PossibleMoves(caseInitial, chessboard);

        // Assert
        Assert.Equal(2, result.Count);
    }
}