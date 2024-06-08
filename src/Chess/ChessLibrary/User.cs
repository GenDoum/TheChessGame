using ChessLibrary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using System.Security.Cryptography;
using System.ComponentModel;

namespace ChessLibrary
{
    /// <summary>
    /// Represents a player in the chess game.
    /// </summary>
    [DataContract(Name = "Players")] // [DataContract, KnownType(typeof(Type fils))] for inheritance
    public class User : INotifyPropertyChanged
    {
        private string _pseudo;
        private int _score;

        /// <summary>
        /// Gets or sets the player's pseudo.
        /// </summary>
        [DataMember]
        public string Pseudo
        {
            get => _pseudo;
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentException("Pseudo or password must be entered and must not be full of white space", nameof(Pseudo));
                }
                _pseudo = value;
                OnPropertyChanged(nameof(Pseudo));
            }
        }

        /// <summary>
        /// Gets or sets the player's password.
        /// </summary>
        [DataMember]
        public string? Password { get; set; }

        /// <summary>
        /// Gets or sets the player's color.
        /// </summary>
        [DataMember]
        public Color Color { get; set; }

        /// <summary>
        /// Gets or sets the player's score.
        /// </summary>
        [DataMember]
        public int Score
        {
            get { return _score; }
            set
            {
                if (_score != value)
                {
                    _score = value;
                    OnPropertyChanged(nameof(Score));
                    OnPropertyChanged(nameof(ScoreWithSuffix));
                }
            }
        }

        /// <summary>
        /// Gets the player's score with a suffix.
        /// </summary>
        public string ScoreWithSuffix => $"{Score} Élo";

        /// <summary>
        /// Gets or sets a value indicating whether the player is connected.
        /// </summary>
        [DataMember]
        public bool IsConnected { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="User"/> class with parameters.
        /// </summary>
        /// <param name="pseudo">The pseudo of the player.</param>
        /// <param name="password">The password of the player.</param>
        /// <param name="color">The color of the player.</param>
        /// <param name="connected">Indicates if the player is connected.</param>
        /// <param name="playerScore">The score of the player.</param>
        /// <exception cref="ArgumentException">Thrown when the pseudo is null or whitespace.</exception>
        public User(string pseudo, string password, Color color, bool connected, int playerScore)
        {
            if (string.IsNullOrWhiteSpace(pseudo))
            {
                throw new ArgumentException("Pseudo or password must be entered and must not be full of white space");
            }

            this._pseudo = pseudo;
            this.Password = User.HashPassword(password);
            this.Color = color;
            this.Score = playerScore;
            this.IsConnected = connected;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="User"/> class with a specified color.
        /// </summary>
        /// <param name="color">The color of the player.</param>
        public User(Color color)
        {
            string name = $"{color} player";
            this._pseudo = name;
            this.Password = null;
            this.Color = color;
            this.Score = 0;
            this.IsConnected = false;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="User"/> class by copying another user.
        /// </summary>
        /// <param name="user">The user to copy.</param>
        public User(User user)
        {
            this._pseudo = user.Pseudo;
            this.Password = user.Password == null ? null : User.HashPassword(user.Password);
            this.Color = user.Color;
            this.Score = user.Score;
            this.IsConnected = user.IsConnected;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="User"/> class without parameters.
        /// This constructor will be used for the invited player.
        /// </summary>
        public User()
        {
            _pseudo = "Invité";
            Password = null;
        }

        /// <summary>
        /// Hashes the specified password using SHA256.
        /// </summary>
        /// <param name="password">The password to hash.</param>
        /// <returns>The hashed password as a base64 string, or null if the password is null.</returns>
        public static string? HashPassword(string password)
        {
            if (password == null)
            {
                return null; // For guest players who don't have a password.
            }

            using (var sha256 = SHA256.Create())
            {
                var hashBytes = SHA256.HashData(Encoding.UTF8.GetBytes(password));
                return Convert.ToBase64String(hashBytes);
            }
        }

        /// <summary>
        /// Checks if the specified password matches the hashed password.
        /// </summary>
        /// <param name="password">The password to check.</param>
        /// <returns>True if the passwords match; otherwise, false.</returns>
        public bool CheckPassword(string password)
        {
            return User.HashPassword(password) == Password;
        }

        /// <summary>
        /// Returns a string that represents the current user.
        /// </summary>
        /// <returns>A string that represents the current user.</returns>
        public override string ToString()
        {
            return $"Pseudo: {Pseudo}, Password: {(Password != null ? "Hashed" : "null")}, Color: {Color}, Score: {Score}, IsConnected: {IsConnected}";
        }

        /// <summary>
        /// Event triggered when a property value changes.
        /// </summary>
        public event PropertyChangedEventHandler? PropertyChanged;

        /// <summary>
        /// Raises the PropertyChanged event.
        /// </summary>
        /// <param name="propertyName">The name of the property that changed.</param>
        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
