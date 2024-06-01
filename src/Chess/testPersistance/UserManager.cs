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
            // /!\ Pour activer la lecture du fichier en XML, décommenter la ligne suivante /!\
            //List<User>? xmlUsers = loaderXML.readUsers(); // Nullable car il peux ne pas y avoir de joueur (au début)

            List<User>? jsonUsers = loaderJson.readUsers();

            List<User> allUsers = new List<User>();
            // Si on active la lecture des deux fichier (json et xml) décommenter le foreach suivant :  
            allUsers.AddRange(jsonUsers); 

            //foreach (User user in xmlUsers)
            //{
            //    // Regarde dans la liste si le pseudo existe déjà, si il existe pas on ajoute le joueur, sinon on passe
            //    if (allUsers.Find(u => u.Pseudo == user.Pseudo) == null)
            //    {
            //        allUsers.Add(user);
            //    }
            //}

            Users = new ReadOnlyCollection<User>(allUsers);
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

        public void saveUsers(List<User> users)
        {
            loaderJson.writeUsers(users);
            loaderXML.writeUsers(users);
        }
    }
}
