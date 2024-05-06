// See https://aka.ms/new-console-template for more information
using System;
using System.Reflection.Metadata;
using ChessLibrary;
using System.Linq;


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
        Console.ResetColor();   //Reset la couleur du texte par défaut (à blanc)
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
        string chaine;
        int nbError = 0;
        Console.WriteLine($"Entrez votre {enter}");
        chaine = Console.ReadLine();

        // Demande le pseudo, laisse trois chance. Si au bout de trois fois
        // le pseudo est toujours vide, on retourne une chaine vide
        while ( string.IsNullOrWhiteSpace(chaine) || string.IsNullOrEmpty(chaine) )
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
            return users.Any(u => u.Pseudo == pseudo);
        }

    public static User connexion( List<User> users, string pseudo)
    {
        Console.Clear();

        User user = new User();

        foreach (User u in users) 
        {
            if ( Equals(u.Pseudo, pseudo) )
            {
                user = u;
            }
        }
        
        if ( string.IsNullOrEmpty(pseudo) )
        {
            errorMessage("Pseudo vide");
            return user;
        }

        if ( !pseudoIsExists(users, pseudo) ) 
        {
            errorMessage($"{pseudo} n'existe pas");
            Thread.Sleep(1000);
            return null;
        }

        user.isConnected = user.isPasswdConsole();
        
        if ( !user.isConnected ) 
        {
            return null;
        }
        Thread.Sleep(2000);
        return user;
    }

    public static bool checkUserConnection (User user)
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

        bool checkPseudo = false;


            pseudo = enterStringCheck("pseudo");

            if (users.Any(u => u.Pseudo == pseudo))
            {
                errorMessage("Pseudo déjà existant, recommener l'opération.");
                return users;
            }
            if ((!string.IsNullOrEmpty(pseudo) || !string.IsNullOrWhiteSpace(pseudo)))
            {
                checkPseudo = true;
            }
            if ( !string.IsNullOrEmpty(pseudo) ) 
            {
                psswd = enterStringCheck("Mot de passe");
                User user = new User(pseudo, psswd, Color.White);
                users.Add(user);
            }

        
        return users;
    }

    public static User menuConnexionDeuxJoueurs(User u1, List<User> users)
    {
        int choix;
        User defaultUser = new User();

        choix = MultipleChoice($"{u1.Pseudo} est connecté, que souhaité vous faire ?", true, "Connecter un deuxième joueur", "Deuxième joueur invité", "Annuler et quitter");


        do
        {

            switch (choix)
            {
                case 0:
                    string pseudoUser2 = enterStringCheck("Pseudo");
                    User user2 = connexion(users, pseudoUser2);
                    return user2;
                
                case 1:
                    return defaultUser;

                default:
                    return defaultUser;
            }

            choix = MultipleChoice($"{u1.Pseudo} est connecté, que souhaité vous faire ?", true, "Connecter un deuxième joueur", "Deuxième joueur invité", "Annuler et quitter");


        } while (choix != -1);

        return defaultUser;

    }


    public static void menuAccueil()
    {
        int choix;

        bool chechPlayer;

        string pseudo = null;
        string enterPseudo = "pseudo";

        Color noir = Color.Black;
        Color blanc = Color.White;

        User balko = new User("MatheoB", "chef", noir);
        User hersan = new User("MatheoH", "proMac", blanc);

        User? playerOne = null;
        User? playerTwo = null;
        User defaultPlayer = new User();

        List<User> users = new List<User>();
        users.Add(hersan);
        users.Add(balko);

        Console.ResetColor();
        do
        {
            choix = MultipleChoice("Bienvenue sut The Chess", true, "Connexion", "Inscription", "Lancer une partie en tant qu'invités", "Afficher les joueurs" , "Quittez l'application");
            switch (choix)
            {
                case -1:
                    Console.Clear();
                    exitApplication();
                    break;

                case 0:
                    Console.Clear();
                    pseudo = enterStringCheck(enterPseudo);
                    playerOne = connexion(users, pseudo);
                    chechPlayer = checkUserConnection(playerOne);
                    if ( Equals(playerOne, null) )
                        break;
                    
                    if (playerOne.isConnected )
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
                    playerOne = new User();
                    playerTwo = new User();
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

