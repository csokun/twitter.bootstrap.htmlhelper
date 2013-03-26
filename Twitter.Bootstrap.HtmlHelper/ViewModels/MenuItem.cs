﻿using System.Collections.Generic;

namespace Twitter.Bootstrap.HtmlHelpers.ViewModels
{
	public class TbMenuItem
	{
		private string _route;
		private string _action;
		private string _controller;

		public string Text { get; set; }

		public string Route
		{
			get { return string.IsNullOrWhiteSpace(_route) ? "Default" : _route; }
			set { _route = value; }
		}

		public string Controller
		{
			get { return string.IsNullOrWhiteSpace(_controller) ? "Home" : _controller; }
			set { _controller = value; }
		}

		public string Action
		{
			get { return string.IsNullOrWhiteSpace(_action) ? "Index" : _action; }
			set { _action = value; }
		}

		public string Key { get; set; }

		public bool Selected { get; set; }

		public List<TbMenuItem> Items { get; set; }
	}
}
