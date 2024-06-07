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
    /// Classe Player
    /// </summary>
    [DataContract(Name = "Players")] // [DataContract, KnownType(typeof(Type fils))] Pour l'héritage


    public class User : INotifyPropertyChanged
    {

        /// <summary>
        /// Pseudo of the Player
        /// </summary>
        [DataMember]
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
                OnPropertyChanged(nameof(_pseudo));

            }
        }

        [DataMember]
        private string _pseudo;
        public event PropertyChangedEventHandler? PropertyChanged;
        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        /// <summary>
        /// Password of the Player
        /// </summary>
        [DataMember]
        public string? Password { get; set; }

        /// <summary>
        /// Type for know the color of the player
        /// </summary>
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
            get { return _score; }  // Retourne la valeur de la variable privée
            set
            {
                if (_score != value)  // Vérifie si la valeur est différente pour éviter des notifications inutiles
                {
                    _score = value;  // Met à jour la valeur de la variable privée
                    OnPropertyChanged(nameof(Score));  // Notifie que la propriété Score a changé
                    OnPropertyChanged(nameof(ScoreWithSuffix));
                }
            }
        }
        public string ScoreWithSuffix => $"{Score} Élo";

        private int _score;  // Variable privée pour stocker la valeur de Score
        /// <summary>
        /// Public boolean to know if the User is connected
        /// </summary>
        [DataMember]
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
        public User(string pseudo, string password, Color color, bool connected, int playerScore)
        {
            if (string.IsNullOrWhiteSpace(pseudo))
            {
                throw new ArgumentException("Pseudo or password must be entered and must not be full of white space");
            }

            this._pseudo = pseudo;
            this.Password = User.HashPassword(password);
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

        public User(User user)
        {
            this._pseudo = user.Pseudo;
            this.Password = user.Password == null ? null : User.HashPassword(user.Password);
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
            _pseudo = "Invité";
            Password = null;

        }
        public static string? HashPassword(string password)
        {
            if (password == null)
            {
                return null; // Dans le cas des joueur invité, qui n'ont pas de mot de passe
            }

            using (var sha256 = SHA256.Create())
            {
                var hashBytes = System.Security.Cryptography.SHA256.HashData(Encoding.UTF8.GetBytes(password));
                return Convert.ToBase64String(hashBytes);
            }
        }

        public bool CheckPassword(string password)
        {
            return User.HashPassword(password) == Password;
        }

        public override string ToString()
        {
            return $"Pseudo: {Pseudo}, Password: {(Password != null ? "Hashed" : "null")}, Color: {Color}, Score: {Score}, IsConnected: {IsConnected}";
        }
    }

}