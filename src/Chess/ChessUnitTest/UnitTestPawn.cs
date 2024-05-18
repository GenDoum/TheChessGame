namespace ChessUnitTest;
using ChessLibrary;

public class UnitTestPawn
{
    [Theory]
    [MemberData(nameof(TestData.ValidPawnPositionsData), MemberType = typeof(TestData))]
    public void CanMove_ValidMove_ReturnsTrue(int x1, int y1, int x2, int y2)
    {
        var pawn = new Pawn(Color.White, 1);
        var result = pawn.CanMove(x1, y1, x2, y2);
        Assert.True(result);
    }

   [Theory]
    [MemberData(nameof(TestData.InvalidPawnPositionsData), MemberType = typeof(TestData))]
    public void CanMove_InvalidMove_ThrowsException(int x1, int y1, int x2, int y2)
    {
        var pawn = new Pawn(Color.White, 1);
        Assert.Throws<InvalidMovementException>(() => pawn.CanMove(x1, y1, x2, y2));
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

    [Fact]
    public void PossibleMoves_CapturablePiece_AddsToResult()
    {
        // Arrange
        var pawn = new Pawn(Color.White, 1);
        var chessboard = new Chessboard(new Case[8, 8], true);
        var caseInitial = new Case(4, 4, pawn);
        var capturablePiece = new Pawn(Color.Black, 2);
        chessboard.AddPiece(capturablePiece, 5, 5);

        // Act
        var result = pawn.PossibleMoves(caseInitial, chessboard);

        // Assert
        Assert.Contains(result, c => c.Column == 5 && c.Line == 5);
    }

    [Fact]
    public void PossibleMoves_NullChessboard_ThrowsException()
    {
        // Arrange
        var pawn = new Pawn(Color.White, 1);
        var caseInitial = new Case(4, 4, pawn);

        // Act & Assert
        Assert.Throws<ArgumentNullException>(() => pawn.PossibleMoves(caseInitial, null));
    }

    // [Theory]
    // [InlineData(Color.White, 1, 2, 3)]
    // [InlineData(Color.Black, 6, 5, 4)]
    // public void CanMove_FirstMoveTwoSteps_ReturnsTrue(Color color, int y1, int y2, int y3)
    // {
    //     // Arrange
    //     var pawn = new Pawn(color, 1);
    //
    //     // Act
    //     var result1 = pawn.CanMove(4, y1, 4, y2);
    //     var result2 = pawn.CanMove(4, y1, 4, y3);
    //
    //     // Assert
    //     Assert.True(result1);
    //     Assert.False(result2);
    // }

    [Fact]
    public void PossibleMoves_DirectionBasedOnColor_ReturnsCorrectMoves()
    {
        // Arrange
        var whitePawn = new Pawn(Color.White, 1);
        var blackPawn = new Pawn(Color.Black, 2);
        var chessboard = new Chessboard(new Case[8, 8], true);
        var whiteCase = new Case(4, 4, whitePawn);
        var blackCase = new Case(4, 5, blackPawn);

        // Act
        var whiteMoves = whitePawn.PossibleMoves(whiteCase, chessboard);
        var blackMoves = blackPawn.PossibleMoves(blackCase, chessboard);

        // Assert
        Assert.All(whiteMoves, move => Assert.True(move.Line > 4));
        Assert.All(blackMoves, move => Assert.True(move.Line < 5));
    }
}