using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChessLibrarys
{
    /// <summary>
    /// Classe Player
    /// </summary>
    public class User 
    {


        /// <summary>
        /// Pseudo of the Player
        /// </summary>
        public string Pseudo
        {
            get => pseudo;
            set
            {
                if (pseudo == value)
                {
                    return;
                }
               pseudo = value;
            }
        }
        private string pseudo;

        /// <summary>
        /// Password of the Player
        /// </summary>
        public string Password
        {
            get => password;
            set
            {
                if (password == value)
                {
                    return;
                }
                password = value;
            }

        }
        private string password;

        /// <summary>
        /// Boolean for know if the player is in the white side or not
        /// </summary>
        public Color IsWhite;

        public int Score
        {
            get => score;
            private set;
        }

        private int score;

        /// <summary>
        /// Constructor of Player with parameters
        /// </summary>
        /// <param name="pseudo"></param>
        /// <param name="password"></param>
        public User(string pseudo, string password, Color color)
        {
            Pseudo = pseudo;
            Password = password;
            Color IsWhite = color;
        }

        /// <summary>
        /// Constructor of Player without paramaters.
        /// This constructor will be used for the invited player.
        /// </summary>
        public User()
        {
            Pseudo = "Invité";
            Password = null;
        }


        /// <summary>
        /// Player's method to check the password
        /// </summary>
        /// <param name="password"></param>
        /// <returns></returns>
        public bool isPasswdConsole()
        {

            Console.WriteLine("Enter your password");
            string password = Console.ReadLine();
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

        public bool isPasswd(string password)
        {
            if (Password == null)
            {
                Console.WriteLine("Invited player, no need to check password\n");
                return true;
            }

            if (password.Equals(Password))
            {
                return true;
            }

            return false;
        }
    }

}
