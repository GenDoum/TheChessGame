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

    [Theory]
    [MemberData(nameof(TestData.InvalidQueenPositionsData), MemberType = typeof(TestData))]
    public void CanMove_InvalidMove_ThrowsException(int x1, int y1, int x2, int y2)
    {
        // Arrange
        var queen = new Queen(Color.White, 1);
        
        // Act & Assert
        Assert.Throws<InvalidMovementException>(() => queen.CanMove(x1, y1, x2, y2));
    }
    
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
        chessboard.Board[5, 5]!.Piece = new Pawn(Color.Black, 2); 

        // Act
        var result = queen.PossibleMoves(caseInitial, chessboard);

        // Assert
        Assert.Contains(chessboard.Board[5, 5], result);
        Assert.DoesNotContain(chessboard.Board[6, 6], result);
    }


    [Fact]
    public void PossibleMoves_BaordWithPiecesAndKing_ReturnsCorrectMoves()
    {
        // Arrange
        Chessboard chessboard = new Chessboard(new Case[8, 8], true);
        King king = new King(Color.White, 1);
        Queen queen = new Queen(Color.Black, 1);
        Pawn pawn = new Pawn(Color.White, 2);
        Case queenCase = new Case(4, 2, queen);
        chessboard.AddPiece(king, 4, 0);
        chessboard.AddPiece(queen, 4, 2);
        chessboard.AddPiece(pawn, 3, 1);

        // Act
        var result = queen.PossibleMoves(queenCase, chessboard);

        // Assert

        Assert.Contains(chessboard.Board[4, 1], result);
        Assert.Contains(chessboard.Board[3, 1], result);
        Assert.Contains(chessboard.Board[5, 1], result);
        Assert.Contains(chessboard.Board[4, 0], result);
        Assert.Contains(chessboard.Board[7, 5], result);
        Assert.Contains(chessboard.Board[0, 6], result);
        Assert.DoesNotContain(chessboard.Board[5, 4], result);
        Assert.DoesNotContain(chessboard.Board[0, 7], result);
        Assert.Equal(24,result.Count);
    }
}