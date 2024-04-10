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
        private string Column { get; set; }
        /// <summary>
        /// Créer un argument Line
        /// </summary>
        private int Line { get; set; }

        private Piece? piece { get; set; }

        public Case(string column, int line , Piece piece1) 
        {
            Column = column;
            Line = line;    
            piece = piece1;
        }

        public bool CaseIsFree => piece == null;

    }
}
