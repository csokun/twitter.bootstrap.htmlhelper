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
			Expression<Func<TModel, TProperty>> expression, object htmlAttributes)
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
			// HACKME: this only work for horizontal-form
			var lbl = html.LabelFor(expression, new {@class = "col-lg-2 control-label"}).ToHtmlString();
	
			// create controls block
			var ctrl = new TagBuilder("div");
			ctrl.AddCssClass("col-lg-" + attributes.Get<int>("gridcol", 10));
			attributes.Remove("gridcol");

			// actual controls
			var hasIconAttached = attributes.ContainsKey("prepend") || attributes.ContainsKey("append");

			var wrap = new TagBuilder("div");

			if (attributes.ContainsKey("prepend"))
			{
				wrap.AddCssClass("input-prepend");
				wrap.InnerHtml = string.Format("<span class=\"input-group-addon\">{0}</span>", html.AttributeEncode(attributes["prepend"]));
			}

			attributes.Ensure("class", "form-control");

			wrap.InnerHtml += html.TextBoxFor(expression, attributes);

			if (attributes.ContainsKey("append"))
			{
				wrap.AddCssClass("input-append");
				wrap.InnerHtml += string.Format("<span class=\"input-group-addon\">{0}</span>", html.AttributeEncode(attributes["append"]));
			}

			if (attributes.ContainsKey("hints"))
			{
				wrap.InnerHtml += string.Format("<span class=\"help-block\">{0}</span>", html.Encode(attributes["hints"]));
			}

			ctrl.InnerHtml = hasIconAttached ? wrap.ToString() : wrap.InnerHtml;

			// validation if required
			var validation = string.Format("{0}", html.ValidationMessageFor(expression, null, new { @class = "help-inline" }));

			if (!string.IsNullOrWhiteSpace(validation))
			{
				ctrl.InnerHtml += validation;
			}

			controlGroup.InnerHtml = lbl + ctrl;

			return MvcHtmlString.Create(controlGroup.ToString());
		}
	}
}
