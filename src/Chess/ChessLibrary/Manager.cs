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

        public ObservableCollection<Game>? Games;

        public ObservableCollection<User>? Users;

        public ObservableCollection<Game> GamesList
        {
            get
            {
                return Games ??= new ObservableCollection<Game>();
            }
            set
            {
                Games = value;
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

            this.persistanceManager = persistanceManager;
        }

        public Manager()
        {
            Users = new ObservableCollection<User>();
            Games = new ObservableCollection<Game>();
        }

        public void LoadData()
        {
            if(persistanceManager != null)
            {
                var data = persistanceManager.LoadData();

                foreach (var game in data.Item1)
                {
                    GamesList.Add(game);
                }

                foreach (var user in data.Item2)
                {
                    Users.Add(user);
                }
            }
        }

        public void SaveData()
        {
            persistanceManager?.SaveData(GamesList, Users);
        }
    }
}
