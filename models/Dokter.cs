using System;
using System.Collections.Generic;
using System.Text;

namespace models
{
    public class Dokter : BasisKlasse
    {
        public int Id { get; set; }
        public string Naam { get; set; }
        public string Voornaam { get; set; }
        public string Email { get; set; }
        public string Tel { get; set; }
        public override string this[string propertynaam]
        {
            get
            {
                if (propertynaam == nameof(Id) && Id < 0)
                {
                    return "Ibannummer mag niet onder 0!";
                }
                if (propertynaam == nameof(Naam) && string.IsNullOrWhiteSpace(Naam))
                {
                    return "Naam is een verplicht!";
                }
                if (propertynaam == nameof(Voornaam) && string.IsNullOrWhiteSpace(Voornaam))
                {
                    return "Voornaam is verplicht!";
                }
                if (propertynaam == nameof(Email) && string.IsNullOrWhiteSpace(Email))
                {
                    return "Email is verplicht!";
                }
                if (propertynaam == nameof(Tel) && Tel.Length != 10)
                {
                    return "Tel moet 10 cijfers bevatten!";
                }
                return "";
            }
        }
        public override string ToString()
        {
            return Voornaam + " " + Naam;
        }
    }
}
