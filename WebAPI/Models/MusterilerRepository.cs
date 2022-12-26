using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace WebAPI.Models
{
    public class MusterilerRepository : IMusterilerRepository
    {
        public IConfiguration Configuration { get; set; }
        public string connectionString;
        private readonly ILogger<MusterilerRepository> _Logger;

        public MusterilerRepository(IConfiguration configuration, ILogger<MusterilerRepository> logger)
        {
            this.Configuration = configuration;
            connectionString = Configuration["ConnectionStrings:DefaultConnection"];
            _Logger = logger;
        }

        public Musteriler AddMusteri(Musteriler musteriler)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    SqlCommand cmd = new SqlCommand("spInsertIntoMusteriler", connection);
                    cmd.CommandType = CommandType.StoredProcedure;
                    connection.Open();
                    cmd.Parameters.AddWithValue("@AdSoyad", musteriler.AdSoyad);
                    cmd.Parameters.AddWithValue("@Adres", musteriler.Adres);
                    cmd.Parameters.AddWithValue("@Telefon", musteriler.Telefon);
                    cmd.Parameters.AddWithValue("@Email", musteriler.Email);

                    cmd.ExecuteNonQuery();
                    connection.Close();

                }
                catch (Exception ex)
                {
                    _Logger.LogError(ex, "hata var addmusteriler metodunda");
                    musteriler = null;
                }
            }
            return musteriler = null;
        }

        public void DeleteMusteri(int? id)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    SqlCommand cmd = new SqlCommand("spDeleteMusteriler", connection);
                    cmd.CommandType = CommandType.StoredProcedure;
                    connection.Open();
                    cmd.Parameters.AddWithValue("@Id", id);


                    cmd.ExecuteNonQuery();
                    connection.Close();

                }
                catch (Exception ex)
                {
                    _Logger.LogError(ex, "hata var delete musteriler metodunda");

                }
            }
        }

        public IEnumerable<Musteriler> GetAllMusteriler()
        {
            List<Musteriler> musterilers = new List<Musteriler>();
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                try
                {
                    SqlCommand cmd = new SqlCommand("spSelectMusteriler", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    con.Open();
                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        Musteriler musteriler = new Musteriler();
                        musteriler.Id = Convert.ToInt32(reader["Id"]);
                        musteriler.AdSoyad = reader["AdSoyad"].ToString();
                        musteriler.Adres = reader["Adres"].ToString();
                        musteriler.Telefon = reader["Telefon"].ToString();
                        musteriler.Email = reader["Email"].ToString();
                        musterilers.Add(musteriler);
                    }
                    reader.Close();
                }
                catch (Exception ex)
                {
                    _Logger.LogError(ex, "hata var getallmusteriler metodunda");
                    musterilers = null;
                }
            }

            return musterilers;
        }

        public Musteriler GetMusteriById(int id)
        {
            Musteriler musteriler = new Musteriler();
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                try
                {
                    SqlCommand cmd = new SqlCommand("spSelectMusteriler", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    con.Open();
                    cmd.Parameters.AddWithValue("@Id", id);
                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        musteriler.Id = id;
                        musteriler.AdSoyad = reader["AdSoyad"].ToString();
                        musteriler.Adres = reader["Adres"].ToString();
                        musteriler.Telefon = reader["Telefon"].ToString();
                        musteriler.Email = reader["Email"].ToString();
                    }
                    reader.Close();
                }
                catch (Exception ex)
                {
                    _Logger.LogError(ex, "hata var getallmusteriler metodunda");
                    musteriler = null;
                }
             }

            return musteriler;

        }

        public Musteriler UpdateMusteri(Musteriler musteriler)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    SqlCommand cmd = new SqlCommand("spUpdateMusteriler", connection);
                    cmd.CommandType = CommandType.StoredProcedure;
                    connection.Open();
                    cmd.Parameters.AddWithValue("@Id", musteriler.Id);
                    cmd.Parameters.AddWithValue("@AdSoyad", musteriler.AdSoyad);
                    cmd.Parameters.AddWithValue("@Adres", musteriler.Adres);
                    cmd.Parameters.AddWithValue("@Telefon", musteriler.Telefon);
                    cmd.Parameters.AddWithValue("@Email", musteriler.Email);

                    cmd.ExecuteNonQuery();
                    connection.Close();

                }
                catch (Exception ex)
                {
                    _Logger.LogError(ex, "hata var updatemusteriler metodunda");
                    musteriler = null;
                }
            }
            return musteriler = null;
        }
    }
}
