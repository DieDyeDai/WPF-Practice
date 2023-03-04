using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace WpfApp1
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {

        private void Application_Startup(object sender, StartupEventArgs e)
        {
            MainWindow mainWindow = new MainWindow();
            mainWindow.Title = "Test";
            if (e.Args.Length > 0) {
                MessageBox.Show(e.Args.ToString());
            } else
            {
                MessageBox.Show("no args");
            }
            mainWindow.Show();
        }
    }
}
