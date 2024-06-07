using System.Runtime.Serialization;

namespace ChessLibrary;

/// <summary>
/// Structure pour facilité la gestion des pièces
/// </summary>
[DataContract(Name = "CoPieces")]
public struct CoPieces
{
    [DataMember]
    public Case? CaseLink { get; set; }

    [DataMember]
    public Piece? piece { get; set; }
}