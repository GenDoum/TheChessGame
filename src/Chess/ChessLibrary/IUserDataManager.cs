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
    public interface IUserDataManager
    {
        public void WriteUsers(List<User> users);

        public List<User> ReadUsers();
    }
}
