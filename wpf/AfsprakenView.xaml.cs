using dal.interfaces;
using dal.repositories;
using models;
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
    /// Interaction logic for AfsprakenView.xaml
    /// </summary>
    public partial class AfsprakenView : UserControl
    {
        private IAfsprakenRepository afsprakenRepository = new AfsprakenRepository();
        private IDoktersRepository dokterRepository = new DoktersRepository();
        private IKlantenRepository klantRepository = new KlantenRepository();
        Dokter dokter = new Dokter();
        public AfsprakenView()
        {
            InitializeComponent();
            cmbDokters.ItemsSource = dokterRepository.OphalenDokters();
            cmbKlanten.ItemsSource = klantRepository.OphalenKlanten();
        }
        private void btnZoekAfspraken_Click(object sender, RoutedEventArgs e)
        {
            datagridAfspraken.ItemsSource = afsprakenRepository.OphalenAfspraken();
        }
        private void btnZoekAfsprakenViaDokters_Click(object sender, RoutedEventArgs e)
        {
            if(cmbDokters.SelectedItem is Dokter dokter)
            {
                datagridAfspraken.ItemsSource = afsprakenRepository.OphalenAfsprakenViaDokter_Id(dokter.Id);
            }
            else
            {
                MessageBox.Show("Eerst een dokter kiezen");
            }
        }
        private void btnZoekAfsprakenViaKlanten_Click(object sender, RoutedEventArgs e)
        {
            if (cmbKlanten.SelectedItem is Klant klant)
            {
                datagridAfspraken.ItemsSource = afsprakenRepository.OphalenAfsprakenViaKlant_Id(klant.Id);
            }
            else
            {
                MessageBox.Show("Eerst een klant kiezen");
            }
        }
        private void btnZoekAfsprakenViaDoktersEnKlanten_Click(object sender, RoutedEventArgs e)
        {
            int dokterId;
            int klantId;
            if(cmbDokters.SelectedItem is Dokter dokter)
            {
                dokterId = dokter.Id;
            }
            else
            {
                MessageBox.Show("Eerst een dokter kiezen");
                return;
            }
            if(cmbKlanten.SelectedItem is Klant klant)
            {
                klantId = klant.Id;
            }
            else
            {
                MessageBox.Show("Eerst een klant kiezen");
                return;
            }
            datagridAfspraken.ItemsSource = afsprakenRepository.OphalenAfsprakenViaKlant_IdEnDokter_Id(dokterId, klantId);
        }
    }
}
