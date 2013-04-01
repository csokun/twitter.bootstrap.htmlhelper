using System.Collections.Generic;
using System.Web.Mvc;

namespace MvcTwitterB.Models
{
	public class FavoriteArtistModel
	{
		public IEnumerable<SelectListItem> Favorites { get; set; }
	}
}