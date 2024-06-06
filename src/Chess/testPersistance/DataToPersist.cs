using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ChessLibrary;

namespace Persistance
{
    public class DataToPersist
    {
        public ObservableCollection<Game> games { get; set; } = new ObservableCollection<Game>();

        public ObservableCollection<User> players { get; set; } = new ObservableCollection<User>();
    }
}
