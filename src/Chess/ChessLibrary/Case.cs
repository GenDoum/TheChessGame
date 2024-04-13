using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChessLibrary
{
    public class Case
    {
        /// <summary>
        /// Créer un argument Colone
        /// </summary>
        public int Column { get; private set; }
        /// <summary>
        /// Créer un argument Line
        /// </summary>
        public int Line { get; private set; }

        public Piece? Piece { get; set; }

        public Case(int column, int line , Piece piece1) 
        {
            Column = column;
            Line = line;    
            Piece = piece1;
        }

        public bool CaseIsFree => Piece == null;

        public bool IsCaseEmpty()
        {
            if (this.Piece == null)
            {
                return true;
            }

            return false;
        }

    }
}
