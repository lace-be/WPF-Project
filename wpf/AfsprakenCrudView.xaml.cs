using dal.interfaces;
using dal.repositories;
using Microsoft.Win32;
using models;
using System;
using System.Collections.Generic;
using System.Reflection.Metadata;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters;
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
    /// Interaction logic for AfsprakenCrudView.xaml
    /// </summary>
    public partial class AfsprakenCrudView : UserControl
    {
        private AfsprakenRepository afsprakenRepository = new AfsprakenRepository();
        private DoktersRepository doktersRepository = new DoktersRepository();
        private KlantenRepository klantenRepository = new KlantenRepository();
        public AfsprakenCrudView()
        {
            InitializeComponent();
            GetData();
        }
        //Zet alle waarden op scherm klaar om te kiezen
        private void GetData()
        {
            lbAfspraken.ItemsSource = afsprakenRepository.OphalenAfspraken();
            cbDokter.ItemsSource = doktersRepository.OphalenDokters();
            cbDokter.SelectedValuePath = "Id";
            cbKlant.ItemsSource = klantenRepository.OphalenKlanten();
            cbKlant.SelectedValuePath = "Id";
        }
        private void lbAfspraken_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (lbAfspraken.SelectedItem != null)
            {
                SetData(((Afspraak)lbAfspraken.SelectedItem).Id);
            }
        }
        //Zet geselecteerde afspraak gegevens in de vakken
        private void SetData(int afspraakId)
        {
            Afspraak afspraak = afsprakenRepository.OphalenAfsprakenViaPK(afspraakId);
            if (afspraak != null)
            {
                txtId.Text = afspraak.Id.ToString();
                txtBehandeling.Text = afspraak.Behandeling;
                cbDokter.SelectedValue = afspraak.DokterId;
                cbKlant.SelectedValue = afspraak.KlantId;
                dpDate.Text = afspraak.Datum.ToShortDateString();
            }
        }
        private void btnVoegAfspraakToe_Click(object sender, RoutedEventArgs e)
        {
            List<Afspraak> afspraken = afsprakenRepository.OphalenAfspraken();
            foreach (Afspraak af in afspraken)
            {
                if (af.Id == int.Parse(txtId.Text))
                {
                    MessageBox.Show("Id al in gebruik");
                    return;
                }
            }
            Save(true);
        }

        private void btnWijzigAfspraak_Click(object sender, RoutedEventArgs e)
        {
            Save(false);
        }

        private void btnVerwijderAfspraak_Click(object sender, RoutedEventArgs e)
        {
            if (lbAfspraken.SelectedItem != null)
            {
                if (MessageBox.Show("Ben je zeker?", "Bevestiging verwijderen", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                {
                    // Verwijder employee
                    if (afsprakenRepository.DeleteAfspraak(((Afspraak)lbAfspraken.SelectedItem).Id))
                    {
                        MessageBox.Show("Afspraak werd verwijderd");
                        //Vernieuwde lijst laten tonen zonder verwijderd item
                        GetData();
                    }
                }
            }
            else
            {
                MessageBox.Show("Je moet een afspraak selecteren!");
            }
        }

        //WijzigenofToevoegenDokter
        private void Save(bool isInsert)
        {
            string error = Valideer();
            //Als geen errors zijn
            if (string.IsNullOrEmpty(error))
            {
                //Voor zowel verwijderen als updaten wordt een object aangemaakt voor de parameter in te vullen
                Afspraak afsp = new Afspraak()
                {
                    Id = int.Parse(txtId.Text),
                    Behandeling = txtBehandeling.Text,
                    DokterId = ((Dokter)cbDokter.SelectedItem).Id,
                    KlantId = ((Klant)cbKlant.SelectedItem).Id,
                    Datum = dpDate.SelectedDate.Value
                };
                if (isInsert)
                {
                    afsprakenRepository.InsertAfspraak(afsp);
                }
                else
                {
                    afsprakenRepository.UpdateAfspraak(afsp);
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
            if (string.IsNullOrEmpty(txtBehandeling.Text))
            {
                fout += "Behandeling is verplicht" + Environment.NewLine;
            }
            if (cbDokter.SelectedItem == null)
            {
                fout += "Dokter is verplicht te selecteren" + Environment.NewLine;
            }
            if (cbKlant.SelectedItem == null)
            {
                fout += "Klant is verplicht te selecteren" + Environment.NewLine;
            }
            if (!dpDate.SelectedDate.HasValue)
            {
                fout += "Datum is verplicht" + Environment.NewLine;
            }

            return fout;
        }
        //Maakt invulvakken leeg
        private void ResetForm()
        {
            txtId.Text = "";
            txtBehandeling.Text = "";
            cbDokter.SelectedItem = null;
            cbKlant.SelectedItem = null;
            dpDate.SelectedDate = null;
        }
    }
}
