using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace ChessLibrary
{
    /// <summary>
    /// Interface for managing data persistence.
    /// </summary>
    public interface IPersistanceManager
    {
        /// <summary>
        /// Loads the data from the persistence layer.
        /// </summary>
        /// <returns>
        /// A tuple containing observable collections of games, users, and chessboards.
        /// </returns>
        (ObservableCollection<Game>, ObservableCollection<User>, ObservableCollection<Chessboard>) LoadData();

        /// <summary>
        /// Saves the data to the persistence layer.
        /// </summary>
        /// <param name="games">The observable collection of games to be saved.</param>
        /// <param name="players">The observable collection of players to be saved.</param>
        /// <param name="chessboards">The observable collection of chessboards to be saved.</param>
        void SaveData(ObservableCollection<Game> games, ObservableCollection<User> players, ObservableCollection<Chessboard> chessboards);
    }
}
