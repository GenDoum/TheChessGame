using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Text.Json.Serialization;
using ChessLibrary;

namespace Persistance
{
    public class LoaderJson : IUserDataManager
    {
        private const string DataDirectory = "..\\..\\..\\..\\.\\testPersistance\\donneePersistance";
        private const string JsonFileName = "User.json";

        public override void WriteUsers(List<User> users)
        {
            if (users == null)
            {
                throw new ArgumentNullException(nameof(users));
            }

            string jsonFilePath = Path.Combine("..", "..", "..", "testPersistance", "donneePersistance", JsonFileName);

            DataContractJsonSerializer serializer = new DataContractJsonSerializer(typeof(List<User>));

            if (File.Exists(jsonFilePath))
            {
                File.Delete(jsonFilePath);
            }

            using (FileStream stream = File.Create(JsonFileName))
            {
                using (var writer = JsonReaderWriterFactory.CreateJsonWriter(stream, Encoding.UTF8, ownsStream: false, indent: true, indentChars: "  "))
                {
                    serializer.WriteObject(writer, users);
                }
            }
        }

        public override List<User>? ReadUsers()
        {

            Directory.SetCurrentDirectory(Path.Combine(Directory.GetCurrentDirectory(), "..", "..", "..", "..", "testPersistance", "donneePersistance")); 
            const string jsonFile = "User.json";
            
            List<User> users = new List<User>();
            Thread.Sleep(1000);


            if (!File.Exists(jsonFile))
            {
                using (File.Create(jsonFile))
                {
                    // Création du fichier et fermeture immédiate pour éviter les erreurs de lecture.
                }
                return new List<User>();
            }
            else
            {
                DataContractJsonSerializer jsonSerializer = new DataContractJsonSerializer(typeof(List<User>));

                using (FileStream memoryStream = File.OpenRead("User.json"))
                {
                    users = (List<User>)jsonSerializer.ReadObject(memoryStream)!;
                }
            }

            return users;

        }
    }
}