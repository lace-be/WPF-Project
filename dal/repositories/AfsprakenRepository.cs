using dal.interfaces;
using models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Text;
using Dapper;
using System.Security.Cryptography;
using System.Linq;

namespace dal.repositories
{
    public class AfsprakenRepository : BaseRepository, IAfsprakenRepository
    {
        public bool DeleteAfspraak(int afspraakId)
        {
            string sql = @"DELETE FROM Dierenarts.Afspraak
                           WHERE id = @afspraakId";
            var parameters = new
            {
                @afspraakId = afspraakId
            };
            using (IDbConnection db = new SqlConnection(ConnectionString))
            {
                var affectedRows = db.Execute(sql, parameters);
                if (affectedRows >= 1)
                {
                    return true;
                }
            }
            return false;
        }
        public bool InsertAfspraak(Afspraak afspraak)
        {
            string sql = @"INSERT INTO Dierenarts.Afspraak (id, behandeling, datum, dokterId, klantId)
                           VALUES (@id, @behandeling, @datum, @dokterId, @klantId)";
            var parameters = new
            {
                @id = afspraak.Id,
                @behandeling = afspraak.Behandeling,
                @datum = afspraak.Datum,
                @dokterId = afspraak.DokterId,
                @klantId = afspraak.KlantId
            };
            using (IDbConnection db = new SqlConnection(ConnectionString))
            {
                var affectedRows = db.Execute(sql, parameters);
                if (affectedRows == 1)
                {
                    return true;
                }
            }
            return false;
        }
        public List<Afspraak> OphalenAfspraken()
        {
            string sql = "SELECT * FROM Dierenarts.Afspraak ORDER BY datum";
            using (IDbConnection db = new SqlConnection(ConnectionString))
            {
                return db.Query<Afspraak>(sql).ToList();
            }
        }
        public List<Afspraak> OphalenAfsprakenViaDokter_Id(int dokter)
        {
            string sql = "SELECT * FROM Dierenarts.Afspraak WHERE dokterId = @dokterId ORDER BY datum";
            var parameters = new { @dokterId = dokter };
            using (IDbConnection db = new SqlConnection(ConnectionString))
            {
                return db.Query<Afspraak>(sql, parameters).AsList();
            }
        }
        public List<Afspraak> OphalenAfsprakenViaKlant_Id(int klant)
        {
            string sql = "SELECT * FROM Dierenarts.Afspraak WHERE klantId = @klantId ORDER BY datum";
            var parameters = new { @klantId = klant };
            using (IDbConnection db = new SqlConnection(ConnectionString))
            {
                return db.Query<Afspraak>(sql, parameters).AsList();
            }
        }
        public List<Afspraak> OphalenAfsprakenViaKlant_IdEnDokter_Id(int dokter, int klant)
        {
            string sql = "SELECT * FROM Dierenarts.Afspraak " +
                "WHERE klantId = @klantId " +
                "AND dokterId=@dokterId " +
                "ORDER BY datum";
            var parameters = new { @dokterId = dokter, @klantId = klant };
            using (IDbConnection db = new SqlConnection(ConnectionString))
            {
                return db.Query<Afspraak>(sql, parameters).AsList();
            }
        }
        public Afspraak OphalenAfsprakenViaPK(int id)
        {
            string sql = "SELECT * FROM Dierenarts.Afspraak WHERE id = @id";
            var parameters = new { @id = id };
            using (IDbConnection db = new SqlConnection(ConnectionString))
            {
                return db.QuerySingleOrDefault<Afspraak>(sql, parameters);
            }
        }
        public bool UpdateAfspraak(Afspraak afspraak)
        {
            string sql = @"UPDATE Dierenarts.Afspraak SET 
                                            klantId = @klantId,
                                            dokterId = @dokterId,
                                            behandeling = @behandeling,
                                            datum = @datum
                          WHERE id = @Id";
            var parameters = new
            {
                @Id = afspraak.Id,
                @klantId = afspraak.KlantId,
                @dokterId = afspraak.DokterId,
                @behandeling = afspraak.Behandeling,
                @datum = afspraak.Datum
            };
            using (IDbConnection db = new SqlConnection(ConnectionString))
            {
                var affectedRows = db.Execute(sql, parameters);
                if (affectedRows == 1)
                {
                    return true;
                }
            }
            return false;
        }
    }
}
