namespace ChessUnitTest;
using ChessLibrary;

public class UnitTestQueen
{
    [Theory]
    [MemberData(nameof(TestData.ValidQueenPositionsData), MemberType = typeof(TestData))]
    public void CanMove_ValidMove_ReturnsTrue(int x1, int y1, int x2, int y2)
    {
        // Arrange
        var queen = new Queen(Color.White, 1);
        
        // Act
        var result = queen.CanMove(x1, y1, x2, y2);
        
        // Assert
        Assert.True(result);
    }

/*    [Theory]
    [MemberData(nameof(TestData.InvalidQueenPositionsData), MemberType = typeof(TestData))]
    public void CanMove_InvalidMove_ThrowsException(int x1, int y1, int x2, int y2)
    {
        // Arrange
        var queen = new Queen(Color.White, 1);
        
        // Act & Assert
        Assert.Throws<InvalidOperationException>(() => queen.CanMove(x1, y1, x2, y2));
    }*/
    
    [Fact]
    public void PossibleMoves_EmptyBoard_ReturnsCorrectMoves()
    {
        // Arrange
        var queen = new Queen(Color.White, 1);
        var chessboard = new Chessboard(new Case[8, 8],true);
        var caseInitial = new Case(4, 4, queen);

        // Act
        var result = queen.PossibleMoves(caseInitial, chessboard);

        // Assert
        Assert.Equal(27, result.Count);
    }
    
    [Fact]
    public void PossibleMoves_BoardWithPieces_ReturnsCorrectMoves()
    {
        // Arrange
        var queen = new Queen(Color.White, 1);
        var chessboard = new Chessboard(new Case[8, 8], true);
        var caseInitial = new Case(4, 4, queen);
        chessboard.Board[5, 5].Piece = new Pawn(Color.Black, 2); 

        // Act
        var result = queen.PossibleMoves(caseInitial, chessboard);

        // Assert
        Assert.Contains(chessboard.Board[5, 5], result);
        Assert.DoesNotContain(chessboard.Board[6, 6], result);
    }
}