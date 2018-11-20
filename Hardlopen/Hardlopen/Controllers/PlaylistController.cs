using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Hardlopen.Controllers
{
    public class PlaylistController : Controller
    {
        // GET: Playlist
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult MuziekLuisteren()
        {
            return View();
        }

        public ActionResult LiedjesToevoegen()
        {
            return View();
        }
    }
}