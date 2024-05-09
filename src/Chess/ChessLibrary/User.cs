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
                if (pseudo == value)
                {
                    return;
                }

                pseudo = value;

                if ( string.IsNullOrWhiteSpace(Pseudo))
                {
                    throw new ArgumentException("Pseudo or password must be entered and must not be full of white space");
                }

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

                if (string.IsNullOrWhiteSpace(Password))
                {
                    throw new ArgumentException("Pseudo or password must be entered and must not be full of white space");
                }

            }

        }
        private string password;

        /// <summary>
        /// Type for know the color of the player
        /// </summary>
        public Color color;

        public int Score
        {
            get => score;
            set
            {
                score = value;
            }
        }

        private int score;

        /// <summary>
        /// Constructor of Player with parameters
        /// </summary>
        /// <param name="pseudo"></param>
        /// <param name="password"></param>
        public User(string pseudo, string password, Color color)
        {
            // En gros ici tu vérifiais si 'Pseudo' et 'Password' étaient null ou vide après leur affectation mais dcp les exceptions étaient pas levées avant que les valeurs soient déjà définies.
            // J'ai aussi enlever IsWhite car il était jamais utilisé je crois. Remet si il servais vraiment.
            // Et dcp la le constructeur vérifie d'abord si pseudo sont vide ou null avant de les affecter a la propriété Pseudo.
            if (string.IsNullOrWhiteSpace(pseudo))
            {
                throw new ArgumentException("Pseudo or password must be entered and must not be full of white space");
            }

            Pseudo = pseudo;
            Password = password;
            this.color = color;
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
            string pass = string.Empty;
            key = System.Console.ReadKey(true);

            while ( key.Key != ConsoleKey.Enter )
            {

                if (key.Key != ConsoleKey.Backspace)
                {
                    pass += key.KeyChar;
                    Console.Write("*");
                }
                else if (key.Key == ConsoleKey.Backspace && !string.IsNullOrEmpty(pass))
                {
                    // Supprime un élément de la liste de char de pass
                    pass = pass.Substring(0, pass.Length - 1);
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
            Console.WriteLine(pass);
            
            if ( Equals( this.Password, null) )
            {
                Console.WriteLine("\nInvited player, no need to check password\n");
                return true;
            }
            if ( Equals(this.Password, pass) )
            {
                Console.WriteLine($"\nGood password, have fun {Pseudo}");
                return true;
            }
            else 
            {
                Console.WriteLine($"\nIt seems like you misswrite {Pseudo}, try again");
                return false;

            }
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