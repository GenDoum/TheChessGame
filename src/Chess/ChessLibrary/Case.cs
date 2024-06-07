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
    /// Represents a square on a chessboard.
    /// </summary>
    [DataContract(Name = "Case")]
    public class Case : INotifyPropertyChanged
    {
        /// <summary>
        /// The column index of the square.
        /// </summary>
        private int _column;

        /// <summary>
        /// The row index of the square.
        /// </summary>
        private int _line;

        /// <summary>
        /// The piece currently on the square.
        /// </summary>
        private Piece? _piece;

        /// <summary>
        /// Backing field for the IsPossibleMove property.
        /// </summary>
        private bool _isPossibleMove;

        /// <summary>
        /// Gets or sets a value indicating whether the move is possible.
        /// </summary>
        [DataMember]
        public bool IsPossibleMove
        {
            get => _isPossibleMove;
            set
            {
                // Set the backing field and notify any listeners that the property has changed.
                _isPossibleMove = value;
                OnPropertyChanged();
            }
        }

        [DataMember]

        /// <summary>
        /// Gets or sets the column index of the square.
        /// </summary>
        public int Column
        {
            get { return _column; }
            private set
            {
                if (_column != value)
                {
                    _column = value;
                    OnPropertyChanged();
                }
            }
        }

        /// <summary>
        /// Gets or sets the row index of the square.
        /// </summary>
        [DataMember]
        public int Line
        {
            get { return _line; }
            private set
            {
                if (_line != value)
                {
                    _line = value;
                    OnPropertyChanged();
                }
            }
        }

        [DataMember]
        /// <summary>
        /// Gets or sets the piece on the square.
        /// </summary>
        public Piece? Piece
        {
            get { return _piece; }
            set
            {
                if (_piece != value)
                {
                    _piece = value;
                    OnPropertyChanged();
                }
            }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Case"/> class with the specified column, line, and piece.
        /// </summary>
        /// <param name="column">The column index of the square.</param>
        /// <param name="line">The row index of the square.</param>
        /// <param name="piece">The piece on the square.</param>
        public Case(int column, int line, Piece? piece)
        {
            Column = column;
            Line = line;
            Piece = piece;
        }

        /// <summary>
        /// Determines whether the square is empty (no piece is present).
        /// </summary>
        /// <returns>true if the square is empty; otherwise, false.</returns>
        public bool IsCaseEmpty() => this.Piece == null;

        /// <summary>
        /// Occurs when a property value changes.
        /// </summary>
        public event PropertyChangedEventHandler? PropertyChanged;

        /// <summary>
        /// Raises the <see cref="PropertyChanged"/> event.
        /// </summary>
        /// <param name="propertyName">The name of the property that changed.</param>
        protected void OnPropertyChanged([CallerMemberName] string propertyName = null!)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
