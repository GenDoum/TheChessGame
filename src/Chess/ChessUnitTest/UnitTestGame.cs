using Persistance;

namespace ChessUnitTest;

using ChessLibrary;

public class UnitTestGame
{
    [Fact]
    public void TestGameConstructorAndCheckMethods()
    {
        // Arrange
        User player1 = new User("Player1", "password", Color.White, true, 0);
        User player2 = new User("Player2", "password", Color.Black, true, 0);
        IUserDataManager userDataManager = new LoaderJson(); // Créez une instance de LoaderJson

        Game game = new Game(player1, player2, userDataManager);

        // Act & Assert
        // Check that the game was properly initialized
        Assert.False(game.WhiteCheck);
        Assert.False(game.BlackCheck);
        Assert.Equal(player1, game.Player1);
        Assert.Equal(player2, game.Player2);
        Assert.NotNull(game.Board);

    }
}