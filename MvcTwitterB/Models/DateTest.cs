using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MvcTwitterB.Models
{
	public class DateTest
	{
		public DateTest()
		{
			Date1 = DateTime.Today;
		}
		public DateTime Date1 { get; set; }

		public DateTime? Date2 { get; set; }
	}
}