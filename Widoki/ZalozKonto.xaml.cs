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
    /// Interaction logic for ZalozKonto.xaml
    /// </summary>
    public partial class ZalozKonto : Window
    {
        public ZalozKonto()
        {
            InitializeComponent();
        }

        private void Zaloguj_Click(object sender, RoutedEventArgs e)
        {
            var zaloguj = new Zaloguj();
            zaloguj.Show();
            Close();
        }

        private void ZalozKonto_Click(object sender, RoutedEventArgs e)
        {
            var zaloguj = new Zaloguj();
            zaloguj.Show();
            Close();
        }
    }
}
