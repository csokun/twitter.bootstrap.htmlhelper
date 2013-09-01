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
			var expected = @"<div class=""checkbox""><label><input checked=""checked"" class=""checkbox"" id=""IsActive"" name=""IsActive"" type=""checkbox"" value=""true"" /><input name=""IsActive"" type=""hidden"" value=""false"" />&nbsp;IsActive</label></div>";

			// act
			var html = helper.TbCheckboxFor(m => m.IsActive).ToHtmlString();

			// assert
			Assert.Equal(expected, html);
		}

	}
}
