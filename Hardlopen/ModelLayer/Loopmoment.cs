using System;

namespace Model
{
    public class Loopmoment
    {
        public DateTime Datum { get; set; }
        public int Tijd { get; set; }
        public decimal GemiddeldeSnelheid { get; set; }
        public int Afstand { get; set; }
        public Array[] Status { get; private set; }
        public Route Route { get; private set; }
        public Playlist Playlist { get; private set; }

        public Loopmoment() { }

        public Loopmoment(int afstand, int tijd)
        {
            Afstand = afstand;
            Tijd = tijd;
        }

        public Loopmoment(decimal gemiddeldeSnelheid)
        {
            GemiddeldeSnelheid = gemiddeldeSnelheid;
        }
    }
}
