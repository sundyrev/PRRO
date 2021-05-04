using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace MainApp
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private MainWindow view;
        private MainVM vm;

        private void OnStartUp(object sender, StartupEventArgs e)
        {
            vm = new MainVM();
            view = new MainWindow()
            {
                DataContext = vm
            };

            view.Show();
        }
    }
}
