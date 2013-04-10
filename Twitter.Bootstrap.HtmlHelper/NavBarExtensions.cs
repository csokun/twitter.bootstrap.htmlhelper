using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Html;
using Twitter.Bootstrap.HtmlHelpers.ViewModels;

namespace Twitter.Bootstrap.HtmlHelpers
{
	public static class NavBarExtensions
	{
		public static IHtmlString NavBar(this HtmlHelper html, NavBar navBar, object htmlAttributes)
		{
			// detect current url

			const string collapsible = @"
						<button type=""button"" class=""btn btn-navbar"" data-toggle=""collapse"" data-target="".nav-collapse"">
							<span class=""icon-bar""></span>
							<span class=""icon-bar""></span>
							<span class=""icon-bar""></span>
						</button>";
			// lv1
			var placeholder = new TagBuilder("div");

			if (navBar.Fixed != NavBarDock.None)
			{
				var name = Enum.GetName(typeof (NavBarDock), navBar.Fixed);
				if (name != null)
					placeholder.AddCssClass("navbar-fixed-" + name.ToLower());
			}

			if (navBar.Inverse)
				placeholder.AddCssClass("navbar-inverse");

			placeholder.AddCssClass("navbar");

			// lv2
			var innerNavBar = new TagBuilder("div");
			innerNavBar.AddCssClass("navbar-inner");

			var container = new TagBuilder("div");
			container.AddCssClass("container");

			// brand insertion
			if (!string.IsNullOrEmpty(navBar.Brand))
			{
				var brand = new TagBuilder("a");
				brand.Attributes["href"] = "#";
				brand.AddCssClass("brand");
				brand.SetInnerText(navBar.Brand);

				container.InnerHtml += brand.ToString();
			}

			var htmlString = new StringBuilder();
			
			htmlString.Append("<ul class=\"nav\">");
			RenderMenuItem(html, htmlString, navBar.Items);
			htmlString.Append("</ul>");


			if (navBar.Collapsible)
			{
				var divCollapse = new TagBuilder("div");
				divCollapse.AddCssClass("nav-collapse collapse navbar-responsive-collapse");

				divCollapse.InnerHtml += htmlString;

				container.InnerHtml += collapsible;
				container.InnerHtml += MvcHtmlString.Create(divCollapse.ToString());
			}
			else
			{
				container.InnerHtml += htmlString;
			}

			innerNavBar.InnerHtml = container.ToString();		
			placeholder.InnerHtml = innerNavBar.ToString();

			return MvcHtmlString.Create(placeholder.ToString(TagRenderMode.Normal));
		}

		private static void RenderMenuItem(HtmlHelper html, StringBuilder htmlStringBuilder, IEnumerable<TbMenuTree> tree)
		{
			if(tree == null) return;

			foreach (var menuItem in tree)
			{
				if (menuItem == null)
				{
					htmlStringBuilder.Append("<li class=\"divider\"></li>");
					continue;
				}

				if (!menuItem.Visible) continue;

				if (menuItem.Leaf)
				{
					htmlStringBuilder.Append( menuItem.Selected ? "<li class=\"active\">" : "<li>" );
					
					htmlStringBuilder.Append(
						html.RouteLink(menuItem.Text, menuItem.Route,
						               new {action = menuItem.Action, controller = menuItem.Controller}));

					htmlStringBuilder.Append("</li>");

					continue;
				}

				htmlStringBuilder.Append("<li class=\"dropdown\">");
				htmlStringBuilder.AppendFormat(
					"<a data-toggle=\"dropdown\" class=\"dropdown-toggle\" href=\"#\">{0} <b class=\"caret\"></b></a>",
					html.Encode(menuItem.Text));
				htmlStringBuilder.Append("<ul class=\"dropdown-menu\">");

				RenderMenuItem(html, htmlStringBuilder, menuItem.Items);

				htmlStringBuilder.Append("</ul>");
				htmlStringBuilder.Append("</li>");
			}
		}

	}
}
