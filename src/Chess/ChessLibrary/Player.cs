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
        readonly string Pseudo;

        /// <summary>
        /// Password of the Player
        /// </summary>
        private string Password;

        /// <summary>
        /// Boolean for know if the player is in the white side or not
        /// </summary>
        public Color IsWhite;

        /// <summary>
        /// Constructor of Player with parameters
        /// </summary>
        /// <param name="pseudo"></param>
        /// <param name="password"></param>
        public Player(string pseudo, string password, Color color) 
        {
            Pseudo = pseudo;
            Password = password;
            Color IsWhite = color;
        }

        /// <summary>
        /// Constructor of Player without paramater.
        /// This constructor will be used for the invited player.
        /// </summary>
        public Player()
        {
            Pseudo = "Invité";
            Password = null;
        }


        /// <summary>
        /// Player's method to check the password
        /// </summary>
        /// <param name="password"></param>
        /// <returns></returns>
        public bool isPasswd(string password)
        {
            if (Password == null) 
            {
                Console.WriteLine("Invited player, no need to check password\n");
                return true;
            }

            if (Password == password)
            {
                return true;
            }

            return false;
        }
    }
}
