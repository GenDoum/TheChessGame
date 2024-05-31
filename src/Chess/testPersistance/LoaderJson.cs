using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Json;
using System.Text;
using ChessLibrary;

namespace Persistance
{
    public class LoaderJson : IUserDataManager
    {
        private const string DataDirectory = "..\\..\\..\\..\\.\\testPersistance\\donneePersistance";
        private const string JsonFileName = "testUser.json";

        public override void writeUsers(List<User> users)
        {
            if (users == null)
            {
                throw new ArgumentNullException(nameof(users));
            }

            Directory.CreateDirectory(DataDirectory);
            string jsonFilePath = Path.Combine(DataDirectory, JsonFileName);

            DataContractJsonSerializer serializer = new DataContractJsonSerializer(typeof(List<User>));


            using (FileStream stream = File.Create(JsonFileName))
            {
                using (var writer = JsonReaderWriterFactory.CreateJsonWriter(stream, Encoding.UTF8, ownsStream: false, indent: true, indentChars: "  "))
                {
                    serializer.WriteObject(writer, users);
                }
            }
        }

        public override List<User>? readUsers()
        {
            Directory.SetCurrentDirectory(Path.Combine(Directory.GetCurrentDirectory(), "..\\..\\testPersistance\\donneePersistance"));
            const string jsonFile = "testUser.json";
            Console.WriteLine(Directory.GetCurrentDirectory());
            Thread.Sleep(1000);
            if (!File.Exists(jsonFile))
            {
                throw new FileNotFoundException("Le fichier JSON spécifié est introuvable.");
            }

            var serializer = new DataContractJsonSerializer(typeof(List<User>));
            string json = File.ReadAllText(jsonFile);

            using (var memoryStream = new MemoryStream(Encoding.UTF8.GetBytes(json)))
            {
                return serializer.ReadObject(memoryStream) as List<User>;
            }
        }
    }
}