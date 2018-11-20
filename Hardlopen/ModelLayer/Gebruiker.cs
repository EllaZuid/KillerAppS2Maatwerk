

namespace Model
{
    public class Gebruiker
    {
        public int Id { get; set; }
        public string Naam { get; set; }
        public string Wachtwoord { get; set; }
        public string Email { get; set; }
        public double Gewicht { get; set; }
        public double Lengte { get; set; }
        public string Geslacht { get; set; }
        //public DateTime Tijd { get; private set; }
        //public Loopmoment Loopmoment { get; private set; }
        //public Playlist Playlist { get; private set; }

        public Gebruiker() { }

        public Gebruiker(int id, string naam)
        {
            Id = id;
            Naam = naam;
        }
        public Gebruiker(int id, string naam, string wachtwoord)
        {
            Id = id;
            Naam = naam;
            Wachtwoord = wachtwoord;
        }

        public Gebruiker(string naam, string wachtwoord, string email, string geslacht, double gewicht, double lengte)
        {
            Naam = naam;
            Wachtwoord = wachtwoord;
            Email = email;
            Geslacht = geslacht;
            Gewicht = gewicht;
            Lengte = lengte;
        }
    }
}
