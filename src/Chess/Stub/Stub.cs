using ChessLibrary;
using System.Collections.ObjectModel;

namespace Stub
{
    public class Stub : IPersistanceManager
    {
        public (ObservableCollection<Game>, ObservableCollection<User>, ObservableCollection<Chessboard>) LoadData()
        {
            ObservableCollection<Game> games = new ObservableCollection<Game>();
            ObservableCollection<User> players = new ObservableCollection<User>();
            ObservableCollection<Chessboard> chessboards = new ObservableCollection<Chessboard>();

            Game TheGame = new Game();

            User user1 = new User("Yannis", "mdp", ChessLibrary.Color.White, false, 1000);
            User user2 = new User("Hersan", "mdp", ChessLibrary.Color.White, false, 0);

            Chessboard board = new Chessboard();


            players.Add(user1); players.Add(user2); 

            games.Add(TheGame);

            chessboards.Add(board);

            return (games, players, chessboards);

        }

        public void SaveData(ObservableCollection<Game> games, ObservableCollection<User> players, ObservableCollection<Chessboard> chessboards)
        {
            throw new NotImplementedException();
        }
    }
}
