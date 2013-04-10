using System.Collections.Generic;
using System.Text;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Html;
using Twitter.Bootstrap.HtmlHelpers.ViewModels;

namespace Twitter.Bootstrap.HtmlHelpers
{
	public static class BreadcrumbsExtensions
	{
		public static IHtmlString Breadcrumbs(this HtmlHelper htmlHelper, IList<TbMenuItem> menuItems)
		{
			var htmlString = new StringBuilder();

			htmlString.Append("<ul class=\"breadcrumb\">\r\n");
			var totalItem = menuItems.Count;
			for (var i = 0; i < totalItem; i++)
			{
				var menu = menuItems[i];
				var lastItem = (i == (totalItem - 1));
				if (lastItem)
				{
					htmlString.AppendFormat("<li class=\"active\">{0}</li>\r\n", menu.Text);
					continue;
				}

				htmlString.AppendFormat("<li>{0} <span class=\"divider\">/</span></li>\r\n",
					htmlHelper.RouteLink(menu.Text, menu.Route, new { controller = menu.Controller, action = menu.Action }));
			}

				htmlString.Append("</ul>");

			return MvcHtmlString.Create(htmlString.ToString());
		}
	}
}
