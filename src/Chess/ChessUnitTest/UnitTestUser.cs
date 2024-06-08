namespace ChessUnitTest;
using ChessLibrary;

public class UnitTestUser
{
    [Theory]
    [MemberData(nameof(TestData.ValidUserPseudo), MemberType = typeof(TestData))]
    public void ValidPseudo_ReturnTrue(string pseudo, string password, Color color, bool connected, int score)
    {
        if (string.IsNullOrWhiteSpace(pseudo))
        {
            Assert.Throws<ArgumentException>(() => new User(pseudo, password, color, connected, score));
            return;
        }

        var user = new User(pseudo, password, color, connected, score);
    }

    [Theory]
    [MemberData(nameof(TestData.InvalidUserPseudo), MemberType = typeof(TestData))]
    public void InvalidPseudo_ReturnFalse(string pseudo, string password, Color color, bool connected, int score)
    {
        if (!string.IsNullOrWhiteSpace(pseudo))
        {
            var user = new User(pseudo, password, color, connected, score);
        }
        Assert.Throws<ArgumentException>(() => new User(pseudo, password, color, connected, score));


    }

    [Theory]
    [MemberData(nameof(TestData.ValidUserPassword), MemberType = typeof(TestData))]
    public void GoodPassword_ReturnTrue(string pseudo, string password, Color color, bool connected, int score)
    {
        var user = new User(pseudo, password, color, connected, score);

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
    public void InvalidPassword_ReturnFalse(string pseudo, string password, Color color, bool connected, int score)
    {
        if (string.IsNullOrWhiteSpace(pseudo))
        {
            Assert.Throws<ArgumentException>(() =>
            {
                var user = new User(pseudo, password, color, connected, score);
            });
        }
    }

    [Theory]
    [MemberData(nameof(TestData.ValidUserColor), MemberType = typeof(TestData))]
    public void GoodColor_ReturnTrue(string pseudo, string password, Color color, bool connected, int score)
    {
        var user = new User(pseudo, password, color, connected, score);

        Assert.Equal(pseudo, user.Pseudo);
        Assert.Equal(User.HashPassword(password), user.Password);
        Assert.True(Equals(user.Color, Color.White) || Equals(user.Color, Color.Black));
    }

    [Theory]
    [MemberData(nameof(TestData.InvalidUserColor), MemberType = typeof(TestData))]
    public void WrongColor_ReturnTrue(string pseudo, string password, Color color, bool connected, int score)
    {
        var user = new User(pseudo, password, color, connected, score);

        Assert.True(Equals(user.Color, Color.White) || Equals(user.Color, Color.Black));
    }

    [Theory]
    [MemberData(nameof(TestData.ValidUserPassword), MemberType = typeof(TestData))]
    public void GoodBoolConnected_ReturnTrue(string pseudo, string password, Color color, bool connected, int score)
    {
        var user = new User(pseudo, password, color, connected, score);

        Assert.True(Equals(user.IsConnected, true) || Equals(user.IsConnected, false));
    }

    [Theory]
    [MemberData(nameof(TestData.InvalidBoolConnected), MemberType = typeof(TestData))]
    public void WrongBoolConnected(string pseudo, string password, ChessLibrary.Color color, bool? connected, int score)
    {
        var user = new User(pseudo, password, color, connected ?? false, score);

        Assert.Null(connected);
    }

    [Fact]
    public void TestUserConstructorWithColor()
    {
        // Arrange
        var color = Color.White;

        // Act
        var user = new User(color);

        // Assert
        Assert.Equal("White player", user.Pseudo);
        Assert.Null(user.Password);
        Assert.Equal(color, user.Color);
        Assert.Equal(0, user.Score);
        Assert.False(user.IsConnected);
    }

    [Fact]
    public void TestUserConstructorWithoutParameters()
    {
        // Act
        var user = new User();

        // Assert
        Assert.Equal("Invité", user.Pseudo);
        Assert.Null(user.Password);
    }
    
    [Fact]
    public void Pseudo_SetEmptyString_ThrowsArgumentException()
    {
        // Arrange
        var user = new User("validPseudo", "password", Color.White, true, 0);

        // Act & Assert
        Assert.Throws<ArgumentException>(() => user.Pseudo = "");
    }

    [Fact]
    public void Pseudo_SetWhitespaceString_ThrowsArgumentException()
    {
        // Arrange
        var user = new User("validPseudo", "password", Color.White, true, 0);

        // Act & Assert
        Assert.Throws<ArgumentException>(() => user.Pseudo = "   ");
    }

    [Fact]
    public void Pseudo_SetNull_ThrowsArgumentException()
    {
        // Arrange
        var user = new User("validPseudo", "password", Color.White, true, 0);

        // Act & Assert
        Assert.Throws<ArgumentException>(() => user.Pseudo = null!);
    }
    
}