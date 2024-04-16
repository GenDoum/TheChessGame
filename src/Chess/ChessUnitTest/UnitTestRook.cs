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
}