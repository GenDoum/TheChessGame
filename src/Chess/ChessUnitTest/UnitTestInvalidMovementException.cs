using ChessLibrary;

namespace ChessUnitTest;

public class UnitTestInvalidMovementException
{
    [Fact]
    public void TestInvalidMovementException_DefaultConstructor()
    {
        var ex = new InvalidMovementException();
        Assert.NotNull(ex);
        Assert.False(string.IsNullOrEmpty(ex.Message));
    }

    [Fact]
    public void TestInvalidMovementException_MessageConstructor()
    {
        var ex = new InvalidMovementException("Test message");
        Assert.NotNull(ex);
        Assert.Equal("Test message", ex.Message);
    }

    [Fact]
    public void TestInvalidMovementException_InnerExceptionConstructor()
    {
        var innerException = new Exception("Inner exception message");
        var ex = new InvalidMovementException("Test message", innerException);
        Assert.NotNull(ex);
        Assert.Equal("Test message", ex.Message);
        Assert.Equal(innerException, ex.InnerException);
    }

    [Fact]
    public void TestInvalidMovementException_ToString()
    {
        var ex = new InvalidMovementException("Test message");
        Assert.Equal("Test message", ex.ToString());
    }
}