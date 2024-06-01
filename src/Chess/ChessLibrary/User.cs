using ChessLibrary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using System.Security.Cryptography;

namespace ChessLibrary
{
    /// <summary>
    /// Classe Player
    /// </summary>
    [DataContract(Name = "Players")] // [DataContract, KnownType(typeof(Type fils))] Pour l'héritage
    public class User
    {

        /// <summary>
        /// Pseudo of the Player
        /// </summary>
        [DataMember]
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

        [DataMember]
        private string pseudo;

        /// <summary>
        /// Password of the Player
        /// </summary>
        [DataMember]

        public string? Password { get; set; }

        /// <summary>
        /// Type for know the color of the player
        /// </summary>
        /// 
        [DataMember]
        public Color Color
        {
            get;
            set;
        }


        /// <summary>
        /// Score of the player
        /// </summary>
        [DataMember]
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

            this.pseudo = pseudo;
            this.Password = HashPassword(password);
            this.Color = color;
            Score = playerScore;
            IsConnected = connected;

        }

        public User(Color color)
        {
            string name = $"{color.ToString()} player";

            this.pseudo = name;
            this.Password = null;
            this.Color = color;
            this.Score = 0;
            this.IsConnected = false;

        }

        public User(User user)
        {
            this.pseudo = user.Pseudo;
            this.Password = user.Password == null ? null : HashPassword(user.Password);
            this.Color = user.Color;
            this.Score = user.Score;
            this.IsConnected = user.IsConnected;
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
        public string? HashPassword(string password)
        {
            if (password == null)
            {
                return null; // Dans le cas des joueur invité, qui n'ont pas de mot de passe
            }

            using (var sha256 = SHA256.Create())
            {
                var hashBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
                return Convert.ToBase64String(hashBytes);
            }
        }

        public bool CheckPassword(string password)
        {
            return HashPassword(password) == Password;
        }

        public override string ToString()
        {
            return $"Pseudo: {Pseudo}, Password: {(Password != null ? "Hashed" : "null")}, Color: {Color}, Score: {Score}, IsConnected: {IsConnected}";
        }
    }

}