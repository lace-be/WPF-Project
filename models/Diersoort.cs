using System;
using System.Collections.Generic;
using System.Text;

namespace models
{
    public class Diersoort : BasisKlasse
    {
        public int Id { get; set; }
        public int SoortId { get; set; }
        public int HuisdierId { get; set; }
        public override string this[string propertynaam]
        {
            get
            {
                if (propertynaam == nameof(Id) && Id < 0)
                {
                    return "Ibannummer mag niet onder 0!";
                }
                if (propertynaam == nameof(Id) && Id < 0)
                {
                    return "Ibannummer mag niet onder 0!";
                }
                if (propertynaam == nameof(Id) && Id < 0)
                {
                    return "Ibannummer mag niet onder 0!";
                }
                return "";
            }
        }
    }
}
