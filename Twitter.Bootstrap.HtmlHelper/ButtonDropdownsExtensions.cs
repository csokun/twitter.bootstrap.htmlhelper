using System.Collections.Generic;
using System.Text;
using System.Web;
using System.Web.Mvc;
using System.Linq;
using System.Web.Mvc.Html;
using Twitter.Bootstrap.HtmlHelpers.ViewModels;

namespace Twitter.Bootstrap.HtmlHelpers
{
	//<div class="btn-group">
	//	<button type="button" class="btn btn-default dropdown-toggle" data-toggle="dropdown">
	//		Action <span class="caret"></span>
	//	</button>
	//	<ul class="dropdown-menu" role="menu">
	//		<li><a href="#">Action</a></li>
	//		<li><a href="#">Another action</a></li>
	//		<li><a href="#">Something else here</a></li>
	//		<li class="divider"></li>
	//		<li><a href="#">Separated link</a></li>
	//	</ul>
	//</div>
	public static class ButtonDropdownsExtensions
	{
		public static IHtmlString ButtonDropdowns(this HtmlHelper html, string actionName, IList<TbMenuItem> menuItems, object htmlAttributes = null)
		{
			var attributes = htmlAttributes as IDictionary<string, object> ??
								 HtmlHelper.AnonymousObjectToHtmlAttributes(htmlAttributes);

			var dropdown = new StringBuilder();

			var btnStyle = "btn btn-" + attributes.GetString("theme", "default");
			attributes.Remove("theme");

			// overwrite style
			var @class = attributes.ContainsKey("class") ? html.AttributeEncode(attributes["class"]) : string.Empty;

			if (!string.IsNullOrWhiteSpace(@class))
			{
				btnStyle += " " + @class;
			}

			var nItems = menuItems.Count(t => t != null && t.Visible);
			var downgrade = attributes.Get<bool>("downgrade", false);
			attributes.Remove("downgrade");

			if (nItems == 1 && downgrade)
			{
				var item = menuItems.First(t => t != null && t.Visible);

				var route = HtmlHelper.AnonymousObjectToHtmlAttributes(item.RouteValues);
				var attrs = HtmlHelper.AnonymousObjectToHtmlAttributes(item.Attributes);
				attrs.Ensure("class", btnStyle);

				var link = html.RouteLink(item.Text,
																	item.RouteName,
																	route,
																	attrs);


				dropdown.Append(link);
			}
			else
			{
				dropdown.Append("<div class=\"btn-group\">");

				BuildCaption(html, actionName, attributes, dropdown, btnStyle);

				// create menu item
				var items = menuItems.Where(t => t == null || t.Visible).ToList();
				BuildMenuItem(html, items, dropdown);

				dropdown.Append("</div>");
			}

			// create btn-group
			return new MvcHtmlString(dropdown.ToString());
		}

		private static void BuildCaption(HtmlHelper html, string actionName, IDictionary<string, object> attributes, StringBuilder dropdown,
																		 string btnStyle)
		{
			if (attributes.ContainsKey("split"))
			{
				dropdown.AppendFormat("<button type=\"button\" class=\"{0}\">{1}</button>",
															btnStyle,
															html.Encode(actionName));

				dropdown.AppendFormat(
					@"<button type=""button"" class=""{0} dropdown-toggle"" data-toggle=""dropdown"">
							<span class=""caret""></span>
						</button>", btnStyle);
			}
			else
			{
				dropdown.AppendFormat(@"<button type=""button"" class=""{0} dropdown-toggle"" data-toggle=""dropdown"">
						{1}	<span class=""caret""></span>
						</button>", btnStyle, html.Encode(actionName));
			}
		}

		private static void BuildMenuItem(HtmlHelper html, IList<TbMenuItem> menuItems, StringBuilder dropdown)
		{
			var itemCount = menuItems.Count;
			dropdown.Append("<ul class=\"dropdown-menu\" role=\"menu\">");
			for (var i = 0; i < itemCount; i++)
			{

				if (menuItems[i] == null)
				{
					if (i == itemCount-1) continue;

					dropdown.Append("<li class=\"divider\"></li>");
					continue;
				}

				var item = menuItems[i];

				if (!item.Visible)
					continue;

				dropdown.Append("<li>");

				dropdown.Append(html.RouteLink(
					item.Text, item.RouteName,
					item.RouteValues,
					item.Attributes));

				dropdown.Append("</li>");
			}
			dropdown.Append("</ul>");
		}
	}
}
