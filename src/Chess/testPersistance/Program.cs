// See https://aka.ms/new-console-template for more information
using System.Runtime.Serialization;
using ChessLibrary;
using System.Xml;
using System.Security.Cryptography;
using ConsoleChess;
using Persistance;


static void XML()
{
    var serializer = new DataContractSerializer(typeof(User)); // , new DataContractSerializerSettings {PreserveObjectReferences = true} pour eviter les boucles infinie et les duplications d'objets

    XmlWriterSettings settings = new XmlWriterSettings() { Indent = true };

    // Crée un dossier où les données seront stockées
    Directory.CreateDirectory("..\\..\\..\\.\\donneePersistance");

    // Change le dossier courant pour le dossier où les données seront stockées
    Directory.SetCurrentDirectory(Path.Combine(Directory.GetCurrentDirectory(), "..\\..\\..\\.", "donneePersistance"));

    // Variavle qui contient le nom du fichier xml
    string xmlFile = "User.xml";

    // Variable qui contient le nom du fichier xml d'une liste de users
    string xmlFileListUser = "ListUser.xml";

    // Création des users à serialiser
    User balko = new User("Balko", "mdp", Color.White, false, 100);
    User hersan = new User("Hersan", "mdp", Color.Black, false, 99);


    // Liste de users pour tester la serialisation de collection
    List<User> users = new List<User>
    {
        balko,
        hersan
    };

    var serializerListUser = new DataContractSerializer(typeof(List<User>));

    using (TextWriter tw = File.CreateText(xmlFileListUser))
    using (XmlWriter writer = XmlWriter.Create(tw, settings))
    {
        serializerListUser.WriteObject(writer, users);
    }

    User playersRead = new User();

    Console.WriteLine(balko.Pseudo);
    Console.WriteLine(hersan.Pseudo);

    // Crée un fichier xml et écrit les données dedans avec les settings créés au dessus
    using (TextWriter tw = File.CreateText(xmlFile))
    {
        using (XmlWriter writer = XmlWriter.Create(tw, settings))
        {
            serializer.WriteObject(writer, balko);
        }
    }

    using (Stream s = File.OpenRead(xmlFile))
    {
        playersRead = serializer.ReadObject(s) as User;
    }

    Console.WriteLine(playersRead.Pseudo);

    Thread.Sleep(1000);
}

static void JsonSerializationDemo()
{
    // Création de l'instance de LoaderJSON
    LoaderJson loader = new LoaderJson();

    // Création des utilisateurs pour les tests
    User user1 = new User("Balko", "mdp", Color.White, false, 100);
    User user2 = new User("Hersan", "mdp", Color.Black, false, 99);

    // Liste des utilisateurs
    List<User> users = new List<User> { user1, user2 };

    // Écriture des utilisateurs dans un fichier JSON
    try
    {
        loader.writeUsers(users);
        Console.WriteLine("Les utilisateurs ont été écrits dans le fichier JSON avec succès.");
    }
    catch (Exception ex)
    {
        Console.WriteLine($"Erreur lors de l'écriture des utilisateurs : {ex.Message}");
    }

    // Lecture des utilisateurs depuis le fichier JSON
    try
    {
        List<User> loadedUsers = loader.readUsers();
        Console.WriteLine("Les utilisateurs ont été lus depuis le fichier JSON avec succès.");
        foreach (User user in loadedUsers)
        {
            Console.WriteLine($"Pseudo: {user.Pseudo}, Couleur: {user.Color}");
        }
    }
    catch (Exception ex)
    {
        Console.WriteLine($"Erreur lors de la lecture des utilisateurs : {ex.Message}");
    }
}


static void Main()
{
    JsonSerializationDemo();
}


Main();