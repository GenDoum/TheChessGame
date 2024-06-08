using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ChessLibrary;
using CommunityToolkit.Maui.Views;

namespace Chess.Pages
{
    /// <summary>
    /// Represents the pause page popup.
    /// </summary>
    public partial class pausePage : Popup
    {
        /// <summary>
        /// Gets the manager instance from the application.
        /// </summary>
        Manager Mymanager = (App.Current as App).MyManager;

        /// <summary>
        /// Initializes a new instance of the <see cref="pausePage"/> class.
        /// </summary>
        public pausePage()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Handles the event to continue the game.
        /// Closes the pause popup.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The event data.</param>
        private void OnContinueGame(object sender, EventArgs e)
        {
            Close();
        }

        /// <summary>
        /// Handles the event to quit the game.
        /// Saves the game data and navigates to the main page.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The event data.</param>
        private async void OnQuitGame(object sender, EventArgs e)
        {
            Mymanager.SaveData();
            await Shell.Current.GoToAsync("//MainPage");
            Close();
        }
    }
}
