using ChessLibrary;

namespace ChessUnitTest
{
    public class Tests
    {
        [Fact]
        public void CanMove_ValidMove_ReturnsTrue()
        {
            // Arrange
            var bishop = new Bishop(Color.White, 1);

            // Act
            var result = bishop.canMove(1, 1, 3, 3);

            // Assert
            Assert.True(result);
        }

        [Fact]
        public void CanMove_InvalidMove_ThrowsException()
        {
            // Arrange
            var bishop = new Bishop(Color.White, 1);

            // Act & Assert
            Assert.Throws<InvalidOperationException>(() => bishop.canMove(1, 1, 2, 3));
        }
        
        [Fact]
        public void CanMove_OneSquareAway_ReturnsTrue()
        {
            var king = new King(Color.White, 1);
            var result = king.canMove(1, 1, 2, 2);
            Assert.True(result);
        }

        [Fact]
        public void CanMove_SameSquare_ReturnsTrue()
        {
            var king = new King(Color.White, 1);
            var result = king.canMove(1, 1, 1, 1);
            Assert.True(result);
        }

        [Fact]
        public void CanMove_TwoSquaresAway_ThrowsException()
        {
            var king = new King(Color.White, 1);
            Assert.Throws<InvalidOperationException>(() => king.canMove(1, 1, 3, 3));
        }

        [Fact]
        public void CanMove_DiagonallyMoreThanOneSquare_ThrowsException()
        {
            var king = new King(Color.White, 1);
            Assert.Throws<InvalidOperationException>(() => king.canMove(1, 1, 3, 3));
        }
    }
}