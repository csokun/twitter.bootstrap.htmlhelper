using System.Web.Mvc;
using Twitter.Bootstrap.HtmlHelpers.Test.Models;
using Xunit;

namespace Twitter.Bootstrap.HtmlHelpers.Test
{
	public class TbCheckboxForExtensionsTest
	{
		private Person _person;
		private ViewDataDictionary<Person> _viewData;

		public TbCheckboxForExtensionsTest()
		{
			_person = new Person()
			{
				IsActive = true
			};
			_viewData = new ViewDataDictionary<Person>()
			{
				Model = _person
			};
		}

		[Fact]
		public void Should_generate_checkbox_inside_div_controls()
		{
			// arrange
			var helper = MvcHelper.GetHtmlHelper(_viewData);

			// act
			var html = helper.TbCheckboxFor(m => m.IsActive).ToHtmlString();

			// assert
			Assert.Contains(@"<div class=""checkbox"">", html);
			Assert.Contains(@"Active</label>", html);
		}
	}
}
