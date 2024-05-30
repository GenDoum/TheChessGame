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
    public abstract class IUserDataManager
    {
        public abstract void writeUsers(List<User> users);

        public abstract List<User> readUsers();
    }
}
