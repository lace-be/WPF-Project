using dal.interfaces;
using dal.repositories;
using models;
using System;
using System.Collections.Generic;
using System.Numerics;
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
    /// Interaction logic for KlantenDierView.xaml
    /// </summary>
    public partial class KlantenDierView : UserControl
    {
        private IKlantenRepository klantenRepository = new KlantenRepository();
        public KlantenDierView()
        {
            InitializeComponent();
        }
        private void btnZoekKlanten_Click(object sender, RoutedEventArgs e)
        {
            lbKlanten.ItemsSource = klantenRepository.OphalenKlantenEnHuisdieren();
            txtKlanten.Text = "Klanten";
        }
        private void btnZoekKlantenViaNaam_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(txtNaam.Text))
            {
                MessageBox.Show("Naam moet ingevuld zijn.");
                return;
            }
            lbKlanten.ItemsSource = klantenRepository.OphalenKlantenViaNaam(txtNaam.Text);
            txtKlanten.Text = "Klanten";
        }
        private void btnZoekKlantenViaPostcode_Click(object sender, RoutedEventArgs e)
        {
            if (!int.TryParse(txtPostcode.Text, out int postcode))
            {
                MessageBox.Show("Postcode moet geldig zijn.");
                return;
            }
            lbKlanten.ItemsSource = klantenRepository.OphalenKlantenViaPostcode(postcode);
            txtKlanten.Text = "Klanten";
        }
        private void btnZoekKlantenViaNaamEnPostcode_Click(object sender, RoutedEventArgs e)
        {
            if (!int.TryParse(txtPostcode.Text, out int postcode) || string.IsNullOrEmpty(txtNaam.Text))
            {
                MessageBox.Show($"Postcode en Naam moet ingevuld zijn.");
                return;
            }
            lbKlanten.ItemsSource = klantenRepository.OphalenKlantenViaNaamEnPostcode(txtNaam.Text, postcode);
            txtKlanten.Text = "Klanten";
        }
        private void lbKlanten_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var selectedItem = lbKlanten.SelectedItem;
            if(selectedItem is Klant)
            {
                var klant = (Klant)selectedItem;
                txtHuisdieren.Text = "Huisdieren";
                lblHuisdieren.Content = klant.HuisdierInfo();
            }
            else
            {
                txtHuisdieren.Text = "";
                lblHuisdieren.Content = null;
            }
        }
    }
}
