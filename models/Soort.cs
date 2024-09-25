using System;
using System.Collections.Generic;
using System.Text;

namespace models
{
    public class Soort : BasisKlasse
    {
        public int Id { get; set; }
        public string Soortnaam { get; set; }
        public string Ras { get; set; }
        public override string this[string propertyname]
        {
            get
            {
                if (propertyname == nameof(Id) && Id < 0)
                {
                    return $"Id mag niet onder 0!";
                }
                if (propertyname == nameof(Soortnaam) && string.IsNullOrWhiteSpace(Soortnaam))
                {
                    return "Soortnaam is verplicht!";
                }
                if (propertyname == nameof(Ras) && string.IsNullOrWhiteSpace(Ras))
                {
                    return "Ras is verplicht!";
                }
                return "";
            }
        }
    }
}
