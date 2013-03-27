using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Html;

namespace Twitter.Bootstrap.HtmlHelpers
{
	public static class TextBoxRowForExtensions
	{

		public static IHtmlString TextBoxRowFor<TModel, TProperty>(this HtmlHelper<TModel> html,
		                                                           Expression<Func<TModel, TProperty>> expression)
		{
			return TextBoxRowFor(html, expression, null);
		}

		public static IHtmlString TextBoxRowFor<TModel, TProperty>(this HtmlHelper<TModel> html,
		                                                           Expression<Func<TModel, TProperty>> expression,
		                                                           object htmlAttributes)
		{
			return TextBoxRowFor(html, expression, false, htmlAttributes);
		}

		public static IHtmlString TextBoxRowFor<TModel, TProperty>(this HtmlHelper<TModel> html,
			Expression<Func<TModel, TProperty>> expression, 
			bool includeValidation, object htmlAttributes)
		{
			if (expression == null)
			{
				throw new ArgumentNullException("expression");
			}

			var attributes = HtmlHelper.AnonymousObjectToHtmlAttributes(htmlAttributes);
			
			var controlGroup = new TagBuilder("div");
			controlGroup.AddCssClass("control-group");

			// create label
			var lbl = html.LabelFor(expression, new {@class = "control-label"}).ToHtmlString();
	
			// create controls block
			var ctrl = new TagBuilder("div");
			ctrl.AddCssClass("controls");
			
			// actual controls
			var input = html.TextBoxFor(expression, attributes);
			ctrl.InnerHtml = input.ToHtmlString();
			if (attributes.ContainsKey("hints"))
			{
				ctrl.InnerHtml += string.Format("<span class=\"help-block\">{0}</span>", html.Encode(attributes["hints"]));
			}

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
