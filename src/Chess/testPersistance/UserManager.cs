using ChessLibrary;
using Persistance;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace testPersistance
{
    public class UserManager
    {
        [DataMember]
        public ReadOnlyCollection<User> Users { get; private set; }
        
        private readonly List<User> users = new List<User>();

        private readonly LoaderXML loaderXML = new LoaderXML();
        private readonly LoaderJson loaderJson = new LoaderJson();

        
        public UserManager()
        {
            Console.WriteLine("Création du UserManager");

            List<User>? xmlUsers = loaderXML.readUsers(); // Nullable car il peux ne pas y avoir de joueur (au début)
            //List<User>? jsonUsers = loaderJson.readUsers();

            List<User> allUsers = new List<User>();

            allUsers.AddRange(xmlUsers);
            /*foreach (User user in jsonUsers)
            {
                if (!allUsers.Contains(user))
                {
                    allUsers.Add(user);
                }
            }*/

            Users = new ReadOnlyCollection<User>(allUsers);
            Console.WriteLine("Fin de la création du UserManager");
        }

        public void AddUser(User user)
        {
            users.Add(user);
        }

        [OnDeserialized]
        private void OnDeserialized(StreamingContext sc = new StreamingContext())
        {
            Users = new ReadOnlyCollection<User>(users);
        }

        public void saveUsers (List<User> users)
        {
            loaderJson.writeUsers(users);
            loaderXML.writeUsers(users);
        }
    }
}
