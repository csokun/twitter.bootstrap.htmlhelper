using System;
using System.Linq.Expressions;
using System.Web;
using System.Web.Mvc;

namespace Twitter.Bootstrap.HtmlHelpers
{
	public static class NumberRowForExtensions
	{
		public static IHtmlString NumberRowFor<TModel, TProperty>(this HtmlHelper<TModel> html,
																															 Expression<Func<TModel, TProperty>> expression)
		{
			return NumberRowFor(html, expression, null);
		}

		public static IHtmlString NumberRowFor<TModel, TProperty>(this HtmlHelper<TModel> html,
			Expression<Func<TModel, TProperty>> expression, object htmlAttributes)
		{
			var content = html.TextBoxRowFor(expression, htmlAttributes).ToHtmlString();
			content = content.Replace("type=\"text\"", "type=\"number\"");

			return MvcHtmlString.Create(content);
		}
	}
}
