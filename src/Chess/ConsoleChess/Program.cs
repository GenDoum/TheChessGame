using System;
using ChessLibrary;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace ConsoleChess
{
    class Program
    {
        
        public static void exitApplication()
        {
            Console.Clear();
            Console.WriteLine("\n\n\t\t A bientot !");
            Console.WriteLine();
            Thread.Sleep(1000);
        }

        public static void errorMessage(string message)
        {
            Console.WriteLine($"{message}");
        }

        public static void displayTitle(string title, bool clear)
        {
            Console.Clear();
            if (clear)
            {
                System.Console.Clear();
            }
            Console.ForegroundColor = ConsoleColor.Green; //Ecris le titre suivant en vert
            Console.WriteLine();
            Console.WriteLine($"-==============- {title} -==============-");
            Console.WriteLine();
            Console.ResetColor();

        }
        
        

        /// <summary>
        /// Player's method to check the password
        /// </summary>
        /// <returns>It return a boolean which say if the user has entering the good password of not</returns>
        public static bool isPasswdConsole(User user)
        {
            Console.WriteLine($"Hello {user.Pseudo}, Enter your password please");

            ConsoleKeyInfo key;
            key = System.Console.ReadKey(true);
            StringBuilder pass = new StringBuilder();
            while (key.Key != ConsoleKey.Enter)
            {

                if (key.Key != ConsoleKey.Backspace)
                {
                    pass.Append(key.KeyChar);
                    Console.Write("*");
                }
                else if (key.Key == ConsoleKey.Backspace && pass.Length > 0)
                {
                    // Supprime un élément de la liste de char de pass
                    pass.Remove(pass.Length - 1, 1);
                    // Récupère la position du curseur
                    int pos = System.Console.CursorLeft;
                    // Déplace le curseur d'un à gauche
                    System.Console.SetCursorPosition(pos - 1, System.Console.CursorTop);
                    // Remplace par un espace dans la console
                    System.Console.Write(" ");
                    // Déplace le curseur d'une position à gauche encore
                    System.Console.SetCursorPosition(pos - 1, System.Console.CursorTop);
                }

                key = System.Console.ReadKey(true);

            }

            if (Equals(user.HashPassword(null), user.Password))
            {
                Console.WriteLine("\nInvited player, no need to check password\n");
                return true;
            }

            string? userPassword = user.Password; // pour éviter un code smells
            if (Equals(user.HashPassword(pass.ToString()), userPassword))
            {
                Console.WriteLine($"\nGood password, have fun {user.Pseudo}");
                return true;
            }
            else
            {
                Console.WriteLine($"\nIl semblerait que tu te soit trompé {user.Pseudo}, essaie à nouveau");
                return false;

            }
        }

        public static int MultipleChoice(string title, bool canCancel, params string[] options)
        {

            displayTitle(title, true);
            // Uint = unsigned int -> pour pas que le décalage soit négatif et entraine une exception
            const uint startX = 17; // Décalage à partir de la gauche
            const uint startY = 4;   // Décalage à partir du haut
            const int optionsPerLine = 1;
            const int spacingPerLine = 50;
            int currentSelection = 0;

            ConsoleKey key;

            System.Console.CursorVisible = false;

            do
            {

                for (int i = 0; i < options.Length; i++)
                {
                    System.Console.SetCursorPosition((int)(startX + (i % optionsPerLine) * spacingPerLine), (int)(startY + i / optionsPerLine));

                    if (i == currentSelection)
                        System.Console.ForegroundColor = ConsoleColor.Blue;

                    System.Console.Write(options[i]);

                    System.Console.ResetColor();
                }

                key = System.Console.ReadKey(true).Key;

                switch (key)
                {
                    case ConsoleKey.LeftArrow:
                        {
                            if (currentSelection % optionsPerLine > 0)
                                currentSelection--;
                            break;
                        }
                    case ConsoleKey.RightArrow:
                        {
                            if (currentSelection % optionsPerLine < optionsPerLine - 1)
                                currentSelection++;
                            break;
                        }
                    case ConsoleKey.UpArrow:
                        {
                            if (currentSelection >= optionsPerLine)
                                currentSelection -= optionsPerLine;
                            break;
                        }
                    case ConsoleKey.DownArrow:
                        {
                            if (currentSelection + optionsPerLine < options.Length)
                                currentSelection += optionsPerLine;
                            break;
                        }
                    case ConsoleKey.Escape:
                        {
                            if (canCancel)
                                return -1;
                            break;
                        }
                }
            } while (key != ConsoleKey.Enter);

            System.Console.CursorVisible = true;

            return currentSelection;
        }


        public static string enterStringCheck(string enter)
        {
            Console.Clear();
            string? chaine;
            int nbError = 0;
            Console.WriteLine($"Entrez votre {enter}");
            chaine = Console.ReadLine();

            // Demande le pseudo, laisse trois chance. Si au bout de trois fois
            // le pseudo est toujours vide, on retourne une chaine vide
            while (string.IsNullOrWhiteSpace(chaine) || string.IsNullOrEmpty(chaine))
            {
                ++nbError;

                if (nbError >= 3)
                {
                    Console.WriteLine("Vous avez atteind le nombre maximal de tentative.");
                    Console.WriteLine("Au revoir");
                    Thread.Sleep(800);
                    return "";
                }
                errorMessage($"Le {enter} entré n'est pas correct\nEntrez le à nouveau");

                chaine = Console.ReadLine();
            }
            return chaine;
        }

        public static bool pseudoIsExists(List<User> users, string pseudo)
        {
            return users.Exists(u => u.Pseudo == pseudo);
        }

        public static User? connexion(List<User> users, string pseudo)
        {
            Console.Clear();

            if (string.IsNullOrEmpty(pseudo))
            {
                errorMessage("Pseudo vide");
                return null;
            }

            User? user = users.Find(u => u.Pseudo == pseudo);

            if (user == null)
            {
                errorMessage($"{pseudo} n'existe pas");
                Thread.Sleep(1000);
                return null;
            }

            if (user.IsConnected)
            {
                errorMessage($"{pseudo} est déjà connecté");
                Thread.Sleep(1000);
                return null;
            }

            user.IsConnected = isPasswdConsole(user);

            if (!user.IsConnected)
            {
                return null;
            }            

            Thread.Sleep(2000);
            return user;
        }

        // Inutile, à supprimer
        public static bool checkUserConnection(User user)
        {

            if (Equals(user, null))
            {
                Console.WriteLine("La connexion n'a pas marché, connecter vous à nouveau");
                Thread.Sleep(1000);
                return false;
            }
            return true;
        }


        public static List<User> inscription(List<User> users)
        {
            string pseudo = "";
            string psswd = "";

            pseudo = enterStringCheck("pseudo");

            if (!users.Exists(u => u.Pseudo == pseudo))
            {
                if (!string.IsNullOrEmpty(pseudo))
                {
                    psswd = enterStringCheck("Mot de passe");
                    User user = new User(pseudo, psswd, Color.White, false, 0);
                    users.Add(user);
                }


                return users;
            }
            errorMessage("Pseudo déjà existant, recommener l'opération.");
            return users;
        }

        public static void displayLeaderboard(List<User> users)
        {
            

            Console.Clear();
            displayTitle("Leaderboard", false);
            Console.WriteLine();
            foreach (User user in users.OrderByDescending(u => u.Score))
            {
                Console.WriteLine($"\t{user.Pseudo} : {user.Score}");
            }
            Console.WriteLine();

            while (true)
            {
                Console.WriteLine("Press 'q' to quit");
                if (Console.ReadKey().Key == ConsoleKey.Q)
                {
                    break;
                }
            }
        }   

        /// <summary>
        /// Menu de connexion pour le deuxième joueur
        /// </summary>
        /// <param name="u1"></param>
        /// <param name="users"></param>
        /// <returns></returns>
        public static User? menuConnexionDeuxJoueurs(User u1, List<User> users)
        {
            int choix;
            User defaultUser = new User();


            do
            {

                choix = MultipleChoice($"{u1.Pseudo} est connecté, que souhaité vous faire ?", true, "Connecter un deuxième joueur", "Deuxième joueur invité", "Annuler et quitter");

                switch (choix)
                {
                    case -1:
                        break;


                    case 0:
                        string pseudoUser2 = enterStringCheck("Pseudo");
                        User? user2 = connexion(users, pseudoUser2);
                        return user2;

                    case 1:
                        return defaultUser;

                    default:
                        return defaultUser;
                }



            } while (choix != -1);

            return defaultUser;
        }

//        public static void

        /// <summary>
        /// Fonction qui gère l'accueil de l'application et la gestion des joueurs
        /// </summary>
        /// <param name="playerOne"></param>
        /// <param name="playerTwo"></param>
        public static void menuAccueil(User? playerOne, User? playerTwo)
        {
            int choix;

            string? pseudo = null;

            Color noir = Color.Black;
            Color blanc = Color.White;

            User balko = new User("MatheoB", "chef", blanc, false, 25);
            User hersan = new User("MatheoH", "proMac", noir, false, 10);


            List<User> users = new List<User>();
            users.Add(hersan);
            users.Add(balko);

            Console.ResetColor();
            do
            {
                choix = MultipleChoice("Welcome on The Chess", true, "Connection", "Inscription", "Start a game", "Learderboard", "Exit application");

                switch (choix)
                {
                    case -1: // Option pour quitter l'application
                        Console.Clear();
                        exitApplication();
                        break;

                    case 0: // Option pour se connecter
                        Console.Clear();
                        pseudo = enterStringCheck("pseudo");
                        playerOne = connexion(users, pseudo);
                        if (Equals(playerOne, null))
                            break;

                        if (playerOne.IsConnected)
                        {
                            playerTwo = menuConnexionDeuxJoueurs(playerOne, users);
                        }
                        break;

                    case 1: // Option pour s'inscrire
                        Console.Clear();
                        Console.Write("Inscription");
                        Thread.Sleep(1000);
                        inscription(users);

                        break;

                    case 2:  // Option pourl ancer une partie
                        Console.Clear();
                        Console.WriteLine("Lancer un partie");
                        Thread.Sleep(1000);
                        if (playerOne == null || playerTwo == null)
                        {
                            errorMessage("Vous devez être connecté pour lancer une partie");
                            choix = MultipleChoice("Welcome on The Chess", true, "Connection", "Inscription", "Start a game", "Learderboard", "Exit application");
                            continue;
                        }
                        if (playerOne.Color == Color.White)
                        {
                            playerTwo.Color = Color.Black;
                        }
                        if(playerOne.Color == Color.Black)
                        {
                            playerOne.Color = Color.White;
                            playerTwo.Color = Color.Black;
                        }
                        Jeu(playerOne, playerTwo);
                        break;

                    case 3: // Option pour afficher le leaderboard
                        Console.Clear();
                        Console.WriteLine("Leaderboard");
                        Thread.Sleep(1000);
                        displayLeaderboard(users);

                        break;

                    case 4: // Option pour quitter l'application
                        Console.Clear();
                        exitApplication();
                        return;

                    default: 
                        exitApplication();
                        return;
                }
            } while (choix != -1);

        }

        static void Jeu(User? player1, User? player2)
        {
            Game game = new Game(player1, player2);

            game.EvolveNotified += (sender, args) =>
            {
                ChoiceUser choice = GetUserChoice();
                Evolve(game, args.Pawn, args.Case, choice);
            };

            game.GameOverNotified += (sender, args) =>
            {
                Console.WriteLine($"Game over! {args.Winner.Pseudo} wins!");
                args.Winner.Score += 5;
                Thread.Sleep(150000);
            };

            int player = 1;
            bool isGameOver = false;
            DisplayBoard(game.Board);

            while (!isGameOver)
            {
                User actualPlayer = (player % 2 == 0) ? player2 : player1;
                Console.WriteLine($"{actualPlayer.Pseudo}'s turn");

                bool validMove = false;
                while (!validMove)
                {
                    try
                    {
                        (int startColumn, int startRow) = GetMoveCoordinates("Enter the position of the piece you want to move (a1, f7 ...):");
                        (int endColumn, int endRow) = GetMoveCoordinates("Enter the destination position (a1, f7 ...):");

                        Case startCase = game.Board.Board[startColumn, startRow];
                        Case endCase = game.Board.Board[endColumn, endRow];
                        Piece? piece = startCase.Piece;

                        if (piece == null || piece.Color != actualPlayer.Color)
                        {
                            Console.WriteLine("Invalid move: No piece or wrong color.");
                            continue; // Retry the same player's turn
                        }

                        bool isValidMove = game.Board.CanMovePiece(piece, startCase, endCase);

                        if (!isValidMove)
                        {
                            Console.WriteLine("Invalid move: Check the rules.");
                            continue; // Retry the same player's turn
                        }

                        game.MovePiece(startCase, endCase, game.Board, actualPlayer);
                        DisplayBoard(game.Board);

                        if (game.Board.IsInCheck(actualPlayer.Color == Color.White ? Color.Black : Color.White))
                        {
                            Console.WriteLine("You are in check");
                            if (game.Board.EchecMat(game.Board.FindKing(actualPlayer.Color), game.Board.FindCase(game.Board.FindKing(actualPlayer.Color))))
                            {
                                isGameOver = true;
                                Console.WriteLine($"Game over! {actualPlayer.Pseudo} loses!");
                            }
                        }

                        validMove = true; // Move was successful, exit the inner loop
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine($"Error: {e.Message}");
                    }
                }
                game.GameOver(player % 2 == 0 ? player1 : player2);
                player++;
            }

        }



        static void Main()
        {


            User player1 = new User(Color.White);
            User player2 = new User(Color.Black);

            menuAccueil(player1, player2);


        }


        static (int, int) GetMoveCoordinates(string prompt)
        {
            while (true)
            {
                try
                {
                    Console.WriteLine(prompt);
                    string pos = Console.ReadLine();
                    return ParseChessNotation(pos);
                }
                catch (Exception e)
                {
                    Console.WriteLine($"Invalid input: {e.Message}. Please try again.");
                }
            }
        }

        /// <summary>
        /// Change a Pawn to another piece.
        /// </summary>
        /// <param name="P"></param>
        /// <param name="C"></param>
        static void Evolve(Game game, Pawn? P, Case C, ChoiceUser choiceUser)
        {
            Queen newQueen;
            Rook newRook;
            Knight newKnight;
            Bishop newBishop;

            switch (choiceUser)
            {
                case ChoiceUser.Queen:
                    newQueen = new Queen(P.Color, P.Id);
                    C.Piece = newQueen;
                    game.Board.ModifPawn(P, newQueen, C);
                    return;

                case ChoiceUser.Rook:
                    newRook = new Rook(P.Color, P.Id);
                    C.Piece = newRook;
                    game.Board.ModifPawn(P, newRook, C);
                    return;
                case ChoiceUser.Bishop:
                    newBishop = new Bishop(P.Color, P.Id);
                    C.Piece = newBishop;
                    game.Board.ModifPawn(P, newBishop, C);
                    return;
                case ChoiceUser.Knight:
                    newKnight = new Knight(P.Color, P.Id);
                    C.Piece = newKnight;
                    game.Board.ModifPawn(P, newKnight, C);
                    return;
                default:
                    break;
            }
        }

        static ChoiceUser GetUserChoice()
        {
            int choice;
            do
            {
                Console.WriteLine("Choose the piece to evolve to:");
                Console.WriteLine("1. Queen");
                Console.WriteLine("2. Rook");
                Console.WriteLine("3. Bishop");
                Console.WriteLine("4. Knight");

                if (!int.TryParse(Console.ReadLine(), out choice) || choice < 1 || choice > 4)
                {
                    Console.WriteLine("Invalid input. Please enter a number between 1 and 4.");
                }
            } while (choice < 1 || choice > 4);

            return (ChoiceUser)choice;
        }

        static void DisplayBoard(Chessboard chessboard)
        {
            Console.WriteLine("   a   b   c   d   e   f   g   h");
            Console.WriteLine(" +---+---+---+---+---+---+---+---+");
            for (int row = 0; row < 8; row++)
            {
                Console.Write((8 - row) + " |");
                for (int column = 0; column < 8; column++)
                {
                    var square = chessboard.Board[column, row];
                    string pieceSymbol = square?.Piece != null ? GetPieceSymbol(square.Piece) : " ";
                    ConsoleColor originalColor = Console.ForegroundColor;
                    if (square?.Piece != null && square.Piece.Color != Color.White)
                    {
                        Console.ForegroundColor = ConsoleColor.DarkMagenta;
                    }
                    Console.Write($" {pieceSymbol} ");
                    Console.ForegroundColor = originalColor;
                    Console.Write("|");
                }
                Console.WriteLine($" {8 - row}");
                Console.WriteLine(" +---+---+---+---+---+---+---+---+");
            }
            Console.WriteLine("   a   b   c   d   e   f   g   h");
        }

        static string GetPieceSymbol(Piece piece)
        {
            return piece.GetType().Name switch
            {
                "Pawn" => "P",
                "Rook" => "R",
                "Knight" => "C",
                "Bishop" => "B",
                "Queen" => "Q",
                "King" => "K",
                _ => "?",
            };
        }

        static (int column, int row) ParseChessNotation(string notation)
        {
            if (notation.Length != 2 || notation[0] < 'a' || notation[0] > 'h' || notation[1] < '1' || notation[1] > '8')
            {
                throw new ArgumentException("Invalid chess notation.");
            }

            int column = notation[0] - 'a';
            int row = 8 - (notation[1] - '0');

            return (column, row);
        }
    }
}