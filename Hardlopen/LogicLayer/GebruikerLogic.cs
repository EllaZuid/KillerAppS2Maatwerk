using System;
using System.Security.Cryptography;
using DAL;

namespace Logic
{
    public class GebruikerLogic
    {
        private readonly GebruikerDal _gebruikerDal = new GebruikerDal();
        public bool Check;
        public string HashedWachtwoord;

        public int? Inloggen(string naamInvoer, string wachtwoordInvoer)
        {
            _gebruikerDal.OphalenGebruikersInfo();
            for (int i = 0; i < _gebruikerDal.GebruikerId.Count; i++)
            {
                string naam = _gebruikerDal.GebruikerId[i].Naam.Replace(" ", "");
                if (naam == naamInvoer)
                {
                    string wachtwoord = _gebruikerDal.GebruikerId[i].Wachtwoord.Replace(" ", "");
                    if (VergelijkWachtwoorden(wachtwoordInvoer, wachtwoord))
                    {
                        Check = true;
                        return _gebruikerDal.GebruikerId[i].Id;
                    }
                    else
                    {
                        Check = false;
                        return 0;
                    }
                }
            }
            return null;
        }

        public int? Registreren(string naamInvoer, string wachtwoordInvoer, string wachtwoord2Invoer, string emailInvoer, decimal gewichtInvoer, double lengteInvoer, string geslachtInvoer)
        {
            decimal gewicht = Convert.ToDecimal(gewichtInvoer);
            if (wachtwoordInvoer == wachtwoord2Invoer)
            {
                HashWachtwoord(wachtwoordInvoer);
                _gebruikerDal.GebruikerRegistreren(naamInvoer, HashedWachtwoord, emailInvoer, geslachtInvoer, gewicht, lengteInvoer);
                Check = true;
                _gebruikerDal.IdRegistratieOphalen(naamInvoer);
                for (int i = 0; i < _gebruikerDal.IdRegistratie.Count; i++)
                {
                    if (naamInvoer == _gebruikerDal.IdRegistratie[i].Naam)
                    {
                        return _gebruikerDal.IdRegistratie[i].Id;
                    }
                }
            }
            else
            {
                Check = false;
                return null;
            }
            return null;
        }

        private void HashWachtwoord(string wachtwoordInvoer)
        {
            byte[] salt;
            new RNGCryptoServiceProvider().GetBytes(salt = new byte[16]);
            var pbkdf2 = new Rfc2898DeriveBytes(wachtwoordInvoer, salt, 10000);
            byte[] hash = pbkdf2.GetBytes(20);
            byte[] hashBytes = new byte[36];
            Array.Copy(salt, 0, hashBytes, 0, 16);
            Array.Copy(hash, 0, hashBytes, 16, 20);
            HashedWachtwoord = Convert.ToBase64String(hashBytes);
        }

        private bool VergelijkWachtwoorden(string wachtwoordInvoer, string wachtwoordhash)
        {
            byte[] hashBytes = Convert.FromBase64String(wachtwoordhash);
            byte[] salt = new byte[16];
            Array.Copy(hashBytes, 0, salt, 0, 16);
            var pbkdf2 = new Rfc2898DeriveBytes(wachtwoordInvoer, salt, 10000);
            byte[] hash = pbkdf2.GetBytes(20);
            for (int i = 0; i < 20; i++)
            {
                if (hashBytes[i + 16] != hash[i])
                {
                    return false;
                }
            }
            return true;
        }
    }
}
