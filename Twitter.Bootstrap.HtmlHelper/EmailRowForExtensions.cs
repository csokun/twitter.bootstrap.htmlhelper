using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace Twitter.Bootstrap.HtmlHelpers
{
	public static class EmailRowForExtensions
	{
		public static IHtmlString EmailRowFor<TModel, TProperty>(this HtmlHelper<TModel> html,
																															 Expression<Func<TModel, TProperty>> expression)
		{
			return EmailRowFor(html, expression, null);
		}

		public static IHtmlString EmailRowFor<TModel, TProperty>(this HtmlHelper<TModel> html,
			Expression<Func<TModel, TProperty>> expression, object htmlAttributes)
		{
			var content = html.TextBoxRowFor(expression, htmlAttributes).ToHtmlString();
			content = content.Replace("type=\"text\"", "type=\"email\"");

			return MvcHtmlString.Create(content);
		}
	}
}
