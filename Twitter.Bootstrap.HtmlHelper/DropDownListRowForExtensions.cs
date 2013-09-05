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
			return DropDownListRowFor(html, expression, selectList, null);
		}


		public static IHtmlString DropDownListRowFor<TModel, TProperty>(this HtmlHelper<TModel> html, 
			Expression<Func<TModel, TProperty>> expression,
			IEnumerable<SelectListItem> selectList, 
			object htmlAttributes)
		{
			if (expression == null)
			{
				throw new ArgumentNullException("expression");
			}

			var attributes = htmlAttributes as IDictionary<string, object> ??
				HtmlHelper.AnonymousObjectToHtmlAttributes(htmlAttributes);

			var controlGroup = new TagBuilder("div");
			controlGroup.AddCssClass("form-group");

			// create label
			var lbl = html.LabelFor(expression, new { @class = "col-lg-2 control-label" }).ToHtmlString();

			// create controls block
			var ctrl = new TagBuilder("div");
		
			ctrl.AddCssClass("col-lg-" + attributes.Get<int>("gridcol", 10));
			attributes.Remove("gridcol");

			// actual controls
			attributes.Ensure("class", "form-control");

			var input = html.DropDownListFor(expression, selectList, attributes);

			ctrl.InnerHtml = input.ToHtmlString();
			if (attributes.ContainsKey("hints"))
			{
				ctrl.InnerHtml += string.Format("<span class=\"help-block\">{0}</span>", html.Encode(attributes["hints"]));
			}

			// validation if required
			var validation = string.Format("{0}", html.ValidationMessageFor(expression, null, new { @class = "help-inline" }));
			if (!string.IsNullOrWhiteSpace(validation))
			{
				ctrl.InnerHtml += validation;
				//controlGroup.AddCssClass("error");
			}

			controlGroup.InnerHtml = lbl + ctrl;

			return MvcHtmlString.Create(controlGroup.ToString());
		}

	}
}
