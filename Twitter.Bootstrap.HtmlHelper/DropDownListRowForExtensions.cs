using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Html;

namespace Twitter.Bootstrap.HtmlHelpers
{
	public static class DropDownListRowForExtensions {

		public static IHtmlString DropDownListRowFor<TModel, TProperty>(this HtmlHelper<TModel> html,
		                                                                Expression<Func<TModel, TProperty>> expression,
		                                                                IEnumerable<SelectListItem> selectList)
		{
			return DropDownListRowFor(html, expression, selectList, false, null);
		}

		public static IHtmlString DropDownListRowFor<TModel, TProperty>(this HtmlHelper<TModel> html,
		                                                                Expression<Func<TModel, TProperty>> expression,
		                                                                IEnumerable<SelectListItem> selectList,
		                                                                bool includeValidation)
		{
			return DropDownListRowFor(html, expression, selectList, includeValidation, null);
		}

		public static IHtmlString DropDownListRowFor<TModel, TProperty>(this HtmlHelper<TModel> html, 
			Expression<Func<TModel, TProperty>> expression,
			IEnumerable<SelectListItem> selectList, 
			bool includeValidation,
			IDictionary<string, object> htmlAttributes)
		{
			if (expression == null)
			{
				throw new ArgumentNullException("expression");
			}

			var controlGroup = new TagBuilder("div");
			controlGroup.AddCssClass("control-group");

			// create label
			var lbl = html.LabelFor(expression, new { @class = "control-label" }).ToHtmlString();

			// create controls block
			var ctrl = new TagBuilder("div");
			ctrl.AddCssClass("controls");

			// actual controls
			var input = html.DropDownListFor(expression, selectList, htmlAttributes);
			ctrl.InnerHtml = input.ToHtmlString();
			// validation if required
			if (includeValidation)
			{
				ctrl.InnerHtml += html.ValidationMessageFor(expression).ToHtmlString();
			}

			controlGroup.InnerHtml = lbl + ctrl;

			return MvcHtmlString.Create(controlGroup.ToString());
		}

	}
}
