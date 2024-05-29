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
            get => _pseudo;
            set
            {
                _pseudo = value;

                if (string.IsNullOrWhiteSpace(Pseudo))
                {
                    throw new ArgumentException("Pseudo or password must be entered and must not be full of white space", nameof(Pseudo));
                }
            }
        }
        private string _pseudo;

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
        /// Constructor of Player with parameters.
        /// </summary>
        /// <param name="pseudo"></param>
        /// <param name="password"></param>
        /// <param name="color"></param>
        /// <param name="connected"></param>
        /// <param name="playerScore"></param>
        /// <exception cref="ArgumentException"></exception>
        public User(string pseudo, string? password, Color color, bool connected, int playerScore)
        {
            if (string.IsNullOrWhiteSpace(pseudo))
            {
                throw new ArgumentException("Pseudo or password must be entered and must not be full of white space");
            }

            this._pseudo = pseudo;
            this.Password = password;
            this.Color = color;
            Score = playerScore;
            IsConnected = connected;

        }

        public User(Color color)
        {
            string name = $"{color.ToString()} player";

            this._pseudo = name;
            this.Password = null;
            this.Color = color;
            this.Score = 0;
            this.IsConnected = false;

        }


        /// <summary>
        /// Constructor of Player without paramaters.
        /// This constructor will be used for the invited player.
        /// </summary>
        public User()
        {
            _pseudo = "Invité";
            Password = null;
        }
    }

}