using ChessLibrary;
using System.Collections.ObjectModel;

namespace Stub
{
    public class Stub : IPersistanceManager
    {
        public (ObservableCollection<Game>, ObservableCollection<User>) LoadData()
        {
            ObservableCollection<Game> games = new ObservableCollection<Game>();
            ObservableCollection<User> players = new ObservableCollection<User>();

            Game TheGame = new Game();

            User user1 = new User("Yannis", "mdp", ChessLibrary.Color.White, false, 1000);
            User user2 = new User("Hersan", "mdp", ChessLibrary.Color.White, false, 0);
            User user3 = new User("Yannis", "mdp", ChessLibrary.Color.White, false, 10);
            User user4 = new User("Yannis", "mdp", ChessLibrary.Color.White, false, 3000);
            User user5 = new User("Yannis", "mdp", ChessLibrary.Color.White, false, 5000);
            User user6 = new User("Yannis", "mdp", ChessLibrary.Color.White, false, 6000);


            players.Add(user1); players.Add(user2); players.Add(user3); players.Add(user4); players.Add(user5); players.Add(user6);

            games.Add(TheGame);

            return (games, players);

        }

        public void SaveData(ObservableCollection<Game> games, ObservableCollection<User> players)
        {
            throw new NotImplementedException();
        }
    }
}
