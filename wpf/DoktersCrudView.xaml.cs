using dal.interfaces;
using dal.repositories;
using models;
using System;
using System.Collections.Generic;
using System.Reflection.Metadata;
using System.Security.Policy;
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
    /// Interaction logic for DoktersCrudView.xaml
    /// </summary>
    public partial class DoktersCrudView : UserControl
    {
        private DoktersRepository dokterRepository = new DoktersRepository();
        private AfsprakenRepository afspraakRepository = new AfsprakenRepository();
        public DoktersCrudView()
        {
            InitializeComponent();
            GetData();
        }
        //automatisch in window => label steken 
        private void GetData()
        {
            lbDokters.ItemsSource = dokterRepository.OphalenDokters();
        }
        //Data van geselecteerde vak in invulvakjes steken + selectionchanged
        private void SetData(int dokterId)
        {
            Dokter dok = dokterRepository.OphalenDokterViaPK(dokterId);
            if (dok != null)
            {
                txtId.Text = dokterId.ToString();
                txtEmail.Text = dok.Email;
                txtNaam.Text = dok.Naam;
                txtTel.Text = dok.Tel;
                txtVoornaam.Text = dok.Voornaam;
            }
        }
        private void btnVoegDokterToe_Click(object sender, RoutedEventArgs e)
        {
            //Dit mag weg
            List<Dokter> dokters = dokterRepository.OphalenDokters();
            foreach (Dokter dok in dokters)
            {
                if (dok.Id == int.Parse(txtId.Text))
                {
                    MessageBox.Show("Id al in gebruik");
                    return;
                }
            }
            //Tot hier
            Save(true);
        }
        //Data van geselecteerde vak in invulvakjes steken
        private void lbDokters_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (lbDokters.SelectedItem != null)
            {
                SetData(((Dokter)lbDokters.SelectedItem).Id);
            }
        }
        private void btnVerwijderDokter_Click(object sender, RoutedEventArgs e)
        {
            List<Afspraak> Afspraken = afspraakRepository.OphalenAfspraken();
            //Object mag weg
            List<Dokter> Dokters = dokterRepository.OphalenDokters();
            //
            if (lbDokters.SelectedItem != null)
            {
                foreach(Afspraak Afspraak in Afspraken)
                {
                    if(Afspraak.DokterId == ((Dokter)lbDokters.SelectedItem).Id)
                    {
                        MessageBox.Show("Kan niet verwijderen. Dokter heeft afspraken");
                        return;
                    }
                }
                if (MessageBox.Show("Ben je zeker?", "Bevestiging verwijderen", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                {
                    if (dokterRepository.DeleteDokter(((Dokter)lbDokters.SelectedItem).Id))
                    {
                        MessageBox.Show("Dokter werd verwijderd");
                        GetData();
                    }
                }
            }
            else
            {
                MessageBox.Show("Je moet een dokter selecteren!");
            }
        }
        private void btnWijzigDokter_Click(object sender, RoutedEventArgs e)
        {
            Save(false);
        }
        //WijzigenofToevoegenDokter
        private void Save(bool isInsert)
        {
            //Dit mag weg
            string error = Valideer();
            //
            //Als validatie werkt
            /*
             if (isInsert)
            {
                error += Valideer2();
            }
            error += Valideer();
             */
            if (string.IsNullOrEmpty(error))
            {
                Dokter dok = new Dokter()
                {
                    Id = int.Parse(txtId.Text),
                    Naam = txtNaam.Text,
                    Voornaam = txtVoornaam.Text,
                    Email = txtEmail.Text,
                    Tel = txtTel.Text
                };
                if (isInsert)
                {
                    dokterRepository.InsertDokter(dok);
                }
                else
                {
                    dokterRepository.UpdateDokter(dok);
                }
                GetData();
                ResetForm();
            }
            else
            {
                MessageBox.Show(error);
            }
        }
        private string Valideer()
        {
            string fout = "";
            if (string.IsNullOrEmpty(txtId.Text))
            {
                fout += "Id is verplicht" + Environment.NewLine;
            }
            if (string.IsNullOrEmpty(txtNaam.Text))
            {
                fout += "Naam is verplicht" + Environment.NewLine;
            }
            if (string.IsNullOrEmpty(txtVoornaam.Text))
            {
                fout += "Voornaam is verplicht" + Environment.NewLine;
            }
            if (string.IsNullOrEmpty(txtEmail.Text))
            {
                fout += "Email is verplicht" + Environment.NewLine;
            }
            if (string.IsNullOrEmpty(txtTel.Text))
            {
                fout += "Telefoonnummer is verplicht" + Environment.NewLine;
            }
            return fout;
        }
        /*private string Valideer2()
        {
            string fout = "";
            if (!string.IsNullOrEmpty(txtId.Text))
            {
                List<Dokter> dokters = dokterRepository.OphalenDokters();
                foreach (Dokter dok in dokters)
                {
                    if (dok.Id == int.Parse(txtId.Text))
                    {
                        fout = "Id al in gebruik" + Environment.NewLine;
                    }
                }
            }
            return fout;
        }*/
        private void ResetForm()
        {
            txtId.Text = string.Empty;
            txtNaam.Text = string.Empty;
            txtVoornaam.Text = string.Empty;
            txtEmail.Text = string.Empty;
            txtTel.Text = string.Empty;
        }
    }
}
