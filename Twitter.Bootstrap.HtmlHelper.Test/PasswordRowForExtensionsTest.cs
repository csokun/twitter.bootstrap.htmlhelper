using System.Web.Mvc;
using Twitter.Bootstrap.HtmlHelpers.Test.Models;
using Xunit;

namespace Twitter.Bootstrap.HtmlHelpers.Test
{
	public class PasswordRowForExtensionsTest
	{
		private HtmlHelper<Person> helper;

		public PasswordRowForExtensionsTest()
		{
			var person = new Person()
				{
					Name = "Chorn Sokun",
					Id = 0,
					OccupationId = 1,
					Gender = new Gender() {Text = "Male"}
				};

			var viewData = new ViewDataDictionary<Person>()
				{
					Model = person
				};
			helper = MvcHelper.GetHtmlHelper(viewData);
		}

		[Fact]
		public void TextBoxRowFor_should_generate_div_class_control_group()
		{
			// Act			
			var html = helper.PasswordRowFor(t => t.Password).ToHtmlString();

			// Assert
			Assert.Contains(@"type=""password""", html);
			Assert.Contains("name=\"Password\"", html);
		}
	}
}
