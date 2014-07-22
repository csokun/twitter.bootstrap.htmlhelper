using System.Collections.Generic;
using System.Web.Mvc;
using Twitter.Bootstrap.HtmlHelpers.Test.Models;
using Xunit;

namespace Twitter.Bootstrap.HtmlHelpers.Test
{
	public class DropDownListHelperTest
	{
		private readonly HtmlHelper<Person> _helper;
		private SelectList _selectList; 

		public DropDownListHelperTest()
		{
			var occupations = new List<Occupation>()
				{
					new Occupation() {Id = 1, Text = "Teacher"},
					new Occupation() {Id = 2, Text = "Information"}
				};

			_selectList = new SelectList(occupations, "Id", "Text");
			
			var person = new Person()
			{
				Name = "Chorn Sokun",
				Id = 0,
				OccupationId = 1,
				Occupations =  occupations,
				Gender = new Gender() { Text = "Male" }
			};

			var viewData = new ViewDataDictionary<Person>()
			{
				Model = person
			};

			_helper = MvcHelper.GetHtmlHelper(viewData);
		}

		[Fact]
		public void DropDownListRowFor_should_generate_inside_control_group()
		{
			// act
			var html = _helper.DropDownListRowFor(t => t.OccupationId, _selectList, null).ToHtmlString();

			// assert
			Assert.Contains(@"<option selected=""selected"" value=""1"">Teacher</option>", html);
		}

		[Fact]
		public void DropDownListRowFor_should_generate_validation_message_for_if_needed()
		{
			_helper.ViewContext.ClientValidationEnabled = true;
			_helper.ViewContext.UnobtrusiveJavaScriptEnabled = true;

			// act
			var html = _helper.DropDownListRowFor(t => t.OccupationId, _selectList,null).ToHtmlString();

			// assert
			Assert.Contains(@"field-validation-valid", html);
		}

		[Fact]
		public void DropDownListRowFor_should_be_able_to_generate_hints()
		{
			// act
			var html = _helper.DropDownListRowFor(t => t.OccupationId, _selectList, new { hints = "This is first name." }).ToHtmlString();

			// assert
			Assert.Contains("<span class=\"help-block\">This is first name.</span>", html);
		}

		[Fact]
		public void DropDownListRowFor_should_render_prefer_label()
		{
			// act
			var html = _helper.DropDownListRowFor(t => t.OccupationId, _selectList, new {@gridcol = 3, label = "Testing" }).ToHtmlString();

			// assert
			Assert.Contains(">Testing</label>", html);
		}
	}
}
