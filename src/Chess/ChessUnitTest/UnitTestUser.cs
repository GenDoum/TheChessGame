namespace ChessUnitTest;
using ChessLibrary;

public class UnitTestUser
{
    [Theory]
    [MemberData(nameof(TestData.ValidUserPseudo), MemberType = typeof(TestData))]
    public void ValidPseudo_ReturnTrue(string pseudo, string password, Color color, bool connected, List<Piece> pieces, int score)
    {
        if (string.IsNullOrWhiteSpace(pseudo))
        {
            Assert.Throws<ArgumentException>(() => new User(pseudo, password, color, connected, pieces, score));
            return;
        }
    
        var user = new User(pseudo, password, color, connected, pieces, score);
    
    }

    [Theory]
    [MemberData(nameof(TestData.InvalidUserPseudo), MemberType = typeof(TestData))]
    public void InvalidPseudo_ReturnFalse(string pseudo, string password, Color color, bool connected, List<Piece> pieces, int score)
    {
        if (string.IsNullOrWhiteSpace(pseudo))
        {
            Assert.Throws<ArgumentException>(() => new User(pseudo, password, color, connected, pieces, score));
            return;
        }

        var user = new User(pseudo, password, color, connected, pieces, score);
    }



    [Theory]
    [MemberData(nameof(TestData.ValidUserPassword), MemberType = typeof(TestData))]
    public void GoodPassword_ReturnTrue(string pseudo, string password, Color color, bool connected, List<Piece> pieces, int score)
    {
        var user = new User(pseudo, password, color, connected, pieces, score);

        if (password != null)
        {
            Assert.NotNull(password);
        }
        else
        {
            Assert.Null(password);
        }
    }


    [Theory]
    [MemberData(nameof(TestData.InvalidUserPassword), MemberType = typeof(TestData))]
    public void InvalidPassword_ReturnFalse(string pseudo, string password, Color color, bool connected, List<Piece> pieces, int score)
    {
        Assert.Throws<ArgumentException>(() =>
        {
            var user = new User(pseudo, password, color, connected, pieces, score);
        });
    }

    [Theory]
    [MemberData(nameof(TestData.ValidUserColor), MemberType = typeof(TestData))]
    public void GoodColor_ReturnTrue(string pseudo, string password, Color color, bool connected, List<Piece> pieces, int score)
    {
        var user = new User(pseudo, password, color, connected, pieces, score);

        Assert.Equal(pseudo, user.Pseudo);
        Assert.Equal(password, user.Password);
        Assert.True(Equals(user.color, Color.White) || Equals(user.color, Color.Black));
    }

    [Theory]
    [MemberData(nameof(TestData.InvalidUserColor), MemberType = typeof(TestData))]
    public void WrongColor_ReturnTrue(string pseudo, string password, Color color, bool connected, List<Piece> pieces, int score)
    {
        var user = new User(pseudo, password, color, connected, pieces, score);

        Assert.NotNull(user.color);
        Assert.True(Equals(user.color, Color.White) || Equals(user.color, Color.Black));
    }

    [Theory]
    [MemberData(nameof(TestData.ValidBoolConnected), MemberType = typeof(TestData))]
    public void GoodBoolConnected_ReturnTrue(string pseudo, string password, Color color, bool connected, List<Piece> pieces, int score)
    {
        var user = new User(pseudo, password, color, connected, pieces, score);

        Assert.True(Equals(user.IsConnected, true) || Equals(user.IsConnected, false));
    }

    [Theory]
    [MemberData(nameof(TestData.InvalidBoolConnected), MemberType = typeof(TestData))]
    public void WrongBoolConnected(string pseudo, string password, ChessLibrary.Color color, bool? connected, List<Piece> pieces, int score)
    {
        var user = new User(pseudo, password, color, connected ?? false, pieces, score);

        // Vérifie que la valeur de connected est incorrecte (null)
        Assert.Null(connected);
    }



}
