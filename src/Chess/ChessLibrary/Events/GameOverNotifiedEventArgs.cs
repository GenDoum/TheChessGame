namespace ChessLibrary.Events;

/// <summary>
/// Arguments pour l'événement de notification de fin de partie
/// </summary>
public class GameOverNotifiedEventArgs : EventArgs
{
    public User? Loser
    {
        get;
        set;

    }
    public User? Winner
    {
        get;
        set;
        
    }
}