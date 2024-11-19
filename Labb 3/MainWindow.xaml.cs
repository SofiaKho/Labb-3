using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;

namespace Labb3._1
{
    public partial class MainWindow : Window
    {
        private List<Pass> PassLista = new List<Pass>(); // Original lista över alla träningspass
        private List<Pass> FilteredClasses = new List<Pass>(); // Filtrerad lista baserad på sökning

        public MainWindow()
        {
            InitializeComponent();
            InitializePassList();
            UpdateListView(PassLista);
        }
        // Fyller listan med exempeldata för träningspass
        private void InitializePassList()
        {
            PassLista = new List<Pass>
            {
                new Pass { Kategori = "Yoga", Tid = new TimeSpan(9, 0, 0), BokadePlatser = 5, MaxPlatser = 10 },
                new Pass { Kategori = "Spinning", Tid = new TimeSpan(10, 30, 0), BokadePlatser = 8, MaxPlatser = 10 },
                new Pass { Kategori = "Boxning", Tid = new TimeSpan(13, 0, 0), BokadePlatser = 6, MaxPlatser = 10 }
            };
        }
        // Uppdaterar ListView med en given lista
        private void UpdateListView(List<Pass> classes)
        {
            lstClasses.ItemsSource = new List<Pass>(classes);
            lstClasses.Items.Refresh();
        }
        // Sökfunktion baserad på användarinmatning i txtTypeFilter och txtTimeFilter
        private void SearchButton_Click(object sender, RoutedEventArgs e)
        {
            string searchCategory = txtTypeFilter.Text.Trim();
            string searchTime = txtTimeFilter.Text.Trim();

            if (searchCategory == "Typ av träning") searchCategory = "";
            if (searchTime == "Tid") searchTime = "";

            // Försök att omvandla inmatad tid till TimeSpan för filtrering

            TimeSpan selectedTime;
            bool validTime = TimeSpan.TryParse(searchTime, out selectedTime);

            // Använd LINQ för att filtrera PassLista baserat på sökkriterier

            FilteredClasses = PassLista.Where(p =>
                (string.IsNullOrEmpty(searchCategory) || p.Kategori.Equals(searchCategory, StringComparison.OrdinalIgnoreCase)) &&
                (!validTime || p.Tid == selectedTime)
            ).ToList();

            // Uppdatera ListView med filtrerade resultat
            UpdateListView(FilteredClasses);
        }

        // Uppdaterar ListView för att visa alla pass igen
        private void RefreshButton_Click(object sender, RoutedEventArgs e)
        {
            txtTypeFilter.Text = "Typ av träning";
            txtTimeFilter.Text = "Tid";
            UpdateListView(PassLista); // Återställ ListView till att visa alla pass
        }

        // Bokar det valda passet i ListView

        private void BookClassButton_Click(object sender, RoutedEventArgs e)
        {
            if (lstClasses.SelectedItem is Pass selectedClass)
            {
                if (!selectedClass.ÄrFullbokat && selectedClass.BokaPlats())
                {
                    txtConfirmation.Text = $"Bokning lyckades! ({selectedClass.BokadePlatser}/{selectedClass.MaxPlatser} platser bokade)";
                    UpdateListView(FilteredClasses.Any() ? FilteredClasses : PassLista);
                }
                else
                {
                    txtConfirmation.Text = $"Kunde inte boka pass. Det är fullt ({selectedClass.MaxPlatser}/{selectedClass.MaxPlatser} platser bokade).";
                }
            }
            else
            {
                txtConfirmation.Text = "Välj ett pass att boka först.";
            }
        }

        // Avbokar det valda passet i ListView

        private void CancelClassButton_Click(object sender, RoutedEventArgs e)
        {
            if (lstClasses.SelectedItem is Pass selectedClass)
            {
                if (selectedClass.AvbokaPlats())
                {
                    txtConfirmation.Text = $"Avbokning lyckades! ({selectedClass.BokadePlatser}/{selectedClass.MaxPlatser} platser bokade)";
                    UpdateListView(FilteredClasses.Any() ? FilteredClasses : PassLista);
                }
                else
                {
                    txtConfirmation.Text = "Kunde inte avboka passet. Inga bokningar finns.";
                }
            }
            else
            {
                txtConfirmation.Text = "Välj ett pass att avboka först.";
            }
        }
    }
}