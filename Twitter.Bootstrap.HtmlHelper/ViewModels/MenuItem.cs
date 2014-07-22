using System.Collections.Generic;

namespace Twitter.Bootstrap.HtmlHelpers.ViewModels
{
	public class TbMenuItem
	{
		private string _route;
		private bool _visible	= true;
		
		public string Text { get; set; }

		public string RouteName
		{
			get { return string.IsNullOrWhiteSpace(_route) ? "Default" : _route; }
			set { _route = value; }
		}

		public string Key { get; set; }

		public bool Selected { get; set; }

		public bool Visible
		{
			get { return _visible; }
			set { _visible = value; }
		}

		public object RouteValues { get; set; }
		public object Attributes { get; set; }
	}

	public class TbMenuTree : TbMenuItem
	{
		public bool Node
		{
			get { return Items != null && Items.Count > 0; }
		}

		public bool Leaf
		{
			get { return Items == null || Items.Count == 0; }
		}

		//public TbMenuItem Parent { get; set; }

		public List<TbMenuTree> Items { get; set; }
	}
}
