using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq.Expressions;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Html;

using System.Web.Routing;

namespace Twitter.Bootstrap.HtmlHelpers
{
	public static class TypeaheadExtensions
	{

		public static IHtmlString TypeaheadRowFor<TModel, TProperty>(this HtmlHelper<TModel> html,
		                                                             Expression<Func<TModel, TProperty>> expression)
		{
			return TypeaheadRowFor(html, expression, null);
		}

		public static IHtmlString TypeaheadRowFor<TModel, TProperty>(this HtmlHelper<TModel> html,
		                                                       Expression<Func<TModel, TProperty>> expression, object htmlAttributes)
		{
			RouteValueDictionary attributes = HtmlHelper.AnonymousObjectToHtmlAttributes(htmlAttributes);
			attributes.Remove("data-provider");
			attributes.Remove("autocomplete");

			attributes.Add("data-provider", "typeahead");
			attributes.Add("autocomplete", "off");

			return html.TextBoxRowFor(expression, attributes);
		}
	}
}
