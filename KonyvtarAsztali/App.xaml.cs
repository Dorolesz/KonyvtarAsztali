using System.Configuration;
using System.Data;
using System.Windows;

namespace KonyvtarAsztali
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);
            {
                if (e.Args.Contains("--stat"))
                {
                    Thread consoleThread = new Thread(new ThreadStart(ConsoleWindow.Run));
                    consoleThread.Start();
                    Shutdown();
                }
                else
                {
                    new MainWindow().Show();
                }
            }
        }
    }

}
