namespace ChessUnitTest;
using ChessLibrary;
using Xunit;

public class UnitTestRook
{
    public static IEnumerable<object[]> ConstructorData()
    {
        yield return new object[]
        {
            true,
            new Rook[]
            {
                new Rook(Color.White, 1),
                new Rook(Color.Black, 1),
            }
        };
    }

    [Theory]
    [MemberData(nameof(ConstructorData))]
    public void TestConstructor(bool isValid, IEnumerable<Rook> rooks)
    {
        bool result = rooks.All(r => r.Color == Color.White || r.Color == Color.Black);
        Assert.Equal(isValid, result);
    }
    
    [Theory]
    [MemberData(nameof(TestData.ValidRookPositionsData), MemberType = typeof(TestData))]
    public void CanMove_ValidMove_ReturnsTrue(int x1, int y1, int x2, int y2)
    {
        // Arrange
        var rook = new Rook(Color.White, 1);
        
        // Act
        var result = rook.canMove(x1, y1, x2, y2);
        
        // Assert
        Assert.True(result);
    }

    [Theory]
    [MemberData(nameof(TestData.InvalidRookPositionsData), MemberType = typeof(TestData))]
    public void CanMove_InvalidMove_ThrowsException(int x1, int y1, int x2, int y2)
    {
        // Arrange
        var rook = new Rook(Color.White, 1);
        
        // Act & Assert
        Assert.Throws<InvalidOperationException>(() => rook.canMove(x1, y1, x2, y2));
    }
    
    [Fact]
    public void PossibleMoves_EmptyBoard_ReturnsCorrectMoves()
    {
        // Arrange
        var rook = new Rook(Color.White, 1);
        var chessboard = new Chessboard(new Case[8, 8],true);
        var caseInitial = new Case(4, 4, rook);

        // Act
        var result = rook.PossibleMoves(caseInitial, chessboard);

        // Assert
        Assert.Equal(14, result.Count);
    }
    
    [Fact]
    public void PossibleMoves_ChessboardIsNull_ThrowsArgumentNullException()
    {
        // Arrange
        var rook = new Rook(Color.White, 1);
        Chessboard chessboard = null;

        // Act & Assert
        Assert.Throws<ArgumentNullException>(() => rook.PossibleMoves(new Case(0, 0, rook), chessboard));
    }

    [Fact]
    public void PossibleMoves_PotentialCaseNotEmptyAndDifferentColor_AddsToResult()
    {
        // Arrange
        var rook = new Rook(Color.White, 1);
        var chessboard = new Chessboard(new Case[8, 8], true);
        var caseInitial = new Case(4, 4, rook);
        chessboard.Board[5, 4] = new Case(5, 4, new Rook(Color.Black, 2)); // Potential case with a piece of different color

        // Act
        var result = rook.PossibleMoves(caseInitial, chessboard);

        // Assert
        Assert.Contains(chessboard.Board[5, 4], result);
    }

    [Fact]
    public void PossibleMoves_PotentialCaseIsEmpty_AddsToResult()
    {
        // Arrange
        var rook = new Rook(Color.White, 1);
        var chessboard = new Chessboard(new Case[8, 8], true);
        var caseInitial = new Case(4, 4, rook);
        chessboard.Board[5, 4] = new Case(5, 4, null); // Potential case is empty

        // Act
        var result = rook.PossibleMoves(caseInitial, chessboard);

        // Assert
        Assert.Contains(chessboard.Board[5, 4], result);
    }
    
    [Fact]
    public void CaseIsFree_WhenPieceIsNull_ReturnsTrue()
    {
        // Arrange
        var rook = new Rook(Color.White, 1);
        var caseForRook = new Case(0, 0, null);

        // Act
        bool result = caseForRook.IsCaseEmpty();

        // Assert
        Assert.True(result);
    }

    [Fact]
    public void CaseIsFree_WhenPieceIsNotNull_ReturnsFalse()
    {
        // Arrange
        var rook = new Rook(Color.White, 1);
        var caseForRook = new Case(0, 0, rook);

        // Act
        bool result = caseForRook.IsCaseEmpty();

        // Assert
        Assert.False(result);
    }
}