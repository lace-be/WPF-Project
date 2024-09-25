using models;
using System;
using System.Collections.Generic;
using System.Text;

namespace dal.interfaces
{
    public interface IDoktersRepository
    {
        public List<Dokter> OphalenDokters();
        public List<Dokter> OphalenDoktersViaNaam(string naam);
        public List<Dokter> OphalenDoktersViaVoornaam(string voornaam);
        public List<Dokter> OphalenDoktersViaNaamEnVoornaam(string naam, string voornaam);
        public Dokter OphalenDokterViaPK(int id);
        public bool InsertDokter(Dokter dokter);
        public bool UpdateDokter(Dokter dokter);
        public bool DeleteDokter(int dokterId);
    }
}
