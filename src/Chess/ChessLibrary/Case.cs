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
        private char Column { get; set; }
        /// <summary>
        /// Créer un argument Line
        /// </summary>
        private int Line { get; set; }

        public Case(char column, int line) 
        {
            Column = column;
            Line = line;    
        }

    }
}
