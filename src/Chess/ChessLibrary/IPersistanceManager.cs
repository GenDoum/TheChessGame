using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChessLibrary
{
    /// <summary>
    /// Interface pour la gestion de la persistance
    /// </summary>
    public interface IPersistanceManager
    {
        (ObservableCollection<Game>, ObservableCollection<User>) LoadData();

        void SaveData(ObservableCollection<Game> games, ObservableCollection<User> players);
    }
}
