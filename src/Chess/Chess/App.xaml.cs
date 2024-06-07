using ChessLibrary;
using Persistance;

namespace Chess
{
    public partial class App : Application
    {
        public string FileName { get; set; } = "data.xml";

        public string FilePath { get; set; } = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "ChessData");

        public Manager MyManager { get; private set; } = new Manager(new Stub.Stub());


        public App()
        {
            InitializeComponent();

            if (File.Exists(Path.Combine(FilePath, FileName)))
            {
                MyManager = new Manager(new Persistance.LoaderXML());
            }

            MyManager.LoadData();

            MainPage = new AppShell();

            if (!File.Exists(Path.Combine(FilePath, FileName)))
            {
                MyManager.persistanceManager = new Persistance.LoaderXML();
            }

            MyManager.SaveData();
        }

        public override void CloseWindow(Window window)
        {
            base.CloseWindow(window);
            MyManager.SaveData();
        }
    }
}
