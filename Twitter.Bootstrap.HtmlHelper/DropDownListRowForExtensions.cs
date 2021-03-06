﻿using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Html;

namespace Twitter.Bootstrap.HtmlHelpers
{
	public static class DropDownListRowForExtensions {

		public static IHtmlString DropDownListRowFor<TModel, TProperty>(this HtmlHelper<TModel> html,
		                                                                Expression<Func<TModel, TProperty>> expression,
		                                                                IEnumerable<SelectListItem> selectList,
		                                                                object htmlAttributes = null)
		{
			if (expression == null)
			{
				throw new ArgumentNullException("expression");
			}

			var attributes = htmlAttributes as IDictionary<string, object> ??
			                 HtmlHelper.AnonymousObjectToHtmlAttributes(htmlAttributes);

			var controlGroup = new TagBuilder("div");
			controlGroup.AddCssClass("form-group");

			var inline = attributes.Get<bool>("inline", false);
			attributes.Remove("inline");

			// create label
			var lbl = html.WriteLabelFor(expression, attributes, inline);

			// create controls block
			var ctrl = new TagBuilder("div");

			ctrl.AddCssClass("col-lg-" + attributes.Get<int>("controlcols", 10));
			attributes.Remove("controlcols");

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
			}

			controlGroup.InnerHtml = lbl + (inline ? ctrl.InnerHtml : ctrl.ToString());

			return MvcHtmlString.Create(controlGroup.ToString());
		}

	}
}
