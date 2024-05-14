// See https://aka.ms/new-console-template for more information
using System;
using System.Reflection.Metadata;
using ChessLibrary;
using System.Linq;
using System.Text;


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

            if (Equals(user.Password, null))
            {
                Console.WriteLine("\nInvited player, no need to check password\n");
                return true;
            }
            if (user.Password.Equals(pass.ToString()))
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


        public static void menuAccueil()
        {
            int choix;

            string? pseudo = null;

            Color noir = Color.Black;
            Color blanc = Color.White;

            User balko = new User("MatheoB", "chef", noir, false, 0);
            User hersan = new User("MatheoH", "proMac", blanc, false, 0);

            User? playerOne = null;
            User? playerTwo = null;

            List<User> users = new List<User>();
            users.Add(hersan);
            users.Add(balko);

            Console.ResetColor();
            do
            {
                choix = MultipleChoice("Bienvenue sut The Chess", true, "Connexion", "Inscription", "Lancer une partie en tant qu'invités", "Afficher les joueurs", "Quittez l'application");
                switch (choix)
                {
                    case -1:
                        Console.Clear();
                        exitApplication();
                        break;

                    case 0:
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

                    case 1:
                        Console.Clear();
                        Console.Write("Inscription");
                        Thread.Sleep(1000);
                        inscription(users);

                        break;

                    case 2:
                        Console.Clear();
                        Console.Write("Lancer un partie en tant qu'invité");
                        Thread.Sleep(1000);
                        break;

                    case 3:
                        Console.Clear();
                        Console.WriteLine("Afficher les joueurs");
                        foreach (User u in users)
                        {
                            Console.WriteLine(u.Pseudo);
                        }
                        Thread.Sleep(10000);
                        break;

                    case 4:
                        Console.Clear();
                        exitApplication();
                        return;

                    default:
                        exitApplication();
                        return;
                }
            } while (choix != -1);

        }

        static void Main(string[] args)
        {
            menuAccueil();
        }
    }

}

