namespace ChessUnitTest;
using ChessLibrary;

public class UnitTestPieces
{
    [Theory]
    [MemberData(nameof(TestData.ValidBishopPositionsData), MemberType = typeof(TestData))]
    public void CanMove_ValidMove_ReturnsTrue(int x1, int y1, int x2, int y2) 
    {
        // Arrange
        var bishop = new Bishop(Color.White, 1);
        
        // Act
        var result = bishop.canMove(x1, y1, x2, y2);
    
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
        Assert.Throws<InvalidOperationException>(() => bishop.canMove(x1, y1, x2, y2));
    }
}