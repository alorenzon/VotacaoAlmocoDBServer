using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using VotacaoAlmoco.Models.Restaurantes;

namespace VotacaoAlmoco.Models.Restaurantes
{
    public class RestauranteController : Controller
    {
        //
        // GET: /Restaurante/

        public ActionResult Index()
        {
            return View(new Restaurante());
        }

    }
}
