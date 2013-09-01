using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Html;

namespace Twitter.Bootstrap.HtmlHelpers
{
	public static class TbCheckboxExtensions
	{
		public static IHtmlString TbCheckboxFor<TModel>(this HtmlHelper<TModel> html,
																																		Expression<Func<TModel, bool>> expression)
		{
			// create controls block
			var wrap = new TagBuilder("div");
			wrap.AddCssClass("checkbox");

			// wrap
			wrap.InnerHtml = LabelCheckbox(html, expression);

			return MvcHtmlString.Create(wrap.ToString());
		}

		private static string LabelCheckbox<TModel>(HtmlHelper<TModel> html, Expression<Func<TModel, bool>> expression, bool inline = false)
		{
			const string pattern = "<label.+?>(.+?</label>)";
			var checkBox = html.CheckBoxFor(expression, new {@class="checkbox"}).ToHtmlString();
			var label = html.LabelFor(expression, new {@class = inline ? "checkbox inline" : "checkbox"}).ToHtmlString();

			// search & replace labelText with checkbox 
			var regEx = new Regex(pattern);
			var match = regEx.Match(label);

			var labelCheckbox = string.Format("<label>{0}&nbsp;{1}", checkBox, match.Groups[1]);
			return labelCheckbox;
		}

		public static IHtmlString TbCheckboxListRowFor<TModel>(this HtmlHelper<TModel> html,
			Expression<Func<TModel, IEnumerable<SelectListItem>>> expression, bool inline)
		{
			if (expression == null)
			{
				throw new ArgumentNullException("expression");
			}
			
			ModelMetadata metadata = ModelMetadata.FromLambdaExpression(expression, html.ViewData);
			var selectList = metadata.Model as IEnumerable<SelectListItem>;
			if (selectList == null)
			{
				throw new ArgumentNullException("expression");
			}

			var name = ExpressionHelper.GetExpressionText(expression);
			string fullName = html.ViewContext.ViewData.TemplateInfo.GetFullHtmlFieldName(name);
			
			var dom = new StringBuilder();
			dom.Append(@"<div class=""col-lg-10"">");
			
			// label
			string labelText = metadata.DisplayName ?? metadata.PropertyName;
			dom.AppendFormat(@"<label class=""col-lg-2 control-label"">{0}</label>", html.Encode(labelText));

			// controls
			var i = 0;
			foreach (var item in selectList)
			{
				var elementName = string.Format("{0}[{1}]", fullName, i);
				var elmentId = string.Format("{0}_{1}", fullName, i);
				var selected = item.Selected ? "checked" : string.Empty;

				elementName = html.AttributeEncode(elementName);

				// checkbox generator
				dom.AppendFormat("<label{0}>", inline ? " class=\"checkbox-inline\"" : string.Empty);
				dom.AppendFormat(@"<input type=""hidden"" name=""{0}.Value"" value=""{1}"" />", elementName,html.AttributeEncode(item.Value));
				dom.AppendFormat(@"<input type=""hidden"" name=""{0}.Text"" value=""{1}"" />", elementName, html.AttributeEncode(item.Text));
				dom.AppendFormat(@"<input id=""{0}_Selected"" class=""checkbox"" type=""checkbox"" name=""{1}.Selected"" value=""true"" {2}/>", elmentId, elementName, selected);
				dom.AppendFormat(@"<input type=""hidden"" name=""{0}.Selected"" value=""false"" />", elementName);
				dom.AppendFormat(" {0}</label>", html.AttributeEncode(item.Text));

				i++;
			}

			dom.Append("</div>");
			return MvcHtmlString.Create(dom.ToString());
		}
	}
}
