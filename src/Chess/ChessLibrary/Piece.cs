using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace ChessLibrary
{
    /// <summary>
    /// Abstract class representing a chess piece.
    /// </summary>
    [DataContract(Name = "Piece")]
    [KnownType(typeof(Rook))]
    [KnownType(typeof(Pawn))]
    [KnownType(typeof(Knight))]
    [KnownType(typeof(King))]
    [KnownType(typeof(Queen))]
    [KnownType(typeof(Bishop))]
    public abstract class Piece : INotifyPropertyChanged
    {
        private Color _color;
        private int _id;
        private string? _imagePath;

        /// <summary>
        /// Gets or sets the color of the piece.
        /// </summary>
        [DataMember]
        public Color Color
        {
            get { return _color; }
            private set
            {
                if (_color != value)
                {
                    _color = value;
                    OnPropertyChanged();
                }
            }
        }

        /// <summary>
        /// Gets or sets the identifier of the piece.
        /// </summary>
        [DataMember]
        public int Id
        {
            get { return _id; }
            private set
            {
                if (_id != value)
                {
                    _id = value;
                    OnPropertyChanged();
                }
            }
        }

        /// <summary>
        /// Gets or sets the image path of the piece.
        /// </summary>
        [DataMember]
        public string? ImagePath
        {
            get { return _imagePath; }
            protected set
            {
                if (_imagePath != value)
                {
                    _imagePath = value;
                    OnPropertyChanged();
                }
            }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Piece"/> class.
        /// </summary>
        /// <param name="color">The color of the piece.</param>
        /// <param name="indentifiant">The unique identifier for this piece.</param>
        /// <exception cref="ArgumentException">Thrown when the color is not valid.</exception>
        protected Piece(Color color, int indentifiant)
        {
            Color = color;
            Id = indentifiant;

            if (color != Color.Black && color != Color.White)
            {
                throw new ArgumentException("Invalid color");
            }
        }

        /// <summary>
        /// Determines if the piece can move to the specified coordinates.
        /// </summary>
        /// <param name="x">The current x-coordinate of the piece.</param>
        /// <param name="y">The current y-coordinate of the piece.</param>
        /// <param name="x2">The x-coordinate of the destination.</param>
        /// <param name="y2">The y-coordinate of the destination.</param>
        /// <returns>Returns true if the move is valid; otherwise, false.</returns>
        public abstract bool CanMove(int x, int y, int x2, int y2);

        /// <summary>
        /// Generates a list of all possible legal moves for the piece from the given position.
        /// </summary>
        /// <param name="caseInitial">The initial case of the piece.</param>
        /// <param name="chessboard">The chessboard on which the piece is placed.</param>
        /// <returns>A list of possible moves for the piece.</returns>
        public abstract List<Case?> PossibleMoves(Case? caseInitial, Chessboard chessboard);

        /// <summary>
        /// Event triggered when a property value changes.
        /// </summary>
        public event PropertyChangedEventHandler? PropertyChanged;

        /// <summary>
        /// Method to raise the PropertyChanged event.
        /// </summary>
        /// <param name="propertyName">The name of the property that changed.</param>
        protected virtual void OnPropertyChanged([CallerMemberName] string? propertyName = null)
            => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}
