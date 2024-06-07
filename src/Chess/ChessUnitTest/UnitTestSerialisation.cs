using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ChessLibrary;
using Stub;

namespace ChessUnitTest
{
    public class UnitTestSerialisation
    {
        //Compare l'initialisation d'une game normale avec une game initialisée avec la persistance
        [Fact]
        public void TestGameSerialisation()
        {
            // Créer une instance de Game normalement
            Game game1 = new Game();

            // Créer une instance de Game avec la persistance
            Stub.Stub stub = new Stub.Stub();

        }
    }
}
