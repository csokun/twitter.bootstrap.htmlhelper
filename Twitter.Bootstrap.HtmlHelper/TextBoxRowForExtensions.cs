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
			var hasIconAttached = attributes.ContainsKey("prepend") || attributes.ContainsKey("append");

			var wrap = new TagBuilder("div");

			if (attributes.ContainsKey("prepend"))
			{
				wrap.AddCssClass("input-prepend");
				wrap.InnerHtml = string.Format("<span class=\"add-on\"><i class=\"{0}\"></i></span>", html.AttributeEncode(attributes["prepend"]));
			} 
			
			wrap.InnerHtml += html.TextBoxFor(expression, attributes);

			if (attributes.ContainsKey("append"))
			{
				wrap.AddCssClass("input-append");
				wrap.InnerHtml += string.Format("<span class=\"add-on\"><i class=\"{0}\"></i></span>", html.AttributeEncode(attributes["append"]));
			}

			if (attributes.ContainsKey("hints"))
			{
				wrap.InnerHtml += string.Format("<span class=\"help-block\">{0}</span>", html.Encode(attributes["hints"]));
			}

			ctrl.InnerHtml = hasIconAttached ? wrap.ToString() : wrap.InnerHtml;

			// validation if required
			if (includeValidation)
			{
				ctrl.InnerHtml += html.ValidationMessageFor(expression);
			}

			controlGroup.InnerHtml = lbl + ctrl;

			return MvcHtmlString.Create(controlGroup.ToString());
		}
	}
}
