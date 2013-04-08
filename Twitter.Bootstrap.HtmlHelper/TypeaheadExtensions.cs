using System;
using System.Linq.Expressions;
using System.Web;
using System.Web.Mvc;

namespace Twitter.Bootstrap.HtmlHelpers
{
	public static class TypeaheadExtensions
	{
		public static IHtmlString Typeahead<TModel, TProperty>(this HtmlHelper<TModel> html,
		                                                       Expression<Func<TModel, TProperty>> expression,
		                                                       string[] source, object htmlAttributes)
		{
			return MvcHtmlString.Create(string.Empty);
		}
	}
}
