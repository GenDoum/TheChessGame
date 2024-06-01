using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace ChessLibrary
{
    /// <summary>
    /// Classe pour une case d'un échiquier
    /// </summary>
    public class Case : INotifyPropertyChanged
    {
        /// <summary>
        /// Crée un argument Column
        /// </summary>
        private int _column;

        /// <summary>
        /// Crée un argument Line
        /// </summary>
        private int _line;

        /// <summary>
        /// Propriété pour la pièce sur cette case
        /// </summary>
        private Piece? _piece;
        
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
        /// Initialise une nouvelle instance de la classe Case avec la colonne, la ligne et la pièce spécifiées.
        /// </summary>
        /// <param name="column">La colonne de la case</param>
        /// <param name="line">La ligne de la case</param>
        /// <param name="piece">La pièce sur la case</param>
        public Case(int column, int line, Piece? piece)
        {
            Column = column;
            Line = line;
            Piece = piece;
        }


        /// <summary>
        /// Détermine si la case est vide (aucune pièce n'est présente).
        /// </summary>
        /// <returns>true si la case est vide, sinon false</returns>
        public bool IsCaseEmpty() => Piece == null;
        
        public event PropertyChangedEventHandler? PropertyChanged;
        
        void OnPropertyChanged([CallerMemberName]string propertyName = null!)
            => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}