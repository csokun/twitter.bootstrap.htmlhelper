using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MvcTwitterB.Models;

namespace MvcTwitterB.Controllers
{
    public class SpikeController : Controller
    {
        //
        // GET: /Spike/

        public ActionResult Index()
        {
            return View(new PagedResult() { PageCount =  3, PageIndex = 1});
        }

			public ActionResult Date()
			{
				var model = new DateTest();
				TryUpdateModel(model);

				return View(model);
			}

			public ActionResult Modal()
			{
				return View();
			}
    }
}
