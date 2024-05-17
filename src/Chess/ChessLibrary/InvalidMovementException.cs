namespace ChessLibrary;

public class InvalidMovementException : Exception
{
    public InvalidMovementException() : base() { }
    public InvalidMovementException(string message) : base(message) { }
    public InvalidMovementException(string message, Exception inner) : base(message, inner) { }

    public override string ToString()
    {
        return Message;
    }
}