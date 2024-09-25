using System;
using System.Collections.Generic;
using System.Security.Policy;
using System.Text;

namespace models
{
    public class Afspraak : BasisKlasse
    {
        public int Id { get; set; }
        public string Behandeling { get; set; }
        public DateTime Datum { get; set; }
        public int DokterId { get; set; }
        public int KlantId { get; set; }
        public override string this[string propertynaam]
        {
            get
            {
                if (propertynaam == nameof(Id) && Id < 0)
                {
                    return "Id mag niet onder 0";
                }
                if (propertynaam == nameof(DokterId) && DokterId < 0)
                {
                    return "DokterId mag niet onder 0";
                }
                if (propertynaam == nameof(KlantId) && KlantId < 0)
                {
                    return "KlantId mag niet onder 0";
                }
                if (propertynaam == nameof(Behandeling) && string.IsNullOrWhiteSpace(Behandeling))
                {
                    return "Behandeling is verplicht";
                }
                if (propertynaam == nameof(Datum) && Datum.Equals(null))
                {
                    return "Datum is verplicht";
                }
                return "";
            }
        }
        public override string ToString()
        {
            return $"{Behandeling}: {Datum.ToShortDateString()}";
        }
    }
}
