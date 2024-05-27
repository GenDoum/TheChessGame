using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;
using System.Xml;
using ChessLibrary;

namespace Persistance
{
    public class LoaderXML : IUserDataManager
    {
        /// <summary>
        /// Ecrit les utilisateurs dans un fichier xml, si la liste est conforme
        /// </summary>
        /// <param name="users"></param>
        /// <exception cref="ArgumentNullException"></exception>
        public override void writeUsers(List<User> users)
        {
            // Vérifie que la liste n'est pas null, comme ça pas besoin de créer un fichier xml vide
            if (users == null)
            {
                throw new ArgumentNullException("users");
            }

            var serializer = new DataContractSerializer(typeof(List<User>));

            XmlWriterSettings settings = new XmlWriterSettings() { Indent = true };

            // Crée un dossier où les données seront stockées
            Directory.CreateDirectory("..\\..\\..\\.\\donneePersistance");

            // Change le dossier courant pour le dossier où les données seront stockées
            Directory.SetCurrentDirectory(Path.Combine(Directory.GetCurrentDirectory(), "..\\..\\..\\.", "donneePersistance"));

            // Variavle qui contient le nom du fichier xml
            string xmlFile = "testUser.xml";
            using (TextWriter tw = File.CreateText(xmlFile))
            using (XmlWriter writer = XmlWriter.Create(tw, settings))
            {
                serializer.WriteObject(writer, users);
            }
        }


        /// <summary>
        /// Fonction qui lit les utilisateurs et les mets au format List<User>
        /// </summary>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public override List<User> readUsers()
        {
            var serializer = new DataContractSerializer(typeof(List<User>));

            XmlReaderSettings settings = new XmlReaderSettings() { IgnoreWhitespace = true };

            // Crée un dossier où les données seront stockées
            Directory.CreateDirectory("..\\..\\..\\.\\donneePersistance");

            // Change le dossier courant pour le dossier où les données seront stockées
            Directory.SetCurrentDirectory(Path.Combine(Directory.GetCurrentDirectory(), "..\\..\\..\\.", "donneePersistance"));

            // Créattion de la liste de users
            List<User> users = new List<User>();

            // Variable qui contient le nom du fichier xml
            string xmlFile = "testUser.xml";
            using (TextReader tr = File.OpenText(xmlFile))
            using (XmlReader reader = XmlReader.Create(tr, settings))
            {
                users = serializer.ReadObject(reader) as List<User>;
            }
            if (users == null)
            {
                throw new Exception("Erreur lors de la lecture du fichier");
            }

            return users;
        }
    }
}
