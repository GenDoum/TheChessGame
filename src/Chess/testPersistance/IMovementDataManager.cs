using ChessLibrary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistance
{
    internal interface IMovementDataManager
    {
        public abstract void WriteUsers(List<User> users);
    }
}
