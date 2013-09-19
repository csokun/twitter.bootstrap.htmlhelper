using System.Collections.Generic;
using System.Text;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc;
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

			var btnStyle = "btn";
			var theme = attributes.ContainsKey("theme") ? "btn-" + html.AttributeEncode(attributes["theme"]) : "btn-default";
			btnStyle += " " + theme;
			// overwrite style
			var @class = attributes.ContainsKey("class") ? html.AttributeEncode(attributes["class"]) : string.Empty;

			if (!string.IsNullOrWhiteSpace(@class))
			{
				btnStyle += " " + @class;
			}

			dropdown.Append("<div class=\"btn-group\">");
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

			// create menu item
			dropdown.Append("<ul class=\"dropdown-menu\" role=\"menu\">");
			foreach (var item in menuItems)
			{
				
				if (item == null)
				{
					dropdown.Append("<li class=\"divider\"></li>");
					continue;
				}

				if (!item.Visible) 
					continue;

				dropdown.Append("<li>");
					
				dropdown.Append(html.RouteLink(
					item.Text, item.RouteName,
					item.RouteValues,
					item.Attributes));

				dropdown.Append("</li>");

			}
			dropdown.Append("</ul></div>");

			// create btn-group
			return new MvcHtmlString(dropdown.ToString());
		}
	}
}
