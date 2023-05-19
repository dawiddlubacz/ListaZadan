using ListaZadan.BazaDanych;
using ListaZadan.Modele;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace ListaZadan
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        Context Context;

        public MainWindow(Context Context)
        {
            this.Context = Context;
            InitializeComponent();
            WyswietlZadania();
            WyswietlKategorie();
        }

        private void WyswietlZadania()
        {
            var listaZadan = Context.Zadania.ToList();
            ListaZadan.ItemsSource = listaZadan;
        }

        private void WyswietlKategorie()
        {
            var listaKategorii = Context.Kategorie.ToList();
            ListaKategorii.ItemsSource = listaKategorii;
            WybierzKategorie.ItemsSource = listaKategorii;
            ListaKategorii.DisplayMemberPath = "Nazwa";
            WybierzKategorie.DisplayMemberPath = "Nazwa";
        }

        private void DodajZadaniePole_KeyDown(object sender, KeyEventArgs e)
        {
            var kategoria = WybierzKategorie.SelectedItem as Kategoria;
            var kategoriaId = kategoria.KategoriaId;

            if (e.Key == Key.Enter)
            {
                var zadanie = new Zadanie
                {
                    Nazwa = DodajZadaniePole.Text,
                    KategoriaId = kategoriaId
                };

                Context.Zadania.Add(zadanie);
                Context.SaveChanges();
                WyswietlZadania();
                DodajZadaniePole.Text = "";
            }
        }

        private void DodajZadaniePole_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            DodajZadaniePole.Text = "";
        }

        private void DodajKategoriePole_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            DodajKategoriePole.Text = "";
        }

        private void DodajKategoriePole_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                var kategoria = new Kategoria
                {
                    Nazwa = DodajKategoriePole.Text
                };

                Context.Kategorie.Add(kategoria);
                Context.SaveChanges();
                WyswietlKategorie();
                DodajKategoriePole.Text = "";
            }
        }

        private void PrzyciskUsun_Click(object sender, RoutedEventArgs e)
        {
            var przycisk = sender as Button;

            if (przycisk != null)
            {
                var zad = przycisk.DataContext as Zadanie;
                var zadanie = Context.Zadania.Find(zad.ZadanieId);
                Context.Zadania.Remove(zadanie);
                Context.SaveChanges();
                WyswietlZadania();
            }
        }
    }
}
