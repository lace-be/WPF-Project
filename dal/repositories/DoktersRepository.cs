using dal.interfaces;
using models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Text;
using Dapper;
using System.Linq;

namespace dal.repositories
{
    public class DoktersRepository : BaseRepository, IDoktersRepository
    {
        public bool DeleteDokter(int dokterId)
        {
            string sql = @"DELETE FROM Dierenarts.Dokter
                           WHERE id = @dokterId";
            var parameters = new
            {
                @dokterId = dokterId
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
        public bool InsertDokter(Dokter dokter)
        {
            string sql = @"INSERT INTO Dierenarts.Dokter (id, naam, voornaam, email, tel)
                           VALUES (@id, @naam, @voornaam, @email, @tel)";
            var parameters = new
            {
                @id = dokter.Id,
                @naam = dokter.Naam,
                @voornaam = dokter.Voornaam,
                @email = dokter.Email,
                @tel = dokter.Tel
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
        public List<Dokter> OphalenDokters()
        {
            string sql = "SELECT * FROM Dierenarts.Dokter ORDER BY id";
            using (IDbConnection db = new SqlConnection(ConnectionString))
            {
                return db.Query<Dokter>(sql).AsList();
            }
        }
        public List<Dokter> OphalenDoktersViaNaam(string naam)
        {
            string sql = "SELECT * FROM Dierenarts.Dokter";
            sql += " WHERE naam like '%'+ @naam +'%'";
            sql += " ORDER BY id";
            var parameters = new { @naam = naam };
            using (IDbConnection db = new SqlConnection(ConnectionString))
            {
                return db.Query<Dokter>(sql, parameters).ToList();
            }
        }
        public List<Dokter> OphalenDoktersViaNaamEnVoornaam(string naam, string voornaam)
        {
            string sql = "SELECT * FROM Dierenarts.Dokter";
            sql += " WHERE naam like '%'+ @naam +'%'";
            sql += " AND voornaam like '%' + @voornaam + '%'";
            sql += " ORDER BY naam";
            var parameters = new { @naam = naam, @voornaam = voornaam };
            using (IDbConnection db = new SqlConnection(ConnectionString))
            {
                return db.Query<Dokter>(sql, parameters).ToList();
            }
        }
        public List<Dokter> OphalenDoktersViaVoornaam(string voornaam)
        {
            string sql = "SELECT * FROM Dierenarts.Dokter";
            sql += " WHERE voornaam like '%'+ @voornaam +'%'";
            sql += " ORDER BY voornaam";
            var parameters = new { @voornaam = voornaam };
            using (IDbConnection db = new SqlConnection(ConnectionString))
            {
                return db.Query<Dokter>(sql, parameters).ToList();
            }
        }
        public Dokter OphalenDokterViaPK(int id)
        {
            string sql = "SELECT * FROM Dierenarts.Dokter WHERE id = @id";
            var parameters = new { @id = id };
            using (IDbConnection db = new SqlConnection(ConnectionString))
            {
                return db.QuerySingleOrDefault<Dokter>(sql, parameters);
            }
        }
        public bool UpdateDokter(Dokter dokter)
        {
            string sql = @"UPDATE Dierenarts.Dokter SET Naam = @naam,
                                            Voornaam = @voornaam,
                                            Email = @email,
                                            Tel = @tel WHERE Id = @dokterid";
            var parameters = new
            {
                @dokterid = dokter.Id,
                @naam = dokter.Naam,
                @voornaam = dokter.Voornaam,
                @email = dokter.Email,
                @tel = dokter.Tel
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
