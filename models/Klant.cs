using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace models
{
    public class Klant : BasisKlasse
    {
        public int Id { get; set; }
        public string Naam { get; set; }
        public string Voornaam { get; set; }
        public string Tel { get; set; }
        public string Email { get; set; }
        public int Postcode { get; set; }
        //Navigation property
        public List<Huisdier> Huisdieren { get; set; }
        public override string this[string propertyname]
        {
            get
            {
                if (propertyname == nameof(Id) && Id < 0)
                {
                    return $"Id mag niet onder 0!";
                }
                if (propertyname == nameof(Naam) && string.IsNullOrWhiteSpace(Naam))
                {
                    return "Ibannummer is verplicht!";
                }
                if (propertyname == nameof(Voornaam) && string.IsNullOrWhiteSpace(Voornaam))
                {
                    return "Ibannummer is verplicht!";
                }
                if (propertyname == nameof(Tel) && Tel.Length != 10)
                {
                    return $"Tel moet 10 cijfers bevatten!";
                }
                if (propertyname == nameof(Email) && string.IsNullOrWhiteSpace(Email))
                {
                    return "Email is verplicht!";
                }
                if (propertyname == nameof(Postcode) && Postcode != 4)
                {
                    return $"Postcode moet 4 cijfers bevatten!";
                }
                return "";
            }
        }
        public override string ToString()
        {
            return Voornaam + " " + Naam + " " + Postcode;
        }
        public string HuisdierInfo()
        {
            string result = "";
            if (this.Huisdieren.Count() > 0)
            {
                foreach (Huisdier huisdier in this.Huisdieren)
                {
                    var age = DateTime.Today.Year - huisdier.Geboortedatum.Year;
                    result += $"- {huisdier.Voornaam} {huisdier.Naam} \n\tLeeftijd: {age} jaar" + Environment.NewLine;
                }
            }
            return result;
        }
    }
}
