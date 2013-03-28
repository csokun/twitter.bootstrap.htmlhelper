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
	public static class PasswordRowForExtensions
	{
		public static IHtmlString PasswordRowFor<TModel, TProperty>(this HtmlHelper<TModel> html,
																															 Expression<Func<TModel, TProperty>> expression)
		{
			return PasswordRowFor(html, expression, null);
		}

		public static IHtmlString PasswordRowFor<TModel, TProperty>(this HtmlHelper<TModel> html,
																															 Expression<Func<TModel, TProperty>> expression,
																															 object htmlAttributes)
		{
			return PasswordRowFor(html, expression, false, htmlAttributes);
		}

		public static IHtmlString PasswordRowFor<TModel, TProperty>(this HtmlHelper<TModel> html,
			Expression<Func<TModel, TProperty>> expression,
			bool includeValidation, object htmlAttributes)
		{

			var content = html.TextBoxRowFor(expression, includeValidation, htmlAttributes).ToHtmlString();
			content = content.Replace("type=\"text\"", "type=\"password\"");

			return MvcHtmlString.Create(content);
		}
	}
}
