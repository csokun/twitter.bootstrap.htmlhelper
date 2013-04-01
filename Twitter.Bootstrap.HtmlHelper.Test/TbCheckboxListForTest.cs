using System.Collections.Generic;
using System.Web.Mvc;
using Twitter.Bootstrap.HtmlHelpers.Test.Models;
using Xunit;

namespace Twitter.Bootstrap.HtmlHelpers.Test
{
	public class TbCheckboxListForTest
	{

		[Fact]
		public void CheckboxList_support()
		{
			// arrange
			var viewData = new ViewDataDictionary<MyOccupation>()
			{
				Model = new MyOccupation()
				{
					Occupations = new List<SelectListItem>()
								{
									new SelectListItem() {Text = "Checkbox #1", Value = "1", Selected = true},
									new SelectListItem() {Text = "Checkbox #2", Value = "2", Selected = false},
								}
				}
			};

			var helper = MvcHelper.GetHtmlHelper(viewData);

			// act
			var html = helper.TbCheckboxListRowFor(m => m.Occupations, false).ToHtmlString();

			// assert
			Assert.Contains("Occupations[0].Value", html);
			Assert.Contains(@"name=""Occupations[0].Value"" value=""1""", html);
		}
	}
}
