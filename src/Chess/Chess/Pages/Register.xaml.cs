using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Persistance;
using ChessLibrary;

namespace Chess.Pages
{
    /// <summary>
    /// Represents the registration page for new users.
    /// </summary>
    public partial class Register : ContentPage
    {
        /// <summary>
        /// Gets the manager instance from the application.
        /// </summary>
        public Manager MyManager => (App.Current as App)!.MyManager;

        /// <summary>
        /// Initializes a new instance of the <see cref="Register"/> class.
        /// </summary>
        public Register()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Handles the click event for the register button.
        /// Registers a new user after validating the input.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The event data.</param>
        async void OnRegisterButtonClicked(object sender, EventArgs e)
        {
            string pseudo = PseudoEntry.Text;
            string password = PasswordEntry.Text;
            string confirmPassword = ConfirmPasswordEntry.Text;

            // Check if the pseudo already exists
            foreach (User user in MyManager.Users!)
            {
                if (user.Pseudo == pseudo)
                {
                    await DisplayAlert("Erreur", "Ce pseudo est déjà utilisé", "OK");
                    return;
                }
            }

            // Validate input fields
            if (string.IsNullOrWhiteSpace(pseudo) || string.IsNullOrWhiteSpace(password) || string.IsNullOrWhiteSpace(confirmPassword))
            {
                await DisplayAlert("Erreur", "Veuillez remplir tous les champs", "OK");
            }
            else if (password != confirmPassword)
            {
                await DisplayAlert("Erreur", "Les mots de passe ne correspondent pas", "OK");
            }
            else
            {
                // Create and register the new user
                User newUser = new User(pseudo, password, ChessLibrary.Color.White, false, 0);
                MyManager.Users.Add(newUser);
                MyManager.SaveData();
                await DisplayAlert("Succès", "Inscription réussie", "OK");

                // Clear input fields
                PseudoEntry.Text = string.Empty;
                PasswordEntry.Text = string.Empty;
                ConfirmPasswordEntry.Text = string.Empty;

                // Navigate to the main page
                await Shell.Current.GoToAsync("//page/MainPage");
            }
        }

        /// <summary>
        /// Handles the click event for the cancel button.
        /// Navigates back to the main page.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The event data.</param>
        async void OnCancelButtonClicked(object sender, EventArgs e)
        {
            await Shell.Current.GoToAsync("//page/MainPage");
        }
    }
}
