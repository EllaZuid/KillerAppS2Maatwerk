using System;
using System.Web.Mvc;
using Hardlopen.viewModels;
using Logic;
using Model;

namespace Hardlopen.Controllers
{
    public class AccountController : Controller
    {
        Gebruiker _gebruiker = new Gebruiker();
        private GebruikerLogic _gebruikerLogic = new GebruikerLogic();

        // GET: Account
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Inloggen()
        {
            InloggenViewModel viewModel = new InloggenViewModel();
            viewModel.GebruikersNaam = String.Empty;
            viewModel.Wachtwoord = String.Empty;
            return View();
        }

        [HttpPost]
        public ActionResult Inloggen(InloggenViewModel viewModel)
        {
            _gebruiker.Naam = viewModel.GebruikersNaam;
            _gebruiker.Wachtwoord = viewModel.Wachtwoord;
            Session["idIngeloggd"] = _gebruikerLogic.Inloggen(_gebruiker.Naam, _gebruiker.Wachtwoord);
            if (_gebruikerLogic.Check)
            {
                return RedirectToAction("Index", "Home");
            }

            return View();
        }

        public ActionResult Registreren()
        {
            RegistrerenViewModel viewModel = new RegistrerenViewModel();
            viewModel.Naam = String.Empty;
            viewModel.Wachtwoord = String.Empty;
            viewModel.WachtwoordHerhaling = String.Empty;
            viewModel.Email = String.Empty;
            viewModel.Gewicht = String.Empty;
            viewModel.Geslacht = String.Empty;
            return View();
        }

        [HttpPost]
        public ActionResult Registreren(RegistrerenViewModel viewModel)
        {
            decimal gewicht = Convert.ToDecimal(viewModel.Gewicht);
            double lengte = Convert.ToDouble(viewModel.Lengte);
            _gebruiker.Naam = viewModel.Naam;
            _gebruiker.Wachtwoord = viewModel.Wachtwoord;
            _gebruiker.Email = viewModel.Email;
            _gebruiker.Gewicht = gewicht;
            _gebruiker.Lengte = lengte;
            _gebruiker.Geslacht = viewModel.Geslacht;
            Session["idIngeloggd"] = _gebruikerLogic.Registreren(_gebruiker.Naam, _gebruiker.Wachtwoord, viewModel.WachtwoordHerhaling, _gebruiker.Email, gewicht, lengte, _gebruiker.Geslacht);
            if (_gebruikerLogic.Check)
            {
                return RedirectToAction("Index", "Home");
            }

            return View();
        }

        public ActionResult Uitloggen()
        {
            Session["idIngeloggd"] = null;
            return RedirectToAction("Index", "Home");
        }
    }
}