using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MvcTwitterB.Models
{
	public class PagedResult
	{
		public int PageCount { get; set; }
		public int PageIndex { get; set; }
	}
}