namespace ChessUnitTest;
using ChessLibrary.Events;
using ChessLibrary;

public class UnitTestEvent
{
    [Fact]
    public void TestEvolveNotifiedEventArgs()
    {
        // Arrange
        var pawn = new Pawn(Color.White, 1);
        var caseForPawn = new Case(0, 0, pawn);

        // Act
        var args = new EvolveNotifiedEventArgs { Pawn = pawn, Case = caseForPawn };

        // Assert
        Assert.Equal(pawn, args.Pawn);
        Assert.Equal(caseForPawn, args.Case);
    }

    [Fact]
    public void TestGameOverNotifiedEventArgs()
    {
        // Arrange
        var user = new User("TestUser", "password", Color.White, true, 0);

        // Act
        var args = new GameOverNotifiedEventArgs { Winner = user };

        // Assert
        Assert.Equal(user, args.Winner);
    }
}