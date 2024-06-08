using Chess.Pages;
using CommunityToolkit.Maui;
using Microsoft.Extensions.Logging;
using Plugin.Maui.Audio;

namespace Chess
{
    /// <summary>
    /// Represents the Maui Program which is the entry point of the application.
    /// </summary>
    public static class MauiProgram
    {
        /// <summary>
        /// Creates and configures the Maui application.
        /// </summary>
        /// <returns>The configured Maui application.</returns>
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();

            builder
                .UseMauiApp<App>()
                .UseMauiCommunityToolkit()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                    fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                    fonts.AddFont("Pacifico.ttf", "Pacifico");
                });

            // Add singleton service for audio management
            builder.Services.AddSingleton(AudioManager.Current);

            // Add transient service for chess board
            builder.Services.AddTransient<chessBoard>();

#if DEBUG
            // Add debug logging in debug mode
            builder.Logging.AddDebug();
#endif

            return builder.Build();
        }
    }
}
