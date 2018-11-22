using System;
using System.Web.Mvc;
using Hardlopen.viewModels;
using Logic;
using Model;

namespace Hardlopen.Controllers
{
    public class AccountController : Controller
    {
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
            Gebruiker gebruiker = new Gebruiker(viewModel.GebruikersNaam, viewModel.Wachtwoord);
            Session["idIngeloggd"] = _gebruikerLogic.Inloggen(gebruiker);
            if ((int)Session["idIngeloggd"] > 0)
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
            double gewicht = Convert.ToDouble(viewModel.Gewicht);
            double lengte = Convert.ToDouble(viewModel.Lengte);
            Gebruiker gebruiker = new Gebruiker(viewModel.Naam, viewModel.Wachtwoord, viewModel.Email, viewModel.Geslacht, gewicht, lengte);
            Session["idIngeloggd"] = _gebruikerLogic.Registreren(gebruiker, viewModel.WachtwoordHerhaling);
            if ((int)Session["idIngeloggd"] > 0)
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