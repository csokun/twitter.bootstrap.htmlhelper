using System;
using System.Web.Mvc;
using Twitter.Bootstrap.HtmlHelpers.Test.Models;
using Xunit;

namespace Twitter.Bootstrap.HtmlHelpers.Test
{
	public class DatepickerForExtensionsTest
	{
		private HtmlHelper<Person> helper;

		public DatepickerForExtensionsTest()
		{
			var viewData = new ViewDataDictionary<Person>
				{
					Model = new Person() { Birthdate = new DateTime(2013, 3, 30) }
				};

			helper = MvcHelper.GetHtmlHelper(viewData);
		}

		[Fact]
		public void Should_throw_exception_when_modelType_not_DateTime()
		{
			// act
			Exception ex = Assert.Throws<ArgumentException>(() => helper.DatepickerRowFor(p => p.Name));

			// assert
			Assert.Contains("Input value is not DateTime.", ex.Message);
		}

		[Fact]
		public void Should_generate_proper_dom()
		{
			// arrange
			const string expect = @"<div class=""input-append date"" data-date=""30-03-2013"" data-date-format=""dd-mm-yyyy"" id=""Birthdate_datepicker""><input class=""span2"" id=""Birthdate"" name=""Birthdate"" readonly=""readonly"" size=""16"" type=""text"" value=""30-03-2013"" /><span class=""add-on""><i class=""icon-calendar""></i></span></div></div>";

			// act
			var html = helper.DatepickerRowFor(p => p.Birthdate).ToHtmlString();

			// assert
			Assert.Contains(expect, html);
		}
	}
}
