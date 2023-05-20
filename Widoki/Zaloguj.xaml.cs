using ListaZadan.BazaDanych;
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
        MainWindow MainWindow;
        ZalozKonto ZalozKonto;
        Context Context;

        public Zaloguj(MainWindow MainWindow)
        {
            this.MainWindow = MainWindow;
            InitializeComponent();
        }
        public Zaloguj(ZalozKonto ZalozKonto, Context Context)
        {
            this.Context = Context;
            this.ZalozKonto = ZalozKonto;
            InitializeComponent();
        }

        public Zaloguj(MainWindow MainWindow, ZalozKonto ZalozKonto, Context Context)
        {
            this.MainWindow = MainWindow;
            this.Context = Context;
            this.ZalozKonto = ZalozKonto;
            InitializeComponent();
        }

        private void ZalozKonto_Click(object sender, RoutedEventArgs e)
        {
            ZalozKonto.Show();
            Hide();
        }

        private void Zaloguj_Click(object sender, RoutedEventArgs e)
        {
            var login = LoginPole.Text;
            var haslo = HasloPole.Password;

            var uzytkownik = Context.Uzytkownicy.FirstOrDefault(uzyt => uzyt.Login == login && uzyt.Haslo == haslo);
            if (uzytkownik == null)
            {
                MessageBox.Show("Niepoprawny login lub hasło");
                return;
            }
            try
            {
                MainWindow.Show();
            }
            catch (Exception)
            {
                var mainWindow = new MainWindow(Context);
                mainWindow.Show();
            }
            Close();
        }
    }
}
