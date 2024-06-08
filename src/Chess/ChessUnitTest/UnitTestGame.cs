using Persistance;

namespace ChessUnitTest;

using ChessLibrary;

public class UnitTestGame
{
    [Theory]
    [MemberData(nameof(TestData.InvalidBishopPositionsData), MemberType = typeof(TestData))]
    public void CanMove_InvalidMove_ThrowsException(int x1, int y1, int x2, int y2)
    {
        // Arrange
        var bishop = new Bishop(Color.White, 1);

        // Act & Assert
        Assert.Throws<InvalidMovementException>(() => bishop.CanMove(x1, y1, x2, y2));
    }

    [Fact]
    public void PossibleMoves_BoardWithOtherPieces_ReturnsCorrectMoves()
    {
        // Arrange
        var bishop = new Bishop(Color.White, 1);
        var chessboard = new Chessboard(new Case[8, 8], true);
        var caseInitial = new Case(4, 4, bishop);
        chessboard.Board[4, 4] = caseInitial;

        chessboard.Board[5, 5] = new Case(5, 5, new Pawn(Color.Black, 2));

        chessboard.Board[3, 3] = new Case(3, 3, new Pawn(Color.White, 3));

        // Act
        var result = bishop.PossibleMoves(caseInitial, chessboard);

        // Assert
        Assert.Contains(result, c => c!.Column == 5 && c.Line == 5);
        Assert.DoesNotContain(result, c => c!.Column == 2 && c.Line == 2);
    }
}