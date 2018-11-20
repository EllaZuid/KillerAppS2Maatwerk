using System;
using System.Collections.Generic;
using System.Web.Mvc;
using ChartJSCore.Models;
using Hardlopen.viewModels;
using Model;
using Logic;

namespace Hardlopen.Controllers
{
    public class LoopmomentController : Controller
    {
        Loopmoment _loopmoment = new Loopmoment();
        LoopmomentLogic _loopmomentLogic = new LoopmomentLogic();
        Chart chart = new Chart();
        Data data = new Data();

        // GET: Loopmoment
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult GegevensInvullen()
        {
            InvullenViewModel viewModel = new InvullenViewModel();
            viewModel.Tijd = string.Empty;
            viewModel.Datum = string.Empty;
            viewModel.Afstand = string.Empty;
            viewModel.GemiddeldeSnelheid = string.Empty;
            return View();
        }

        [HttpPost]
        public ActionResult GegevensInvullen(InvullenViewModel viewModel)
        {
            int tijd = Convert.ToInt32(viewModel.Tijd);
            DateTime datum = Convert.ToDateTime(viewModel.Datum);
            int afstand = Convert.ToInt32(viewModel.Afstand);
            _loopmoment.Tijd = tijd;
            _loopmoment.Datum = datum;
            _loopmoment.Afstand = afstand;
            _loopmomentLogic.GegevensInvullen(tijd, datum, afstand, (int)Session["idIngeloggd"]);
            return RedirectToAction("Index", "Home");
        }

        public ActionResult Overzicht()
        {
            chart.Type = "bar";
            data.Labels = null;
            BarDataset datasetbarTijd = new BarDataset()
            {
                Label = "Tijd",
                Data = _loopmomentLogic.ToonOverzichtTijdBar((int)Session["idIngeloggd"])
            };
            BarDataset datasetbarAfstand = new BarDataset()
            {
                Label = "Tijd",
                Data = _loopmomentLogic.ToonOverzichtAfstandBar((int)Session["idIngeloggd"])
            };
            LineDataset datasetline = new LineDataset()
            {
                Label = "Gemiddelde snelheid",
                Data = _loopmomentLogic.ToonOverzichtLine((int)Session["idIngeloggd"]),
                Fill = "false",
            };
            data.Datasets = new List<Dataset>();
            data.Datasets.Add(datasetbarTijd);
            data.Datasets.Add(datasetbarAfstand);
            data.Datasets.Add(datasetline);
            chart.Data = data;
            ViewData["chart"] = chart;
            return View();
        }

        public ActionResult Vergelijken()
        {
            return View();
        }
    }
}