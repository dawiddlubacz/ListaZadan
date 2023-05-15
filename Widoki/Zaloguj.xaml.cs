using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace ListaZadan.Widoki
{
    /// <summary>
    /// Interaction logic for Zaloguj.xaml
    /// </summary>
    public partial class Zaloguj : Window
    {
        public Zaloguj()
        {
            InitializeComponent();
        }

        private void ZalozKonto_Click(object sender, RoutedEventArgs e)
        {
            var zaloz = new ZalozKonto();
            zaloz.Show();
            Close();
        }

        private void Zaloguj_Click(object sender, RoutedEventArgs e)
        {
            var main = new MainWindow();
            main.Show();
            Close();
        }
    }
}
