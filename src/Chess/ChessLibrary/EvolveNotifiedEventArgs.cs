namespace ChessLibrary;

public class EvolveNotifiedEventArgs : EventArgs
{
    public Pawn? Pawn { get; set; }
    public Case Case { get; set; }
}