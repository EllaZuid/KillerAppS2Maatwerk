

namespace Model
{
    public class Gebruiker
    {
        public int Id { get; private set; }
        public string Naam { get; private set; }
        public string Wachtwoord { get; private set; }
        public string Email { get; private set; }
        public double Gewicht { get; private set; }
        public double Lengte { get; private set; }
        public string Geslacht { get; private set; }
        //public DateTime Tijd { get; private set; }
        //public Loopmoment Loopmoment { get; private set; }    
        //public Playlist Playlist { get; private set; }

        public Gebruiker() { }

        public Gebruiker(int id, string naam)
        {
            Id = id;
            Naam = naam;
        }

        public Gebruiker(string naam, string wachtwoord)
        {
            Naam = naam;
            Wachtwoord = wachtwoord;
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
