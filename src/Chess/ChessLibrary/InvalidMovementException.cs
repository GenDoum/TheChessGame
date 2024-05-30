using System.Runtime.Serialization;

namespace ChessLibrary;

[Serializable]
public class InvalidMovementException : Exception
{
    /// <summary>
    /// Exception pour un mouvement invalide
    /// </summary>
    public InvalidMovementException() : base() { }
    public InvalidMovementException(string message) : base(message) { }

    public InvalidMovementException(string? message, Exception? innerException) : base(message, innerException)
    {
    }


    public override string ToString()
    {
        return Message;
    }
}