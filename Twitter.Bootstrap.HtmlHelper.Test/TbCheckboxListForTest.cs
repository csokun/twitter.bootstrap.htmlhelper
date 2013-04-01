using System.Collections.Generic;
using System.Web.Mvc;
using Twitter.Bootstrap.HtmlHelpers.Test.Models;
using Xunit;

namespace Twitter.Bootstrap.HtmlHelpers.Test
{
	public class TbCheckboxListForTest
	{
		private HtmlHelper<MyOccupation> helper;
 
		public TbCheckboxListForTest()
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
								},
					Favorites = new SelectListItem[0]{}
				}
			};

			helper = MvcHelper.GetHtmlHelper(viewData);
		}

		[Fact]
		public void CheckboxForList_generate_necessary_dom_content()
		{
			// act
			var html = helper.TbCheckboxListRowFor(m => m.Occupations, false).ToHtmlString();

			// assert
			Assert.Contains(@"<div class=""control-group"">", html);
			Assert.Contains(@"<label class=""control-label"">Occupations</label>", html);

			Assert.Contains("Occupations[0].Value", html);
			Assert.Contains(@"name=""Occupations[0].Value"" value=""1""", html);
		}

		[Fact]
		public void CheckboxListFor_should_generate_label_using_display_attribute()
		{
			// act
			var html = helper.TbCheckboxListRowFor(m => m.Favorites, false).ToHtmlString();

			// assert
			Assert.Contains(@"<label class=""control-label"">My Favorites</label>", html);
		}

		[Fact]
		public void CheckboxListFor_support_inline_item_generation()
		{
			// act
			var html = helper.TbCheckboxListRowFor(m => m.Occupations, true).ToHtmlString();

			// assert
			Assert.Contains(@"<label class=""checkbox inline"">", html);
		}
	}
}
