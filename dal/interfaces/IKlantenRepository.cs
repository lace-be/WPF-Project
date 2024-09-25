using models;
using System;
using System.Collections.Generic;
using System.Security.Policy;
using System.Text;

namespace dal.interfaces
{
    public interface IKlantenRepository
    {
        public IEnumerable<Klant> OphalenKlanten();
        public List<Klant> OphalenKlantenViaNaam(string naam);
        public List<Klant> OphalenKlantenViaPostcode(int postcode);
        public List<Klant> OphalenKlantenViaNaamEnPostcode(string naam, int postcode);
        public List<Klant> OphalenKlantenEnHuisdieren();
    }
}
