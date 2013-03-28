using System;
using System.Linq.Expressions;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace Twitter.Bootstrap.HtmlHelpers
{
	public static class TbCheckboxForExtensions
	{
		public static IHtmlString TbCheckboxFor<TModel, TProperty>(this HtmlHelper<TModel> html,
																																		Expression<Func<TModel, TProperty>> expression)
		{
			var htmlString = new StringBuilder();

			return MvcHtmlString.Create(string.Empty);
		}
	}
}
