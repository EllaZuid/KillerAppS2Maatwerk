using System;
using System.Security.Cryptography;
using DAL;
using Model;

namespace Logic
{
    public class GebruikerLogic
    {
        private readonly GebruikerDal _gebruikerDal = new GebruikerDal();
        public string HashedWachtwoord;

        public int? Inloggen(Gebruiker gebruiker)
        {
            _gebruikerDal.OphalenGebruikersInfo();
            for (int i = 0; i < _gebruikerDal.GebruikerId.Count; i++)
            {
                string naam = _gebruikerDal.GebruikerId[i].Naam.Replace(" ", "");
                if (naam == gebruiker.Naam)
                {
                    string wachtwoord = _gebruikerDal.GebruikerId[i].Wachtwoord.Replace(" ", "");
                    if (VergelijkWachtwoorden(gebruiker.Wachtwoord, wachtwoord))
                    { 
                        return _gebruikerDal.GebruikerId[i].Id;
                    }
                    else
                    {
                        return 0;
                    }
                }
            }
            return null;
        }

        public int? Registreren(Gebruiker gebruiker, string wachtwoord2Invoer)
        {
            if (gebruiker.Wachtwoord == wachtwoord2Invoer)
            {
                HashWachtwoord(gebruiker.Wachtwoord);
                _gebruikerDal.GebruikerRegistreren(gebruiker.Naam, HashedWachtwoord, gebruiker.Email, gebruiker.Geslacht, gebruiker.Gewicht, gebruiker.Lengte);
                _gebruikerDal.IdRegistratieOphalen(gebruiker.Naam);
                for (int i = 0; i < _gebruikerDal.IdRegistratie.Count; i++)
                {
                    if (gebruiker.Naam == _gebruikerDal.IdRegistratie[i].Naam)
                    {
                        return _gebruikerDal.IdRegistratie[i].Id;
                    }
                }
            }
            else
            {
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
