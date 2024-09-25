using dal.interfaces;
using dal.repositories;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace wpf
{
    /// <summary>
    /// Interaction logic for DoktView.xaml
    /// </summary>
    public partial class DoktView : UserControl
    {
        private IDoktersRepository dokterRepository = new DoktersRepository();
        public DoktView()
        {
            InitializeComponent();
        }

        private void btnZoekDokters_Click(object sender, RoutedEventArgs e)
        {
            datagridDokters.ItemsSource = dokterRepository.OphalenDokters();
        }

        private void btnZoekDoktersViaNaam_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(txtNaam.Text))
            {
                MessageBox.Show("Naam moet ingevuld zijn.");
                return;
            }
            datagridDokters.ItemsSource = dokterRepository.OphalenDoktersViaNaam(txtNaam.Text);
        }

        private void btnZoekDoktersViaVoornaam_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(txtVoornaam.Text))
            {
                MessageBox.Show("Voornaam moet ingevuld zijn.");
                return;
            }
            datagridDokters.ItemsSource = dokterRepository.OphalenDoktersViaVoornaam(txtVoornaam.Text);
        }

        private void btnZoekDoktersViaNaamEnVoornaam_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(txtNaam.Text) || string.IsNullOrEmpty(txtVoornaam.Text))
            {
                MessageBox.Show("Naam en voornaam moeten ingevuld zijn.");
                return;
            }
            datagridDokters.ItemsSource = dokterRepository.OphalenDoktersViaNaamEnVoornaam(txtNaam.Text, txtVoornaam.Text);
        }
    }
}
