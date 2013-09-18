using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Html;

namespace Twitter.Bootstrap.HtmlHelpers
{
	public static class DatePickerForExtensions
	{
		public static IHtmlString DatepickerRowFor<TModel, TProperty>(this HtmlHelper<TModel> html,
		                                                              Expression<Func<TModel, TProperty>> expression)
		{
			return DatepickerRowFor(html, expression, null);
		}

		public static IHtmlString DatepickerRowFor<TModel, TProperty>(this HtmlHelper<TModel> html,
		                                                              Expression<Func<TModel, TProperty>> expression,
		                                                              object htmlAttributes)
		{
			VerifyExpression(html, expression);

			var attributes = htmlAttributes as IDictionary<string, object> ??
				HtmlHelper.AnonymousObjectToHtmlAttributes(htmlAttributes);
			
			var name = ExpressionHelper.GetExpressionText(expression);
			string fullName = html.ViewContext.ViewData.TemplateInfo.GetFullHtmlFieldName(name);

			var formGroup = new TagBuilder("div");
			formGroup.AddCssClass("form-group");

			// create label
			var lbl = html.WriteLabelFor(expression, attributes, false);

			// create controls block
			var wrap = DatepickerTagBuilder(html, expression, attributes).InnerHtml;

			formGroup.InnerHtml = lbl + wrap;
			
			// validation if required
			var validation = string.Format("{0}", html.ValidationMessageFor(expression, null, new { @class = "help-inline" }));

			if (!attributes.ContainsKey("readonly") && !string.IsNullOrWhiteSpace(validation))
			{
				formGroup.InnerHtml += validation;
			}

			return MvcHtmlString.Create(formGroup.ToString());
		}

		private static void VerifyExpression<TModel, TProperty>(HtmlHelper<TModel> html, Expression<Func<TModel, TProperty>> expression)
		{
			if (expression == null)
			{
				throw new ArgumentNullException("expression");
			}

			var metadata = ModelMetadata.FromLambdaExpression(expression, html.ViewData);

			if (metadata.ModelType.Name != "DateTime" && metadata.ModelType.FullName != typeof (DateTime?).FullName)
			{
				throw new ArgumentException("Input value is not DateTime.");
			}
		}

		private static TagBuilder DatepickerTagBuilder<TModel, TProperty>(HtmlHelper<TModel> html, Expression<Func<TModel, TProperty>> expression,
		                                                                  IDictionary<string, object> attributes)
		{
			var metadata = ModelMetadata.FromLambdaExpression(expression, html.ViewData);
			var div1 = new TagBuilder("div");

			var wrap = new TagBuilder("div");
			wrap.AddCssClass("input-group date");

			var inline = attributes.Get<bool>("inline", false);
			attributes.Remove("inline");
			if (inline)
			{
				wrap.Attributes.Add("style", "display: inline-table; width: 150px;");
			}
			
			// id
			var name = ExpressionHelper.GetExpressionText(expression);
			string fullName = html.ViewContext.ViewData.TemplateInfo.GetFullHtmlFieldName(name);
			wrap.GenerateId(fullName + "_datepicker");

			// data-date-format
			var dateFormat = attributes.ContainsKey("data-date-format")
				                 ? html.AttributeEncode(attributes["data-date-format"])
				                 : "yyyy-mm-dd";
			wrap.Attributes.Add("data-date-format", dateFormat);

			// data-date
			var dotNetDateFormat = dateFormat.Replace("m", "M");

			var value = GetDateValue(metadata, dotNetDateFormat);

			wrap.Attributes.Add("data-date",
			                    attributes.ContainsKey("data-date")
				                    ? html.AttributeEncode(attributes["data-date"])
				                    : value);

			var readOnly = attributes.ContainsKey("readonly");

			// Date validation @ client-side is troublesome ~
			var clientValidationEnabled = html.ViewContext.ClientValidationEnabled;
			var unobtrusiveJavaScriptEnabled = html.ViewContext.UnobtrusiveJavaScriptEnabled;
			html.ViewContext.ClientValidationEnabled = false;
			html.ViewContext.UnobtrusiveJavaScriptEnabled = false;

			// Why don't I hook attributes directly?
			wrap.InnerHtml += html.TextBox(fullName, value, new { @class = "form-control", @size = 16, @readonly = "readonly" });

			html.ViewContext.ClientValidationEnabled = clientValidationEnabled;
			html.ViewContext.UnobtrusiveJavaScriptEnabled = unobtrusiveJavaScriptEnabled;

			if (!readOnly)
			{
				wrap.InnerHtml += "<span class=\"input-group-addon\"><i class=\"glyphicon glyphicon-calendar\"></i></span>";
			}
				
			if (attributes.ContainsKey("hints"))
			{
				wrap.InnerHtml += string.Format("<span class=\"help-block\">{0}</span>", html.Encode(attributes["hints"]));
			}

			div1.InnerHtml = wrap.ToString() + html.Hidden(fullName + ".DateFormat", dotNetDateFormat);

			return div1;
		}

		private static string GetDateValue(ModelMetadata metadata, string dotNetDateFormat)
		{
			DateTime date;

			if (metadata.ModelType.FullName == typeof (DateTime?).FullName)
			{
				var nDate = (DateTime?) metadata.Model;
				date = nDate.HasValue ? nDate.Value : DateTime.Today;
			}
			else
			{
				date = (DateTime) metadata.Model;
			}

			var value = (date <= DateTime.MinValue)
				            ? DateTime.Today.ToString(dotNetDateFormat)
				            : date.ToString(dotNetDateFormat);
			return value;
		}

		public static IHtmlString DatepickerFor<TModel, TProperty>(this HtmlHelper<TModel> html,
		                                                           Expression<Func<TModel, TProperty>> expression)
		{
			return DatepickerFor(html, expression, true, null);
		}

		public static IHtmlString DatepickerFor<TModel, TProperty>(this HtmlHelper<TModel> html,
		                                                           Expression<Func<TModel, TProperty>> expression, bool showLabel)
		{
			return DatepickerFor(html, expression, showLabel, null);
		}

		public static IHtmlString DatepickerFor<TModel, TProperty>(this HtmlHelper<TModel> html,
		                                                    Expression<Func<TModel, TProperty>> expression, bool showLabel, object htmlAttributes)
		{
			VerifyExpression(html, expression);

			var attributes = htmlAttributes as IDictionary<string, object> ??
				HtmlHelper.AnonymousObjectToHtmlAttributes(htmlAttributes);

			var tagBuilder = DatepickerTagBuilder(html, expression, attributes).InnerHtml;

			if (!showLabel)
			{
				return MvcHtmlString.Create( tagBuilder );
			}
				
			var label = html.LabelFor(expression);

			return MvcHtmlString.Create(label + tagBuilder);
		}

	}
}
