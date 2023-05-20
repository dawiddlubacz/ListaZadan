using ListaZadan.BazaDanych;
using ListaZadan.Modele;
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
        MainWindow MainWindow;
        Context Context;

        public ZalozKonto(MainWindow MainWindow)
        {
            this.MainWindow = MainWindow;
            InitializeComponent();
        }
        public ZalozKonto(Context Context, MainWindow MainWindow)
        {
            this.MainWindow = MainWindow;
            this.Context = Context;
            InitializeComponent();
        }

        private void Zaloguj_Click(object sender, RoutedEventArgs e)
        {
            var zaloguj = new Zaloguj(this, Context);
            zaloguj.Show();
            Hide();
        }

        private void ZalozKonto_Click(object sender, RoutedEventArgs e)
        {
            var login = LoginPole.Text;
            var haslo = HasloPole.Password;

            var uzytkownik = Context.Uzytkownicy.FirstOrDefault(uzyt => uzyt.Login == login);
            if (uzytkownik != null)
            {
                MessageBox.Show("Użytkownik o podanym loginie już istnieje");
                return;
            }

            Context.Uzytkownicy.Add(new Uzytkownik
            {
                Login = login,
                Haslo = haslo
            });

            Context.SaveChanges();
            MainWindow.Show();
            Close();
        }
    }
}
