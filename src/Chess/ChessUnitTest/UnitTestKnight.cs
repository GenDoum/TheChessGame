namespace ChessUnitTest;
using ChessLibrary;

public class UnitTestKnight
{
    [Theory]
    [MemberData(nameof(TestData.ValidKnightPositionsData), MemberType = typeof(TestData))]
    public void CanMove_ValidMove_ReturnsTrue(int x1, int y1, int x2, int y2)
    {
        // Arrange
        var knight = new Knight(Color.White, 1);

        // Act
        var result = knight.canMove(x1, y1, x2, y2);

        // Assert
        Assert.True(result);
    }

    [Theory]
    [MemberData(nameof(TestData.InvalidKnightPositionsData), MemberType = typeof(TestData))]
    public void CanMove_InvalidMove_ThrowsException(int x1, int y1, int x2, int y2)
    {
        // Arrange
        var knight = new Knight(Color.White, 1);

        // Act & Assert
        Assert.Throws<InvalidOperationException>(() => knight.canMove(x1, y1, x2, y2));
    }
}