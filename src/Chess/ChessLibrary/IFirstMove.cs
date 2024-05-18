using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChessLibrary
{
    /// <summary>
    /// Interface pour le premier mouvement
    /// </summary>
    public interface IFirstMove
    {
        /// <summary>
        /// Premier mouvement
        /// </summary>
        bool FirstMove { get; set; }
    }
}
