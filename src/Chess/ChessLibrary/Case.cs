using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChessLibrary
{
    /// <summary>
    /// Class that represents a case
    /// </summary>
    public class Case
    {
        /// <summary>
        /// Create a column
        /// </summary>
        private string Column { get; set; }
        /// <summary>
        /// Create a line
        /// </summary>
        private int Line { get; set; }

        public Piece? Piece { get; set; }

        public Case(string column, int line , Piece piece1) 
        {
            Column = column;
            Line = line;    
            Piece = piece1;
        }

        public bool CaseIsFree => Piece == null;

    }
}
