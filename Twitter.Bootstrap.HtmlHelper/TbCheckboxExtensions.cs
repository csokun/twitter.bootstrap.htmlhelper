using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Html;

namespace Twitter.Bootstrap.HtmlHelpers
{
	public static class TbCheckboxExtensions
	{
		public static IHtmlString TbCheckboxFor<TModel>(this HtmlHelper<TModel> html,
			Expression<Func<TModel, bool>> expression, object htmlAttributes = null)
		{
			var attributes = htmlAttributes as IDictionary<string, object> ??
								 HtmlHelper.AnonymousObjectToHtmlAttributes(htmlAttributes);

			var offset = attributes.Get<int>("offset", 2);
			var cols = 12 - offset;
			var metadata = ModelMetadata.FromLambdaExpression(expression, html.ViewData);

			var content = new StringBuilder();

			var inline = attributes.Get<bool>("inline", false);
			attributes.Remove("inline");
			
			if (!inline)
			{
				content.Append(@"<div class=""form-group"">");
				content.AppendFormat(@"	<div class=""col-lg-offset-{0} col-lg-{1}"">", offset, cols);
			}

			content.Append(@"<div class=""checkbox""><label>");			
			content.Append(html.CheckBoxFor(expression, new {@class = "checkbox"}).ToHtmlString());
			content.Append(html.Encode(metadata.GetDisplayName()));
			content.Append(@"</label></div>");

			if (!inline)
			{
				content.Append(@"</div></div>");
			}

			return MvcHtmlString.Create(content.ToString());
		}

		public static IHtmlString TbCheckboxListRowFor<TModel>(this HtmlHelper<TModel> html,
			Expression<Func<TModel, IEnumerable<SelectListItem>>> expression, object htmlAttributes = null)
		{
			if (expression == null)
			{
				throw new ArgumentNullException("expression");
			}
			var attributes = htmlAttributes as IDictionary<string, object> ??
								 HtmlHelper.AnonymousObjectToHtmlAttributes(htmlAttributes);
			
			var inline = attributes.Get<bool>("inline", false);
			var metadata = ModelMetadata.FromLambdaExpression(expression, html.ViewData);
			var selectList = metadata.Model as IEnumerable<SelectListItem>;
			if (selectList == null)
			{
				throw new ArgumentNullException("expression");
			}

			var name = ExpressionHelper.GetExpressionText(expression);
			var fullName = html.ViewContext.ViewData.TemplateInfo.GetFullHtmlFieldName(name);
			
			var dom = new StringBuilder();
			dom.Append(@"<div class=""form-group"">");

			dom.Append(@"<div class=""col-lg-offset-2 col-lg-10"">");
			
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

				if (!inline)
					dom.Append(@"<div class=""checkbox"">");

				// checkbox generator
				dom.AppendFormat("<label{0}>", inline ? " class=\"checkbox-inline\"" : string.Empty);
				dom.AppendFormat(@"<input type=""hidden"" name=""{0}.Value"" value=""{1}"" />", elementName,html.AttributeEncode(item.Value));
				dom.AppendFormat(@"<input type=""hidden"" name=""{0}.Text"" value=""{1}"" />", elementName, html.AttributeEncode(item.Text));
				dom.AppendFormat(@"<input id=""{0}_Selected"" class=""checkbox"" type=""checkbox"" name=""{1}.Selected"" value=""true"" {2}/>", elmentId, elementName, selected);
				dom.AppendFormat(@"<input type=""hidden"" name=""{0}.Selected"" value=""false"" />", elementName);
				dom.AppendFormat(" {0}</label>", html.AttributeEncode(item.Text));

				if (!inline)
					dom.Append(@"</div>");

				i++;
			}

			dom.Append("</div></div>");
			return MvcHtmlString.Create(dom.ToString());
		}
	}
}
