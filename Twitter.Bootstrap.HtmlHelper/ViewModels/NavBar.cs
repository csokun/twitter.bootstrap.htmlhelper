using System.Collections.Generic;

namespace Twitter.Bootstrap.HtmlHelpers.ViewModels
{
	public enum NavBarDock
	{
		None,
		Top,
		Bottom
	}

	public class NavBar
	{
		public NavBar()
		{
			Fixed = NavBarDock.None;
		}

		public string Brand { get; set; }

		// collapsed 
		public bool Collapsible { get; set; }

		// .navbar-inverse
		public bool Inverse { get; set; }

		// .navbar-fixed-top
		public NavBarDock Fixed { get; set; }

		public IList<TbMenuTree> Items { get; set; }
	}
}
