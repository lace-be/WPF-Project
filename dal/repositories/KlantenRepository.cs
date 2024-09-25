using dal.interfaces;
using models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Text;
using Dapper;
using System.Linq;
using System.Security.Policy;
using System.Xml.Linq;
using System.ComponentModel;

namespace dal.repositories
{
    public class KlantenRepository : BaseRepository, IKlantenRepository
    {
        public IEnumerable<Klant> OphalenKlanten()
        {
            string sql = "SELECT * FROM Dierenarts.Klant ORDER BY id";

            using (IDbConnection db = new SqlConnection(ConnectionString))
            {
                return db.Query<Klant>(sql).AsList();
            }
        }
        //Inner Join Enkelvoudige relatie klant heeft meerdere huisdieren
        public List<Klant> OphalenKlantenEnHuisdieren()
        {
            string sql = "SELECT Dierenarts.Klant.*, '' AS SplitCol, Dierenarts.Huisdier.*" +
                "FROM Dierenarts.Klant " +
                "INNER JOIN Dierenarts.Huisdier ON Klant.id = Huisdier.KlantId";
            using (IDbConnection db = new SqlConnection(ConnectionString))
            {
                var klanten = db.Query<Klant, Huisdier, Klant>(
                    sql,
                    (klant, huisdier) =>
                    {
                        huisdier.KlantId = klant.Id;
                        klant.Huisdieren = new List<Huisdier>() { huisdier };
                        return klant;
                    },
                    splitOn: "SplitCol"
                    );
                return GroepeerKlanten(klanten).ToList();
            }
        }
        private static IEnumerable<Klant> GroepeerKlanten(IEnumerable<Klant> klanten)
        {
            var gegroepeerd = klanten.GroupBy(k => k.Id);
            List<Klant> klantMetHuisdieren = new List<Klant>();
            foreach (var groep in gegroepeerd)
            {
                var klant = groep.First();
                List<Huisdier> alleHuisdieren = new List<Huisdier>();

                foreach (var h in groep)
                {
                    alleHuisdieren.Add(h.Huisdieren.First());
                }

                klant.Huisdieren = alleHuisdieren;
                klantMetHuisdieren.Add(klant);
            }
            return klantMetHuisdieren;
        }
        public List<Klant> OphalenKlantenViaNaam(string naam = " ")
        {
            string sql = "SELECT Dierenarts.Klant.*, '' AS SplitCol, Dierenarts.Huisdier.* " +
               "FROM Dierenarts.Klant " +
               "INNER JOIN Dierenarts.Huisdier ON Klant.id = Huisdier.KlantId " +
               "WHERE Klant.naam LIKE '%' + @naam + '%' ";
            using (IDbConnection db = new SqlConnection(ConnectionString))
            {
                var klanten = db.Query<Klant, Huisdier, Klant>(
                    sql,
                    (klant, huisdier) =>
                    {
                        huisdier.KlantId = klant.Id;
                        klant.Huisdieren = new List<Huisdier>() { huisdier };
                        return klant;
                    },
                    new { naam = naam },
                    splitOn: "SplitCol"
                    );
                return GroepeerKlanten(klanten).ToList();
            }
        }
        public List<Klant> OphalenKlantenViaNaamEnPostcode(string naam = "", int postcode = 0)
        {
            string sql = "SELECT Dierenarts.Klant.*, '' AS SplitCol, Dierenarts.Huisdier.* " +
               "FROM Dierenarts.Klant " +
               "INNER JOIN Dierenarts.Huisdier ON Klant.id = Huisdier.KlantId " +
               "WHERE Klant.naam LIKE '%' + @naam + '%' " +
               "AND postcode = @postcode";
            using (IDbConnection db = new SqlConnection(ConnectionString))
            {
                var klanten = db.Query<Klant, Huisdier, Klant>(
                    sql,
                    (klant, huisdier) =>
                    {
                        huisdier.KlantId = klant.Id;
                        klant.Huisdieren = new List<Huisdier>() { huisdier };
                        return klant;
                    },
                    new { naam = naam, postcode = postcode },
                    splitOn: "SplitCol"
                    );
                return GroepeerKlanten(klanten).ToList();
            }
        }
        public List<Klant> OphalenKlantenViaPostcode(int postcode = 0)
        {
            string sql = "SELECT Dierenarts.Klant.*, '' AS SplitCol, Dierenarts.Huisdier.* " +
               "FROM Dierenarts.Klant " +
               "INNER JOIN Dierenarts.Huisdier ON Klant.id = Huisdier.KlantId " +
               "WHERE postcode = @postcode ";
            using (IDbConnection db = new SqlConnection(ConnectionString))
            {
                var klanten = db.Query<Klant, Huisdier, Klant>(
                    sql,
                    (klant, huisdier) =>
                    {
                        huisdier.KlantId = klant.Id;
                        klant.Huisdieren = new List<Huisdier>() { huisdier };
                        return klant;
                    },
                    new { postcode = postcode },
                    splitOn: "SplitCol"
                    );
                return GroepeerKlanten(klanten).ToList();
            }
        }
    }
}
