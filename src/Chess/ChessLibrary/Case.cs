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
        public int Column { get; private set; }

        /// <summary>
        /// Crée un argument Line
        /// </summary>
        public int Line { get; private set; }

        /// <summary>
        /// Propriété pour la pièce sur cette case
        /// </summary>
        public Piece? Piece { get; set; }

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

        protected virtual void OnPropertyChanged([CallerMemberName] string? propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        protected bool SetField<T>(ref T field, T value, [CallerMemberName] string? propertyName = null)
        {
            if (EqualityComparer<T>.Default.Equals(field, value)) return false;
            field = value;
            OnPropertyChanged(propertyName);
            return true;
        }
    }
}