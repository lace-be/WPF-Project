using models;
using System;
using System.Collections.Generic;
using System.Text;

namespace dal.interfaces
{
    public interface IAfsprakenRepository
    {
        public List<Afspraak> OphalenAfspraken();
        public List<Afspraak> OphalenAfsprakenViaDokter_Id(int dokter);
        public List<Afspraak> OphalenAfsprakenViaKlant_Id(int klant);
        public List<Afspraak> OphalenAfsprakenViaKlant_IdEnDokter_Id(int dokter, int klant);
        public Afspraak OphalenAfsprakenViaPK(int id);
        public bool InsertAfspraak(Afspraak afspraak);
        public bool UpdateAfspraak(Afspraak afspraak);
        public bool DeleteAfspraak(int afspraakId);
    }
}
