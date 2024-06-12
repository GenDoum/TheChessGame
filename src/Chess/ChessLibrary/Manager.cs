using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace ChessLibrary
{
    /// <summary>
    /// Manages the chess games, users, and chessboards, and handles data persistence.
    /// </summary>
    public class Manager : INotifyPropertyChanged
    {
        /// <summary>
        /// Gets or sets the persistence manager for data storage.
        /// </summary>
        public IPersistanceManager? PersistanceManager { get; set; }

        private ObservableCollection<Game>? _games;

        /// <summary>
        /// Gets or sets the collection of users.
        /// </summary>
        public ObservableCollection<User>? Users { get; set; }

        /// <summary>
        /// Gets or sets the collection of chessboards.
        /// </summary>
        public ObservableCollection<Chessboard>? Chessboards { get; set; }

        /// <summary>
        /// Gets or sets the collection of games.
        /// </summary>
        public ObservableCollection<Game> Games
        {
            get
            {
                return _games ??= new ObservableCollection<Game>();
            }
            set
            {
                _games = value;
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// Event triggered when a property value changes.
        /// </summary>
        public event PropertyChangedEventHandler? PropertyChanged;

        /// <summary>
        /// Method to raise the PropertyChanged event.
        /// </summary>
        /// <param name="propertyName">The name of the property that changed.</param>
        void OnPropertyChanged([CallerMemberName] string? propertyName = null)
            => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));

        /// <summary>
        /// Initializes a new instance of the <see cref="Manager"/> class with a specified persistence manager.
        /// </summary>
        /// <param name="persistanceManager">The persistence manager for data storage.</param>
        public Manager(IPersistanceManager persistanceManager)
        {
            Users = new ObservableCollection<User>();
            Games = new ObservableCollection<Game>();
            Chessboards = new ObservableCollection<Chessboard>();

            this.PersistanceManager = persistanceManager;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Manager"/> class.
        /// </summary>
        public Manager()
        {
            Users = new ObservableCollection<User>();
            Games = new ObservableCollection<Game>();
            Chessboards = new ObservableCollection<Chessboard>();
        }
        
        /// <summary>
        /// Loads data from the persistence manager.
        /// </summary>
        public void LoadData()
        {
            if (PersistanceManager != null)
            {
                var data = PersistanceManager.LoadData();

                List<Game> gamesToRemove = new List<Game>();

                foreach (var game in data.Item1)
                {
                    if (game.Player2.Pseudo != "Black player")
                    {
                        gamesToRemove.Add(game);
                    }
                }
        
                foreach (var game in gamesToRemove)
                {
                    Games.Remove(game);
                }

                foreach (var user in data.Item2)
                {
                    Users!.Add(user);
                }

                foreach (var chessboard in data.Item3)
                {
                    Chessboards!.Add(chessboard);
                }
            }
        }
        
        /// <summary>
        /// Saves data to the persistence manager.
        /// </summary>
        public void SaveData()
        {
            PersistanceManager?.SaveData(Games, Users!, Chessboards!);
        }
    }
}
