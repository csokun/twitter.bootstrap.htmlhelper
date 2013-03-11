using System.Collections.Generic;
using System.Web.Mvc;
using Twitter.Bootstrap.HtmlHelpers.Test.Models;
using Xunit;

namespace Twitter.Bootstrap.HtmlHelpers.Test
{
	public class DropDownListHelperTest
	{
		private HtmlHelper<Person> helper;

		public DropDownListHelperTest()
		{
			var person = new Person()
			{
				Name = "Chorn Sokun",
				Id = 0,
				OccupationId = 1,
				Occupations = new List<Occupation>()
						{
							new Occupation() { Id = 1, Text = "Teacher" },
							new Occupation() { Id = 2, Text = "Information" }
						},
				Gender = new Gender() { Text = "Male" }
			};

			var viewData = new ViewDataDictionary<Person>()
			{
				Model = person
			};
			helper = MvcHelper.GetHtmlHelper(viewData);
		}

		[Fact]
		public void DropDownListRowFor_should_generate_inside_control_group()
		{
			// arrange
			var occupations = new List<Occupation>()
				{
					new Occupation() {Id = 1, Text = "Teacher"},
					new Occupation() {Id = 2, Text = "Information"}
				};
			var selectList = new SelectList(occupations, "Id", "Text");

			// act
			var html = helper.DropDownListRowFor(t => t.OccupationId, selectList, false, null).ToHtmlString();

			// assert
			Assert.Contains(@"<option selected=""selected"" value=""1"">Teacher</option>", html);
		}

		[Fact]
		public void DropDownListRowFor_should_generate_validation_message_for_if_needed()
		{
			helper.ViewContext.ClientValidationEnabled = true;
			helper.ViewContext.UnobtrusiveJavaScriptEnabled = true;
			var occupations = new List<Occupation>()
				{
					new Occupation() {Id = 1, Text = "Teacher"},
					new Occupation() {Id = 2, Text = "Information"}
				};
			var selectList = new SelectList(occupations, "Id", "Text");

			// act
			var html = helper.DropDownListRowFor(t => t.OccupationId, selectList, true, null).ToHtmlString();

			// assert
			Assert.Contains(@"field-validation-valid", html);
		}
	}
}
