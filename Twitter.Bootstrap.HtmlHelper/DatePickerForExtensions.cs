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
			if (expression == null)
			{
				throw new ArgumentNullException("expression");
			}
			
			var attributes = htmlAttributes as IDictionary<string, object> ??
				HtmlHelper.AnonymousObjectToHtmlAttributes(htmlAttributes);

			var metadata = ModelMetadata.FromLambdaExpression(expression, html.ViewData);
			
			if (metadata.ModelType.Name != "DateTime" && metadata.ModelType.FullName != typeof(DateTime?).FullName)
			{
				throw new ArgumentException("Input value is not DateTime.");
			}

			var controlGroup = new TagBuilder("div");
			controlGroup.AddCssClass("control-group");

			// create label
			var lbl = html.LabelFor(expression, new { @class = "control-label" }).ToHtmlString();

			// create controls block
			var ctrl = new TagBuilder("div");
			ctrl.AddCssClass("controls");

			var wrap = DatepickerTagBuilder(html, expression, attributes, metadata);

			ctrl.InnerHtml = wrap.ToString();

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

		private static TagBuilder DatepickerTagBuilder<TModel, TProperty>(HtmlHelper<TModel> html, Expression<Func<TModel, TProperty>> expression,
		                                                                  IDictionary<string, object> attributes, ModelMetadata metadata)
		{
			var wrap = new TagBuilder("div");
			wrap.AddCssClass("input-append date");
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

			wrap.InnerHtml += html.TextBox(fullName, value, new {@class = "input-small", @size = 16, @readonly = "readonly"});
			wrap.InnerHtml += html.Hidden(fullName + ".DateFormat", dotNetDateFormat);
			//wrap.InnerHtml += string.Format(@"<input type=""text"" class=""span2"" id=""{0}"" name=""{1}"" value=""{2}"" readonly size=16 />", fullName,
			//																fullName, value);
			wrap.InnerHtml += "<span class=\"add-on\"><i class=\"icon-calendar\"></i></span>";

			if (attributes.ContainsKey("hints"))
			{
				wrap.InnerHtml += string.Format("<span class=\"help-block\">{0}</span>", html.Encode(attributes["hints"]));
			}
			return wrap;
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
		                                                    Expression<Func<TProperty>> expression, object htmlAttribute)
		{
			return MvcHtmlString.Empty;
		}

	}
}
