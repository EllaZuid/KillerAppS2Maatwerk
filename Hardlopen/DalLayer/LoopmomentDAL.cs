using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using Model;

namespace DAL
{
    public class LoopmomentDal
    {
        const string Connectionstring = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\Ella\source\repos\Maatwerk-S2\Hardlopen\Hardlopen\App_Data\DatabaseHardlopen.mdf;Integrated Security=True";
        readonly SqlConnection _conn = new SqlConnection(Connectionstring);

        public List<double> LoopmomentOverzichtTijdBar = new List<double>();
        public List<double> LoopmomentOverzichtAfstandBar = new List<double>();
        public List<double> LoopmomentOverzichtLine = new List<double>();

        public void Open()
        {
            _conn.Open();
            Console.WriteLine("Verbonden met " + Connectionstring + ".");
        }

        public void GegevensInvullen(int tijd, DateTime datum, int afstand, decimal gemiddeldeSnelheid, int gebruikerId)
        {
            Open();
            string query = "INSERT INTO dbo.[Loopmoment] (gebruiker, tijd, datum, afstand, gemiddeldeSnelheid) VALUES (@Gebruiker, @Tijd, @Datum, @Afstand, @GemiddeldeSnelheid)";
            SqlCommand commandInvullen = new SqlCommand(query, _conn);
            commandInvullen.Parameters.Add("@Gebruiker", SqlDbType.Int).Value = gebruikerId;
            commandInvullen.Parameters.Add("@Tijd", SqlDbType.Int).Value = tijd;
            commandInvullen.Parameters.Add("@Datum", SqlDbType.DateTime).Value = datum;
            commandInvullen.Parameters.Add("@Afstand", SqlDbType.Int).Value = afstand;
            commandInvullen.Parameters.Add("@GemiddeldeSnelheid", SqlDbType.Decimal).Value = gemiddeldeSnelheid;
            commandInvullen.ExecuteNonQuery();
            Close();
        }

        public List<double> GegevensOverzichtOphalenTijdBar(int id)
        {
            Open();
            string query = "SELECT Tijd FROM Loopmoment WHERE Afstand > 1000 AND Gebruiker = @Id";
            SqlCommand commandOverzicht = new SqlCommand(query, _conn);
            commandOverzicht.Parameters.Add("@Id", SqlDbType.NChar).Value = id;
            using (SqlDataReader reader = commandOverzicht.ExecuteReader())
            {
                while (reader.Read())
                {
                    int tijd = reader.GetInt32(0);
                    double tijddouble = Convert.ToDouble(tijd);
                    LoopmomentOverzichtTijdBar.Add(tijddouble);
                }
            }
            Close();
            return LoopmomentOverzichtTijdBar;
        }
        public List<double> GegevensOverzichtOphalenAfstandBar(int id)
        {
            Open();
            string query = "SELECT Afstand FROM Loopmoment WHERE Afstand > 1000 AND Gebruiker = @Id";
            SqlCommand commandOverzicht = new SqlCommand(query, _conn);
            commandOverzicht.Parameters.Add("@Id", SqlDbType.NChar).Value = id;
            using (SqlDataReader reader = commandOverzicht.ExecuteReader())
            {
                while (reader.Read())
                {
                    int afstand = reader.GetInt32(0);
                    double afstanddouble = Convert.ToDouble(afstand);
                    LoopmomentOverzichtAfstandBar.Add(afstanddouble);
                }
            }
            Close();
            return LoopmomentOverzichtTijdBar;
        }

        public List<double> GegevensOverzichtOphalenLine(int id)
        {
            Open();
            string query = "SELECT GemiddeldeSnelheid FROM Loopmoment WHERE Afstand > 1000 AND Gebruiker = @Id";
            SqlCommand commandOverzicht = new SqlCommand(query, _conn);
            commandOverzicht.Parameters.Add("@Id", SqlDbType.NChar).Value = id;
            using (SqlDataReader reader = commandOverzicht.ExecuteReader())
            {
                while (reader.Read())
                {
                    decimal gemiddeldeSnelheid = reader.GetDecimal(0);
                    double gemiddeldeSnelheiddouble = Convert.ToDouble(gemiddeldeSnelheid);
                    LoopmomentOverzichtLine.Add(gemiddeldeSnelheiddouble);
                }
            }
            Close();
            return LoopmomentOverzichtLine;
        }

        public void Close()
        {
            _conn.Close();
            Console.WriteLine("Verbinding verbroken.");
        }
    }
}
