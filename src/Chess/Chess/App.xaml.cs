using ChessLibrary;
using Persistance;

namespace Chess
{
    /// <summary>
    /// Represents the main application class.
    /// </summary>
    public partial class App : Application
    {
        /// <summary>
        /// Gets or sets the name of the data file.
        /// </summary>
        public string FileName { get; set; } = "data.xml";

        /// <summary>
        /// Gets or sets the file path where data will be stored.
        /// </summary>
        public string FilePath { get; set; } = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "ChessData");

        /// <summary>
        /// Gets the manager instance for the application.
        /// </summary>
        public Manager MyManager { get; private set; } = new Manager(new Stub.Stub());

        /// <summary>
        /// Initializes a new instance of the <see cref="App"/> class.
        /// </summary>
        public App()
        {
            InitializeComponent();

            // Check if the data file exists and use the appropriate persistence manager
            if (File.Exists(Path.Combine(FilePath, FileName)))
            {
                MyManager = new Manager(new Persistance.LoaderXML());
            }

            // Load data from the persistence manager
            MyManager.LoadData();

            // Set the main page of the application
            MainPage = new AppShell();

            // Set the persistence manager to XML loader if the data file does not exist
            if (!File.Exists(Path.Combine(FilePath, FileName)))
            {
                MyManager.persistanceManager = new Persistance.LoaderXML();
            }

            // Save the current state of the application data
            MyManager.SaveData();
        }

        /// <summary>
        /// Overrides the CloseWindow method to save data when a window is closed.
        /// </summary>
        /// <param name="window">The window to be closed.</param>
        public override void CloseWindow(Window window)
        {
            base.CloseWindow(window);
            MyManager.SaveData();
        }
    }
}
