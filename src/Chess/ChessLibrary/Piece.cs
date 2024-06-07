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
    /// Classe abstraite pour les pièces
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
        /// Constructeur de la pièce
        /// </summary>
        /// <param name="color"></param>
        /// <param name="indentifiant"></param>
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
        /// Vérifier si la pièce peut bouger
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="x2"></param>
        /// <param name="y2"></param>
        /// <returns></returns>
        public abstract bool CanMove(int x, int y, int x2, int y2);
        
        /// <summary>
        /// Stocker tous les mouvements possibles de la pièce
        /// </summary>
        /// <param name="caseInitial"></param>
        /// <param name="chessboard"></param>
        /// <returns></returns>
        public abstract List<Case?> PossibleMoves(Case? caseInitial, Chessboard chessboard);

        public event PropertyChangedEventHandler? PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string? propertyName = null)
            => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));

    }
}