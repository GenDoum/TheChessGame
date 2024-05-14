using ChessLibrary;
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
                pseudo = value;

                if ( string.IsNullOrWhiteSpace(Pseudo))
                {
                    throw new ArgumentException("Pseudo or password must be entered and must not be full of white space", nameof(Pseudo));
                }
            }
        }
        private string pseudo;

        /// <summary>
        /// Password of the Player
        /// </summary>
        public string? Password { get; set; }

        /// <summary>
        /// Type for know the color of the player
        /// </summary>
        /// 
        public Color Color
        {
            get;
            set;
        }


        /// <summary>
        /// Score of the player
        /// </summary>
        public int Score
        {
            get;
            set;
        }

        /// <summary>
        /// Public boolean to know if the User is connected
        /// </summary>
        public bool IsConnected
        {
            get;
            set;
        }

        /// <summary>
        /// Constructor of Player with parameters
        /// </summary>
        /// <param name="pseudo"></param>
        /// <param name="password"></param>
        public User(string pseudo, string password, Color color, bool connected, int playerScore)
        {
            if (string.IsNullOrWhiteSpace(pseudo))
            {
                throw new ArgumentException("Pseudo or password must be entered and must not be full of white space");
            }

            Pseudo = pseudo;
            Password = password;
            this.Color = color;
            Score = playerScore;
            IsConnected = connected;
        
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
        /// <returns>It return a boolean which say if the user has entering the good password of not</returns>
        public bool isPasswdConsole()
        {
            Console.WriteLine($"Hello {Pseudo}, Enter your password please");

            ConsoleKeyInfo key;
            key = System.Console.ReadKey(true);
            StringBuilder pass = new StringBuilder();
            while ( key.Key != ConsoleKey.Enter )
            {

                if (key.Key != ConsoleKey.Backspace)
                {
                    pass.Append(key.KeyChar);
                    Console.Write("*");
                }
                else if (key.Key == ConsoleKey.Backspace && pass.Length > 0)
                {
                    // Supprime un élément de la liste de char de pass
                    pass.Remove(pass.Length - 1, 1);
                    // Récupère la position du curseur
                    int pos = System.Console.CursorLeft;
                    // Déplace le curseur d'un à gauche
                    System.Console.SetCursorPosition(pos - 1, System.Console.CursorTop);
                    // Remplace par un espace dans la console
                    System.Console.Write(" ");
                    // Déplace le curseur d'une position à gauche encore
                    System.Console.SetCursorPosition(pos - 1, System.Console.CursorTop);
                }

                key = System.Console.ReadKey(true);

            }
            
            if ( Equals( this.Password, null) )
            {
                Console.WriteLine("\nInvited player, no need to check password\n");
                return true;
            }
            if ( this.Password.Equals(pass.ToString()) )
            {
                Console.WriteLine($"\nGood password, have fun {Pseudo}");
                return true;
            }
            else 
            {
                Console.WriteLine($"\nIl semblerait que tu te soit trompé {Pseudo}, essaie à nouveau");
                return false;

            }
        }

        /// <summary>
        /// Player's method for check the password
        /// </summary>
        /// <param name="password"></param>
        /// <returns></returns>
        public bool isPasswd(string password)
        {
            if (Password == null)
            {
                Console.WriteLine("Joeur invité, pas besoin de mot de passe\n");
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