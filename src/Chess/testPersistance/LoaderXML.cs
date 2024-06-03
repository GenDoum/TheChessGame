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

        public void WriteUsers(List<User> users)
        {
            const string xmlFile = "User.xml";

            if (users == null)
            {
                throw new ArgumentNullException(nameof(users));
            }

            var serializer = new DataContractSerializer(typeof(List<User>));
            var settings = new XmlWriterSettings() { Indent = true };

            if (File.Exists(xmlFile))
            {
                File.Delete(xmlFile);
            }

            using(TextWriter tw = File.CreateText(xmlFile))
            {
                using (var writer = XmlWriter.Create(tw, settings))
                {
                    serializer.WriteObject(writer, users);
                }
            }


        }

        public List<User> ReadUsers()
        {

            Directory.SetCurrentDirectory(Path.Combine(Directory.GetCurrentDirectory(), "..", "..", "testPersistance", "donneePersistance"));            
            const string xmlFile = "User.xml";


            var serializer = new DataContractSerializer(typeof(List<User>));
            var settings = new XmlReaderSettings { IgnoreWhitespace = true };
            using (TextReader tr = File.OpenText(xmlFile))
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
