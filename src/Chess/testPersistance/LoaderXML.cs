using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization;
using System.Xml;
using ChessLibrary;

namespace Persistance
{
    public class LoaderXML : IUserDataManager
    {

        public override void writeUsers(List<User> users)
        {
            if (users == null)
            {
                throw new ArgumentNullException(nameof(users));
            }

            var serializer = new DataContractSerializer(typeof(List<User>));
            var settings = new XmlWriterSettings() { Indent = true };

            // Crée un dossier où les données seront stockées
            Directory.CreateDirectory("..\\..\\..\\.\\..\\testPersistance\\donneePersistance");

            // Change le dossier courant pour le dossier où les données seront stockées
            Directory.SetCurrentDirectory(Path.Combine(Directory.GetCurrentDirectory(), "..\\..\\..\\..\\testPersistance\\donneePersistance"));


            using(TextWriter tw = File.CreateText("User.xml"))
            {
                using (var writer = XmlWriter.Create(tw, settings))
                {
                    serializer.WriteObject(writer, users);
                }
            }


        }

        public override List<User> readUsers()
        {

            Directory.SetCurrentDirectory(Path.Combine(Directory.GetCurrentDirectory(), "..\\..\\..\\..\\testPersistance\\donneePersistance"));
            const string xmlFile = "User.xml";


            var serializer = new DataContractSerializer(typeof(List<User>));
            var settings = new XmlReaderSettings { IgnoreWhitespace = true };
            using (TextReader tr = File.OpenText("User.xml"))
            {
                using (var reader = XmlReader.Create(tr, settings))
                {
                    var users = serializer.ReadObject(reader) as List<User>;
                    if (users == null)
                    {
                        throw new SerializationException("Erreur lors de la désérialisation des utilisateurs.");
                    }
                    return users;
                }
            }
        }
    }
}
