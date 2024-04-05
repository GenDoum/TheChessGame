using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChessLibrary
{
    /// <summary>
    /// Classe Player
    /// </summary>
    public class Player
    {
        /// <summary>
        /// Pseudo of the Player
        /// </summary>
        public string Pseudo { get; private set; }

        /// <summary>
        /// Mot de passe du Player
        /// </summary>
        private string Mdp;

        /// <summary>
        /// Contstructeur du Player
        /// </summary>
        /// <param name="pseudo"></param>
        /// <param name="mdp"></param>
        public Player(string pseudo, string mdp) 
        {
            Pseudo = pseudo;
            Mdp = mdp;
        }

    }
}
