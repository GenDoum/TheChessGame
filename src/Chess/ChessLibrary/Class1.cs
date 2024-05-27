using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace ChessLibrary
{
    public class UsersManager
    {
        [DataMember]
        public ReadOnlyCollection<User> Users { get; private set; }
        private readonly List<User> users = new List<User>();
        
        public UsersManager()
        {
            Users = new ReadOnlyCollection<User>(users);
        }

        public void AddUser(User user)
        {
            users.Add(user);
        }

        [OnDeserialized]
        private void OnDeserialized(StreamingContext sc = new StreamingContext() )
        {
            Users = new ReadOnlyCollection<User>(users);
        }
    }
}