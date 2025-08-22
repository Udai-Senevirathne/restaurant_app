using System.Configuration;
using System.Data;
using System.Windows;

namespace restaurent_app
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private void Application_Startup(object sender, StartupEventArgs e)
        {
            // Show splash screen first
            var splashScreen = new POSSystem.SplashScreen();
            splashScreen.Show();
        }
    }
}
