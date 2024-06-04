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
        public void WriteUsers(List<User> users)
        {
            DataContractJsonSerializer serializer = new DataContractJsonSerializer(typeof(List<User>));
            
            MemoryStream stream = new MemoryStream();
            
            serializer.WriteObject(stream, users);
            using(FileStream MemStream = File.Create("User.json"))
            {
                stream.WriteTo(stream);
            }
        }

        public List<User> ReadUsers()
        {
            Directory.SetCurrentDirectory(Path.Combine(Directory.GetCurrentDirectory(), "..", "..", "..", "..", "testPersistance", "donneePersistance"));
            Console.WriteLine();
            
            List<User> users;
            DataContractJsonSerializer serializer = new DataContractJsonSerializer(typeof(List<User>));
            using(FileStream MemStream = File.OpenRead("User.json"))
            {
                users = serializer.ReadObject(MemStream) as List<User>;
            }

            return users;
        }
    }
}