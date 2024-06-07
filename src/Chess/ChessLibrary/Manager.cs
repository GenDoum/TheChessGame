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
    public class Manager : INotifyPropertyChanged
    {
        public IPersistanceManager? persistanceManager { get; set; }

        public ObservableCollection<Game>? games;

        public ObservableCollection<User>? Users;

        public ObservableCollection<Chessboard>? Chessboards;


        public ObservableCollection<Game> Games
        {
            get
            {
                return games ??= new ObservableCollection<Game>();
            }
            set
            {
                games = value;
                OnPropertyChanged();
            }
        }

        public event PropertyChangedEventHandler? PropertyChanged;

        void OnPropertyChanged([CallerMemberName] string? propertyName = null)
            => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));

        public Manager(IPersistanceManager persistanceManager)
        {
            Users = new ObservableCollection<User>();
            Games = new ObservableCollection<Game>();
            Chessboards = new ObservableCollection<Chessboard>();

            this.persistanceManager = persistanceManager;
        }

        public Manager()
        {
            Users = new ObservableCollection<User>();
            Games = new ObservableCollection<Game>();
            Chessboards = new ObservableCollection<Chessboard>();
        }

        public void LoadData()
        {
            if(persistanceManager != null)
            {
                var data = persistanceManager.LoadData();

                foreach (var game in data.Item1)
                {
                    Games.Add(game);
                }

                foreach (var user in data.Item2)
                {
                    Users.Add(user);
                }

                foreach (var chessboard in data.Item3)
                {
                    Chessboards.Add(chessboard);
                }

            }
        }

        public void SaveData()
        {
            persistanceManager?.SaveData(Games, Users, Chessboards);
        }
    }
}
