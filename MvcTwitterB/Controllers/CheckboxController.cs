using System.Collections.Generic;
using System.Web.Mvc;
using BootstrapMvcSample.Controllers;
using MvcTwitterB.Models;

namespace MvcTwitterB.Controllers
{
	public class CheckboxController : BootstrapBaseController
	{
		//
		// GET: /Checkbox/

		public ActionResult Index()
		{
			var model = new FavoriteArtistModel()
				{
					Favorites = new List<SelectListItem>()
						{
							new SelectListItem() {Text = "Kanha", Value = "10", Selected = false},
							new SelectListItem() {Text = "Sovath", Value = "20"}
						}
				};

			return View(model);
		}

		[HttpPost]
		public ActionResult Index(FavoriteArtistModel model)
		{
			return View(model);
		}
	}
}
