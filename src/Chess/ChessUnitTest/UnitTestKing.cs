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
        var result = king.canMove(x1, y1, x2, y2);
        
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
        Assert.Throws<InvalidOperationException>(() => king.canMove(x1, y1, x2, y2));
    }
}