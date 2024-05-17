namespace ChessLibrary;

public class GameOverNotifiedEventArgs : EventArgs
{
    public User Winner { get; set; }
}