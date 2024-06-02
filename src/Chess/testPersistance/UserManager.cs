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

        private readonly LoaderXML loaderXML = new LoaderXML();
        private readonly LoaderJson loaderJson = new LoaderJson();


        public UserManager()
        {

        }

        public override List<User> readUsers()
        {
            // /!\ Pour activer la lecture du fichier en XML, décommenter la ligne suivante /!\
            // List<User>? allUsers = loaderXML.readUsers();!

            List<User>? allUsers = loaderJson.readUsers();

            return allUsers!;
        }
        public override void writeUsers(List<User> users)
        {
            loaderJson.writeUsers(users);
            loaderXML.writeUsers(users);
        }
    }
}
