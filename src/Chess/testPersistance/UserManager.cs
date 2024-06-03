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
    public class UserManager : IUserDataManager
    {
        private readonly LoaderJson loaderJson = new LoaderJson();

        private readonly LoaderXML loaderXML = new LoaderXML();


        public UserManager()
        {

        }

        public override List<User> ReadUsers()
        {
            // /!\ Pour activer la lecture du fichier en XML, décommenter la ligne suivante /!\
            // List<User>? allUsers = loaderXML.readUsers();!

            List<User>? allUsers = loaderJson.ReadUsers();

            return allUsers!;
        }
        public override void WriteUsers(List<User> users)
        {
            loaderJson.WriteUsers(users);
            loaderXML.WriteUsers(users);
        }
    }
}
