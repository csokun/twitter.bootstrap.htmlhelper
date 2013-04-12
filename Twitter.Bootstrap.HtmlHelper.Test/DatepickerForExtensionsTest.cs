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
		public void Should_support_Nullable_DateTime()
		{
			// act
			var html = helper.DatepickerRowFor(t => t.EmployDate);

			// assert
			Assert.NotEmpty(html.ToHtmlString());
		}

		[Fact]
		public void Should_generate_necessary_minimal_dom_el()
		{
			// arrange
			const string expectWrapper = @"<div class=""input-append date"" data-date=""2013-03-30"" data-date-format=""yyyy-mm-dd"" id=""Birthdate_datepicker"">";
			const string expectInput = @"<input class=""input-small"" id=""Birthdate"" name=""Birthdate"" readonly=""readonly"" size=""16"" type=""text"" value=""2013-03-30"" />";
			const string expectIcon = @"<span class=""add-on""><i class=""icon-calendar""></i></span>";

			// act
			var html = helper.DatepickerRowFor(p => p.Birthdate).ToHtmlString();

			// assert
			Assert.Contains(expectWrapper, html);
			Assert.Contains(expectInput, html);
			Assert.Contains(expectIcon, html);
		}

		[Fact]
		public void Should_generate_hidden_format_field()
		{
			// arrange
			var expected = @"<input id=""Birthdate_DateFormat"" name=""Birthdate.DateFormat"" type=""hidden"" value=""dd-MM-yyyy"" />";

			// act
			var html = helper.DatepickerRowFor(p => p.Birthdate, new { @data_date_format = "dd-MM-yyyy"}).ToHtmlString();

			// assert
			Assert.Contains(expected, html);
		}

		[Fact]
		public void DatepickerFor_Should_generate_Datepicker_without_div_container()
		{
			// arrange
			const string expected = @"<label for=""Birthdate"">Birthdate</label><div class=""input-append date"" data-date=""2013-03-30"" data-date-format=""yyyy-mm-dd"" id=""Birthdate_datepicker""><input class=""input-small"" id=""Birthdate"" name=""Birthdate"" readonly=""readonly"" size=""16"" type=""text"" value=""2013-03-30"" /><input id=""Birthdate_DateFormat"" name=""Birthdate.DateFormat"" type=""hidden"" value=""yyyy-MM-dd"" /><span class=""add-on""><i class=""icon-calendar""></i></span></div>";

			// act
			var html = helper.DatepickerFor(p => p.Birthdate);

			// assert
			Assert.Equal(expected, html.ToHtmlString());
		}

		[Fact]
		public void DatepickerFor_Should_genernate_Datepicker_without_label_el()
		{
			// arrange
			const string expected = @"<div class=""input-append date"" data-date=""2013-03-30"" data-date-format=""yyyy-mm-dd"" id=""Birthdate_datepicker""><input class=""input-small"" id=""Birthdate"" name=""Birthdate"" readonly=""readonly"" size=""16"" type=""text"" value=""2013-03-30"" /><input id=""Birthdate_DateFormat"" name=""Birthdate.DateFormat"" type=""hidden"" value=""yyyy-MM-dd"" /><span class=""add-on""><i class=""icon-calendar""></i></span></div>";

			// act
			var html = helper.DatepickerFor(p => p.Birthdate, false);

			// assert
			Assert.Equal(expected, html.ToHtmlString());
		}
	}
}
