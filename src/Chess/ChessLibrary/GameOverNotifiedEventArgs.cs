namespace ChessLibrary;

public class GameOverNotifiedEventArgs : EventArgs
{
    public User Winner
    {
        get
        {
            return Winner;
        }
        set
        {
            Winner.Score += 5;
        }
    }
}