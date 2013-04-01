using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace Twitter.Bootstrap.HtmlHelpers.Test.Models
{
	public class MyOccupation
	{
		[Display(Name = "My Favorites")]
		public IEnumerable<SelectListItem> Favorites { get; set; } 

		public IEnumerable<SelectListItem> Occupations { get; set; }
	}
}
