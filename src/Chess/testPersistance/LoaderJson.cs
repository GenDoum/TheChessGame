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
        private readonly string _dataDirectory;
        private const string JsonFileName = "User.json";

        public LoaderJson()
        {
            _dataDirectory = Path.Combine(AppContext.BaseDirectory, "donneePersistance");
            if (!Directory.Exists(_dataDirectory))
            {
                Directory.CreateDirectory(_dataDirectory);
            }
        }

        public  void WriteUsers(List<User> users)
        {
            if (users == null)
            {
                throw new ArgumentNullException(nameof(users));
            }

            string jsonFilePath = Path.Combine(_dataDirectory, JsonFileName);

            DataContractJsonSerializer serializer = new DataContractJsonSerializer(typeof(List<User>));

            if (File.Exists(jsonFilePath))
            {
                File.Delete(jsonFilePath);
            }

            using (FileStream stream = File.Create(jsonFilePath))
            {
                using (var writer = JsonReaderWriterFactory.CreateJsonWriter(stream, Encoding.UTF8, ownsStream: false, indent: true, indentChars: "  "))
                {
                    serializer.WriteObject(writer, users);
                }
            }
        }

        public  List<User>? ReadUsers()
        {
            string jsonFilePath = JsonFileName;

            Directory.SetCurrentDirectory(Path.Combine(_dataDirectory, "..\\..\\..\\..\\..\\..\\..\\", ".\\testPersistance\\donneePersistance\\"));

            List<User> users = new List<User>();

            if (!File.Exists(jsonFilePath))
            {
                using (File.Create(jsonFilePath))
                {
                    // Création du fichier et fermeture immédiate pour éviter les erreurs de lecture.
                }
                return new List<User>();
            }
            else
            {
                DataContractJsonSerializer jsonSerializer = new DataContractJsonSerializer(typeof(List<User>));

                using (FileStream memoryStream = File.OpenRead(jsonFilePath))
                {
                    users = (List<User>)jsonSerializer.ReadObject(memoryStream)!;
                }
            }

            return users;
        }
    }
}