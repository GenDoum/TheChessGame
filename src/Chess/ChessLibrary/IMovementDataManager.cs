using ChessLibrary;
using System;
using System.Collections.Generic;

namespace Persistance
{
    /// <summary>
    /// Interface for managing movement data.
    /// </summary>
    internal interface IMovementDataManager
    {
        /// <summary>
        /// Writes the user data to the persistence layer.
        /// </summary>
        /// <param name="users">The list of users to be written.</param>
        void WriteUsers(List<User> users);
    }
}
