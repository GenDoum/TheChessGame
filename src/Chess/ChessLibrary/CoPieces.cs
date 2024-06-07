using System.Runtime.Serialization;

namespace ChessLibrary;

/// <summary>
/// Structure to facilitate the management of chess pieces.
/// </summary>
[DataContract(Name = "CoPieces")]
public struct CoPieces
{
    [DataMember]
    /// <summary>
    /// Gets or sets the case associated with the piece.
    /// </summary>
    public Case? CaseLink { get; set; }

    [DataMember]
    /// <summary>
    /// Gets or sets the piece.
    /// </summary>
    public Piece? piece { get; set; }
}