// See https://aka.ms/new-console-template for more information
using System.Runtime.Serialization;
using ChessLibrary;
using System.Xml;


static void Main()
{
    
    var serializer = new DataContractSerializer(typeof(User)); // , new DataContractSerializerSettings {PreserveObjectReferences = true} pour eviter les boucles infinie et les duplications d'objets

    XmlWriterSettings settings = new XmlWriterSettings() { Indent = true };

    // Crée un dossier où les données seront stockées
    Directory.CreateDirectory("..\\..\\..\\.\\donneeTestPersistance");

    // Change le dossier courant pour le dossier où les données seront stockées
    Directory.SetCurrentDirectory(Path.Combine(Directory.GetCurrentDirectory(), "..\\..\\..\\.", "donneeTestPersistance"));

    // Variavle qui contient le nom du fichier xml
    string xmlFile = "testUser.xml";

    // Variable qui contient le nom du fichier xml d'une liste de users
    string xmlFileListUser = "testListUser.xml";

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
    using(XmlWriter writer = XmlWriter.Create(tw, settings))
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


Main();