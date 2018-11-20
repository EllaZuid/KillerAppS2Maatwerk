using System;
using System.Collections.Generic;
using DAL;
using Google.Protobuf.WellKnownTypes;

namespace Logic
{
    public class LoopmomentLogic
    {
        private readonly LoopmomentDal _loopmomentDal = new LoopmomentDal();
        public List<double> LineOverzicht = new List<double>();
        public List<double> BarTijdOverzicht = new List<double>();
        public List<double> BarAfstandOverzicht = new List<double>();

        public void GegevensInvullen(int tijdInvoer, DateTime datumInvoer, int afstandInvoer, int gebruikerId)
        {
            int tijd = tijdInvoer * 60;
            DateTime datum = datumInvoer;
            int afstand = afstandInvoer * 1000;
            decimal gemiddeldeSnelheid = afstand / tijd;
            _loopmomentDal.GegevensInvullen(tijd, datum, afstand, gemiddeldeSnelheid, gebruikerId);
        }
        public List<double> ToonOverzichtLine(int id)
        {
            _loopmomentDal.GegevensOverzichtOphalenLine(id);
            foreach (var line in _loopmomentDal.LoopmomentOverzichtLine)
            {
                LineOverzicht.Add(line);
            }

            return LineOverzicht;
        }
        public List<double> ToonOverzichtTijdBar(int id)
        {
            _loopmomentDal.GegevensOverzichtOphalenTijdBar(id);
            foreach (var line in _loopmomentDal.LoopmomentOverzichtTijdBar)
            {
                BarTijdOverzicht.Add(line);
            }

            return BarTijdOverzicht;
        }

        public List<double> ToonOverzichtAfstandBar(int id)
        {
            _loopmomentDal.GegevensOverzichtOphalenAfstandBar(id);
            foreach (var line in _loopmomentDal.LoopmomentOverzichtAfstandBar)
            {
                BarAfstandOverzicht.Add(line);
            }

            return BarAfstandOverzicht;
        }

        public void GeefStatusUpdate()
        {

        }

        public void ToonVergelijking()
        {

        }
    }
}
