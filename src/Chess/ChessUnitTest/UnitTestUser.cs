namespace ChessUnitTest;
using ChessLibrary;
using ChessLibrarys;

public class UnitTestUser
{
    [Theory]
    [MemberData(nameof(TestData.ValidUserPseudo), MemberType = typeof(TestData))]
    public void ValidPseudo_ReturnTrue(string pseudo, string password, Color color)
    {
        if (string.IsNullOrWhiteSpace(pseudo))
        {
            Assert.Throws<ArgumentException>(() => new User(pseudo, password, color));
            return;
        }
    
        var user = new User(pseudo, password, color);
    
    }

    [Theory]
    [MemberData(nameof(TestData.InvalidUserPseudo), MemberType = typeof(TestData))]
    public void InvalidPseudo_ReturnFalse(string pseudo, string password, Color color)
    {
        if (string.IsNullOrWhiteSpace(pseudo))
        {
            Assert.Throws<ArgumentException>(() => new User(pseudo, password, color));
            return;
        }

        var user = new User(pseudo, password, color);
    }



    [Theory]
    [MemberData(nameof(TestData.ValidUserPassword), MemberType = typeof(TestData))]
    public void GoodPassword_ReturnTrue(string pseudo, string password, Color color)
    {
        var user = new User(pseudo, password, color);

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
    public void InvalidPassword_ReturnFalse(string pseudo, string password, Color color)
    {
        Assert.Throws<ArgumentException>(() =>
        {
            var user = new User(pseudo, password, color);
        });
    }

    [Theory]
    [MemberData(nameof(TestData.ValidUserColor), MemberType = typeof(TestData))]
    public void GoodColor_ReturnTrue(string pseudo, string password, Color color)
    {
        var user = new User(pseudo, password, color);

        Assert.Equal(pseudo, user.Pseudo);
        Assert.Equal(password, user.Password);
        Assert.True(Equals(user.color, Color.White) || Equals(user.color, Color.Black));
    }

    [Theory]
    [MemberData(nameof(TestData.InvalidUserColor), MemberType = typeof(TestData))]
    public void WrongColor_ReturnTrue(string pseudo, string password, Color color)
    {
        var user = new User(pseudo, password, color);

        Assert.NotNull(user.color);
        Assert.True(Equals(user.color, Color.White) || Equals(user.color, Color.Black));
    }
}
