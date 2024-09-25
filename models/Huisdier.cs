using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace models
{
    public class Huisdier : BasisKlasse
    {
        public int Id { get; set; }
        public string Naam { get; set; }
        public string Voornaam { get; set; }
        public DateTime Geboortedatum { get; set; }
        public int KlantId { get; set; }
        public override string this[string propertynaam]
        {
            get
            {
                if (propertynaam == nameof(Id) && Id < 0)
                {
                    return $"Id mag niet onder 0";
                }
                if (propertynaam == nameof(Naam) && string.IsNullOrWhiteSpace(Naam))
                {
                    return "Naam is verplicht!";
                }
                if (propertynaam == nameof(Voornaam) && string.IsNullOrWhiteSpace(Voornaam))
                {
                    return "Voornaam is verplicht!";
                }
                if (propertynaam == nameof(Geboortedatum) && Geboortedatum.Equals(0))
                {
                    return $"Geboortedatum is verplicht";
                }
                if (propertynaam == nameof(KlantId) && KlantId < 0)
                {
                    return $"KlantId mag niet onder 0";
                }
                return "";
            }
        }
    }
}
