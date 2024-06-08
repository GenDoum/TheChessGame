namespace ChessUnitTest;
using ChessLibrary;
using System.Collections.ObjectModel;

public class UnitTestManager
{
    [Fact]
    public void Games_GetterInitializesCollectionIfNull()
    {
        var Manager = new Manager();
        // Arrange
        var game = new Game();

        // Act
        var gamesCollection = Manager.Games;
        var UserCollection = Manager.Users;
        var ChessboardCollection = Manager.Chessboards;
        // Assert
        Assert.NotNull(gamesCollection);
        Assert.IsType<ObservableCollection<Game>>(gamesCollection);
        Assert.Empty(gamesCollection);
        Assert.NotNull(UserCollection);
        Assert.IsType<ObservableCollection<User>>(UserCollection);
        Assert.Empty(UserCollection);
        Assert.NotNull(ChessboardCollection);
        Assert.IsType<ObservableCollection<Chessboard>>(ChessboardCollection);
        Assert.Empty(ChessboardCollection);
    }

    [Fact]
    public void Games_SetterAssignsNewCollectionAndRaisesPropertyChanged()
    {
        var Manager = new Manager();
        // Arrange
        var game = new Game();
        var newGamesCollection = new ObservableCollection<Game>();


        // Act
        Manager.Games = newGamesCollection;

        // Assert
        Assert.Equal(newGamesCollection, Manager.Games);

    }
}

