﻿using ListaZadan.BazaDanych;
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
    /// Okno listy wszystkich zadań i kategorii
    /// </summary>
    public partial class MainWindow : Window
    {
        Context Context;

        public MainWindow(Context Context)
        {
            this.Context = Context;
            InitializeComponent();
            ListaKategorii.SelectedIndex = 0;
            WyswietlZadania();
            WyswietlKategorie();
        }

        /// <summary>
        /// Wyświetla wszystkie dostępne zadania
        /// </summary>
        private void WyswietlZadania()
        {
            var kategoria = ListaKategorii.SelectedItem as Kategoria;
            if (kategoria != null)
            {
                if (kategoria.Nazwa == "Wszystko")
                {
                    var wszystkieZadania = Context.Zadania.Select(zadanie => new Zadanie
                    {
                        ZadanieId = zadanie.ZadanieId,
                        Nazwa = zadanie.Nazwa,
                        KategoriaId = zadanie.KategoriaId,
                    }).ToList();

                    ListaZadan.ItemsSource = wszystkieZadania;
                    return;
                }
                var zadania = Context.Zadania.Select(zadanie => new Zadanie
                {
                    ZadanieId = zadanie.ZadanieId,
                    Nazwa = zadanie.Nazwa,
                    KategoriaId = zadanie.KategoriaId,
                }).Where(zadanie => zadanie.KategoriaId == kategoria.KategoriaId).ToList();

                ListaZadan.ItemsSource = zadania;
            }
        }
        /// <summary>
        /// Wyświetla wszystkie dostępne kategorie
        /// </summary>
        private void WyswietlKategorie()
        {
            var listaKategorii = Context.Kategorie.ToList();
            ListaKategorii.ItemsSource = listaKategorii;
            WybierzKategorie.ItemsSource = listaKategorii;
            ListaKategorii.DisplayMemberPath = "Nazwa";
            WybierzKategorie.DisplayMemberPath = "Nazwa";
        }
        /// <summary>
        /// Dodaje nowe zadanie po wciśnięciu klawisza Enter
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
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

                if (Context.Zadania.Any(zad => zad.Nazwa == zadanie.Nazwa))
                {
                    MessageBox.Show("Zadanie już istnieje");
                    return;
                }

                Context.Zadania.Add(zadanie);
                Context.SaveChanges();
                WyswietlZadania();
                DodajZadaniePole.Text = "";
            }
        }
        /// <summary>
        /// Czyści pole z domyślnego tekstu po kliknięciu
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DodajZadaniePole_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (DodajZadaniePole.Text == "Dodaj zadanie...")
                DodajZadaniePole.Text = "";
        }

        /// <summary>
        /// Czyści pole z domyślnego tekstu po kliknięciu
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DodajKategoriePole_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (DodajKategoriePole.Text == "Dodaj kategorie...")
                DodajKategoriePole.Text = "";
        }

        /// <summary>
        /// Dodaje nową kategorię po wciśnięciu klawisza Enter
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DodajKategoriePole_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                var kategoria = new Kategoria
                {
                    Nazwa = DodajKategoriePole.Text,
                };

                if (Context.Kategorie.Any(kat => kat.Nazwa == kategoria.Nazwa))
                {
                    MessageBox.Show("Kategoria już istnieje");
                    return;
                }

                Context.Kategorie.Add(kategoria);
                Context.SaveChanges();
                WyswietlKategorie();
                DodajKategoriePole.Text = "";
            }
        }

        /// <summary>
        /// Usuwa zadanie po kliknięciu przycisku "Usuń"
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void PrzyciskUsun_Click(object sender, RoutedEventArgs e)
        {
            var przycisk = sender as Button;

            if (przycisk != null)
            {
                var zad = przycisk.DataContext as Zadanie;
                if (zad != null)
                {
                    var zadanie = Context.Zadania.Find(zad.ZadanieId);
                    Context.Zadania.Remove(zadanie);
                    Context.SaveChanges();
                    WyswietlZadania();
                }
            }
        }

        /// <summary>
        /// Wyświetla liste zadań dla zaznaczonej kategorii
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ListaKategorii_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            WyswietlZadania();
        }

        /// <summary>
        /// Usuwa zaznaczoną kategorię po podwójnym kliknięciu
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ListaKategorii_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            var kategoria = ListaKategorii.SelectedItem as Kategoria;
            if (kategoria != null)
            {
                if (kategoria.Nazwa == "Wszystko" || kategoria.Nazwa == "Ważne")
                {
                    MessageBox.Show("Nie można usunąć tej kategorii");
                    return;
                }

                var zadania = Context.Zadania.Where(zadanie => zadanie.KategoriaId == kategoria.KategoriaId).ToList();
                foreach (var zadanie in zadania)
                {
                    Context.Zadania.Remove(zadanie);
                }

                Context.Kategorie.Remove(kategoria);
                Context.SaveChanges();
                WyswietlKategorie();
                WyswietlZadania();
            }
        }
    }
}
