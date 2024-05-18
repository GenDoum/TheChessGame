namespace ChessLibrary;

/// <summary>
/// Arguments pour l'événement de notification d'évolution
/// </summary>
public class EvolveNotifiedEventArgs : EventArgs
{
    public Pawn? Pawn { get; set; }
    public Case Case { get; set; }
}