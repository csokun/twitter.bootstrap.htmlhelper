using System.Collections.Generic;

namespace Twitter.Bootstrap.HtmlHelpers
{
	public class NavBar
	{
		public class Link
		{
			public string Url { get; set; }

			public string Text { get; set; }

			public bool Selected { get; set; }

			public IEnumerable<Link> Items { get; set; } 
		}

		public string Brand { get; set; }

		// collapsed 
		public bool Collapsible { get; set; }

		// .navbar-inverse
		public bool Inverse { get; set; }

		// .navbar-fixed-top
		public bool Fixed { get; set; }

		public IEnumerable<Link> Items { get; set; }
	}
}
