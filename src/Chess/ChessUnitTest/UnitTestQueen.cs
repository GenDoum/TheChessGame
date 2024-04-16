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
        var result = queen.canMove(x1, y1, x2, y2);
        
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
        Assert.Throws<InvalidOperationException>(() => queen.canMove(x1, y1, x2, y2));
    }
}