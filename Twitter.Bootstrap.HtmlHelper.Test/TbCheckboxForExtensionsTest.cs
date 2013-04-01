using System.Web.Mvc;
using Twitter.Bootstrap.HtmlHelpers.Test.Models;
using Xunit;

namespace Twitter.Bootstrap.HtmlHelpers.Test
{
	public class TbCheckboxForExtensionsTest
	{
	
		[Fact]
		public void Should_generate_checkbox_inside_div_controls()
		{
			// arrange
			var person = new Person()
			{
				IsActive = true
			};

			var viewData = new ViewDataDictionary<Person>()
			{
				Model = person
			};

			var helper = MvcHelper.GetHtmlHelper(viewData);
			var expected = @"<label class=""checkbox"" for=""IsActive""><input checked=""checked"" id=""IsActive"" name=""IsActive"" type=""checkbox"" value=""true"" /><input name=""IsActive"" type=""hidden"" value=""false"" />&nbsp;IsActive</label>";

			// act
			var html = helper.TbCheckboxFor(m => m.IsActive).ToHtmlString();

			// assert
			Assert.Contains(expected, html);
		}

	}
}
