using System;
using System.Collections.Generic;
using System.Text;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Html;
using Twitter.Bootstrap.HtmlHelpers.ViewModels;

namespace Twitter.Bootstrap.HtmlHelpers
{
	public static class NavBarExtensions
	{
		public static IHtmlString NavBar(this HtmlHelper html, NavBar navBar, object htmlAttributes = null)
		{
			// detect current url
			const string collapsible = @"
						<button type=""button"" class=""navbar-toggle collapsed"" data-toggle=""collapse"" data-target=""#{0}"">
							<span class=""sr-only"">Toggle navigation</span>
							<span class=""icon-bar""></span>
							<span class=""icon-bar""></span>
							<span class=""icon-bar""></span>
						</button>";

			var attributes = htmlAttributes as IDictionary<string, object> ??
								 HtmlHelper.AnonymousObjectToHtmlAttributes(htmlAttributes);

			var collapsibleTarget = attributes.ContainsKey("data-collapsible-target") ?
					html.AttributeEncode((string)attributes["data-collapsible-target"]) : "bs-navbar-collapse";

			// lv1
			var placeholder = new TagBuilder("div");
			placeholder.AddCssClass("navbar navbar-default");

			if (navBar.Fixed != NavBarDock.None)
			{
				var name = Enum.GetName(typeof(NavBarDock), navBar.Fixed);
				if (name != null)
					placeholder.AddCssClass("navbar-fixed-" + name.ToLower());
			}

			if (navBar.Inverse)
				placeholder.AddCssClass("navbar-inverse");

			// brand

			var header = new TagBuilder("div");
			header.AddCssClass("navbar-header");

			// brand insertion
			var brand = new TagBuilder("a");
			if (!string.IsNullOrEmpty(navBar.Brand))
			{
				brand.Attributes["href"] = "/";
				brand.AddCssClass("navbar-brand");
				brand.InnerHtml = navBar.Brand;
			}

			var innerNavBar = new TagBuilder("nav");
			innerNavBar.Attributes.Add("role", "navigation");

			if (navBar.Collapsible)
			{
				header.InnerHtml = string.Format(collapsible, collapsibleTarget);
				header.InnerHtml += brand;

				innerNavBar.GenerateId(collapsibleTarget);
			}
			else
			{
				header.InnerHtml = brand.ToString();
			}
			innerNavBar.AddCssClass("navbar-collapse collapse");

			// content
			var elements = new StringBuilder();

			elements.Append("<ul class=\"nav navbar-nav\">");
			RenderMenuItem(html, elements, navBar.Items);
			elements.Append("</ul>");

			innerNavBar.InnerHtml = elements.ToString();

			if (attributes.ContainsKey("wrap") && attributes["wrap"].Equals("false"))
			{
				placeholder.InnerHtml = header + innerNavBar.ToString();
			}
			else
			{
				var containerCss = navBar.Fluid? "container-fluid" : "container";
				placeholder.InnerHtml = "<div class=\"" + containerCss  + "\">" + header + innerNavBar + "</div>";
			}


			return MvcHtmlString.Create(placeholder.ToString(TagRenderMode.Normal));
		}

		private static void RenderMenuItem(HtmlHelper html, StringBuilder htmlContent, IEnumerable<TbMenuTree> tree)
		{
			if (tree == null) return;

			foreach (var item in tree)
			{
				if (item == null)
				{
					htmlContent.Append("<li class=\"divider\"></li>");
					continue;
				}

				if (!item.Visible) continue;

				if (item.Leaf)
				{
					htmlContent.Append(item.Selected ? "<li class=\"active\">" : "<li>");

					htmlContent.Append(html.RouteLink(
						item.Text,
						item.RouteName,
						item.RouteValues,
						item.Attributes));

					htmlContent.Append("</li>");

					continue;
				}

				htmlContent.Append("<li class=\"dropdown\">");
				htmlContent.AppendFormat(
					"<a data-toggle=\"dropdown\" class=\"dropdown-toggle\" href=\"#\">{0} <b class=\"caret\"></b></a>",
					html.Encode(item.Text));
				htmlContent.Append("<ul class=\"dropdown-menu\">");

				RenderMenuItem(html, htmlContent, item.Items);

				htmlContent.Append("</ul>");
				htmlContent.Append("</li>");
			}
		}

	}
}
