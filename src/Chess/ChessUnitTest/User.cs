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

                if (string.IsNullOrWhiteSpace(Pseudo))
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
<<<<<<< HEAD
=======
        /// <summary>
        /// Private score of the player
        /// </summary>
        private int score;
>>>>>>> 58cea1162f8d2694b92c38d176ae7b9ab331234b

        /// <summary>
        /// Public boolean to know if the User is connected
        /// </summary>
        public bool IsConnected
        {
            get;
            set;
<<<<<<< HEAD
        }
=======
        }
        /// <summary>
        /// Private boolean of the player
        /// </summary>
        private bool isConnected;

        /// <summary>
        /// Public list of pieces of the player
        /// </summary>
        public List<Piece> Pieces
        {
            get => pieces;
            set
            {
                pieces = value;
            }
        }
        /// <summary>
        /// Private list of pieces of the player
        /// </summary>
        private List<Piece> pieces;

>>>>>>> 58cea1162f8d2694b92c38d176ae7b9ab331234b

        /// <summary>
        /// Constructor of Player with parameters
        /// </summary>
        /// <param name="pseudo"></param>
        /// <param name="password"></param>
<<<<<<< HEAD
        public User(string pseudo, string? password, Color color, bool connected, int playerScore)
=======
        public User(string pseudo, string password, Color color, bool connected, List<Piece> listPieces, int playerScore)
>>>>>>> 58cea1162f8d2694b92c38d176ae7b9ab331234b
        {
            if (string.IsNullOrWhiteSpace(pseudo))
            {
                throw new ArgumentException("Pseudo or password must be entered and must not be full of white space");
            }

<<<<<<< HEAD
            this.pseudo = pseudo;
            this.Password = password;
            this.Color = color;
            Score = playerScore;
            IsConnected = connected;
        
=======
            Pseudo = pseudo;
            Password = password;
            this.color = color;
            Score = playerScore;
            IsConnected = connected;
            Pieces = listPieces;

>>>>>>> 58cea1162f8d2694b92c38d176ae7b9ab331234b
        }

        /// <summary>
        /// Constructor of Player without paramaters.
        /// This constructor will be used for the invited player.
        /// </summary>
        public User()
        {
            pseudo = "Invité";
            Password = null;
        }

<<<<<<< HEAD
        
=======
        /// <summary>
        /// Player's method to check the password
        /// </summary>
        /// <returns>It return a boolean which say if the user has entering the good password of not</returns>
        public bool isPasswdConsole()
        {

            Console.WriteLine($"Hello {Pseudo}, Enter your password please");

            ConsoleKeyInfo key;
            string pass = "";
            key = System.Console.ReadKey(true);

            while (key.Key != ConsoleKey.Enter)
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

            if (Equals(this.Password, null))
            {
                Console.WriteLine("\nInvited player, no need to check password\n");
                return true;
            }
            if (Equals(this.Password, pass))
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
>>>>>>> 58cea1162f8d2694b92c38d176ae7b9ab331234b

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