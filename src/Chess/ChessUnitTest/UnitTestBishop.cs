namespace ChessUnitTest;
using ChessLibrary;

public class UnitTestBishop
{
    [Theory]
    [MemberData(nameof(TestData.ValidBishopPositionsData), MemberType = typeof(TestData))]
    public void CanMove_ValidMove_ReturnsTrue(int x1, int y1, int x2, int y2) 
    {
        // Arrange
        var bishop = new Bishop(Color.White, 1);
        
        // Act
        var result = bishop.CanMove(x1, y1, x2, y2);
    
        // Assert
        Assert.True(result);
    }
    
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
        Assert.Contains(result, c => c.Column == 5 && c.Line == 5);
        Assert.DoesNotContain(result, c => c.Column == 2 && c.Line == 2);
    }
    
    [Fact]
    public void PossibleMoves_NullChessboard_ThrowsException()
    {
        // Arrange
        var bishop = new Bishop(Color.White, 1);
        var caseInitial = new Case(4, 4, bishop);

        // Act & Assert
        Assert.Throws<ArgumentNullException>(() => bishop.PossibleMoves(caseInitial, null!));
    }

    [Fact]
    public void PossibleMoves_NullCaseInitial_ThrowsException()
    {
        // Arrange
        var bishop = new Bishop(Color.White, 1);
        var chessboard = new Chessboard(new Case[8, 8],true);

        // Act & Assert
        Assert.Throws<ArgumentNullException>(() => bishop.PossibleMoves(null!, chessboard));
    }
    
    [Fact]
    public void PossibleMoves_NonEmptyCase_AddsToResult()
    {
        // Arrange
        var bishop = new Bishop(Color.White, 1);
        var chessboard = new Chessboard(new Case[8, 8],true);
        var caseInitial = new Case(4, 4, bishop);
        var otherPiece = new Bishop(Color.Black, 2);
        chessboard.Board[5, 5] = new Case(5, 5, otherPiece);

        // Act
        var result = bishop.PossibleMoves(caseInitial, chessboard);

        // Assert
        Assert.Contains(chessboard.Board[5, 5], result);
    }
    
}