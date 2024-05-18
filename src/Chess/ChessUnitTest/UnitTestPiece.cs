namespace ChessUnitTest;
using ChessLibrary;

public class UnitTestPiece
{
    [Fact]
    public void Constructor_ValidColor_SetsPropertiesCorrectly()
    {
        // Arrange
        var color = Color.White;
        var id = 1;

        // Act
        var piece = new Rook(color, id);

        // Assert
        Assert.Equal(color, piece.Color);
        Assert.Equal(id, piece.Id);
    }

    [Fact]
    public void Constructor_InvalidColor_ThrowsArgumentException()
    {
        // Arrange
        var color = (Color)999; // Invalid color
        var id = 1;

        // Act & Assert
        Assert.Throws<ArgumentException>(() => new Rook(color, id));
    }

}

