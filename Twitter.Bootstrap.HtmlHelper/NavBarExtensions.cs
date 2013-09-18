﻿using System;
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
			const string defaultNavCollapseTarget = ".navbar-ex1-collapse";
			const string collapsible = @"
						<button type=""button"" class=""navbar-toggle"" data-toggle=""collapse"" data-target="".navbar-ex1-collapse"">
							<span class=""sr-only"">Toggle navigation</span>
							<span class=""icon-bar""></span>
							<span class=""icon-bar""></span>
							<span class=""icon-bar""></span>
						</button>";
			
			var attributes = htmlAttributes as IDictionary<string, object> ??
								 HtmlHelper.AnonymousObjectToHtmlAttributes(htmlAttributes);
			
			var dataTarget = attributes.ContainsKey("data-target") ? html.AttributeEncode((string)attributes["data-target"]) : "ex1";
			dataTarget = string.Format("navbar-{0}-collapse", dataTarget);

			// lv1
			var placeholder = new TagBuilder("div");
			placeholder.AddCssClass("navbar navbar-default");
			placeholder.Attributes.Add(new KeyValuePair<string, string>("role", "navigation"));
			
			if (navBar.Fixed != NavBarDock.None)
			{
				var name = Enum.GetName(typeof (NavBarDock), navBar.Fixed);
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
				brand.SetInnerText(navBar.Brand);
			}

			var innerNavBar = new TagBuilder("div");
			innerNavBar.AddCssClass("collapse navbar-collapse");
			if (navBar.Collapsible)
			{
				header.InnerHtml = collapsible.Replace(defaultNavCollapseTarget, dataTarget);
				header.InnerHtml += brand;

				innerNavBar.AddCssClass(dataTarget);
			}
			else
			{
				header.InnerHtml = brand.ToString();
			}

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
				placeholder.InnerHtml = "<div class=\"container\">" + header + innerNavBar + "</div>"; 
			}
			

			return MvcHtmlString.Create(placeholder.ToString(TagRenderMode.Normal));
		}

		private static void RenderMenuItem(HtmlHelper html, StringBuilder htmlContent, IEnumerable<TbMenuTree> tree)
		{
			if(tree == null) return;

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
					htmlContent.Append( item.Selected ? "<li class=\"active\">" : "<li>" );

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
