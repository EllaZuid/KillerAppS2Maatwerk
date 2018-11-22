using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using Model;

namespace DAL
{
    public class GebruikerDal
    {
        const string Connectionstring = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\Ella\source\repos\Maatwerk-S2\Hardlopen\Hardlopen\App_Data\DatabaseHardlopen.mdf;Integrated Security=True";
        readonly SqlConnection _conn = new SqlConnection(Connectionstring);

        public List<Gebruiker> GebruikerId = new List<Gebruiker>();
        public List<Gebruiker> IdRegistratie = new List<Gebruiker>();

        private void Open()
        {
            _conn.Open(); 
            Console.WriteLine("Verbonden met " + Connectionstring + ".");
        }

        public List<Gebruiker> OphalenGebruikersInfo()
        {
            Open();
            string query = "SELECT ID, Naam, Wachtwoord FROM Gebruiker";
            SqlCommand commandInloggen = new SqlCommand(query, _conn);
            
            using (SqlDataReader reader = commandInloggen.ExecuteReader())
            {
                while (reader.Read())
                {
                    int id = reader.GetInt32(0);
                    string naam = reader.GetString(1);
                    string wachtwoord = reader.GetString(2);
                    Gebruiker persoon = new Gebruiker(id, naam, wachtwoord);
                    GebruikerId.Add(persoon);
                }
            }
            Close();
            return GebruikerId;
        }

        public void GebruikerRegistreren(string naam, string wachtwoord, string email, string geslacht, double gewicht, double lengte)
        {
            Open();
            string query = "INSERT INTO dbo.[Gebruiker] (naam, wachtwoord, email, geslacht, gewicht, lengte) VALUES (@Naam, @Wachtwoord, @Email, @Geslacht, @Gewicht, @Lengte)";
            SqlCommand commandRegistreren = new SqlCommand(query, _conn);
            commandRegistreren.Parameters.Add("@Naam", SqlDbType.NChar).Value = naam;
            commandRegistreren.Parameters.Add("@Wachtwoord", SqlDbType.VarChar).Value = wachtwoord;
            commandRegistreren.Parameters.Add("@Email", SqlDbType.NChar).Value = email;
            commandRegistreren.Parameters.Add("@geslacht", SqlDbType.NChar).Value = geslacht;
            commandRegistreren.Parameters.Add("@Lengte", SqlDbType.Float).Value = lengte;
            commandRegistreren.Parameters.Add("@Gewicht", SqlDbType.Float).Value = gewicht;
            commandRegistreren.ExecuteNonQuery();
            Close();
        }

        public List<Gebruiker> IdRegistratieOphalen(string naam)
        {
            Open();
            string query = "SELECT ID FROM Gebruiker WHERE Naam = @Naam";
            SqlCommand commandId = new SqlCommand(query, _conn);
            commandId.Parameters.Add("@Naam", SqlDbType.NChar).Value = naam;
            using (SqlDataReader reader = commandId.ExecuteReader())
            {
                while (reader.Read())
                {
                    int id = reader.GetInt32(0);
                    Gebruiker persoon = new Gebruiker(id, naam);
                    IdRegistratie.Add(persoon);
                }
            }
            Close();
            return IdRegistratie;
        }

        private void Close()
        {
            _conn.Close();
            Console.WriteLine("Verbinding verbroken.");
        }
    }
}
