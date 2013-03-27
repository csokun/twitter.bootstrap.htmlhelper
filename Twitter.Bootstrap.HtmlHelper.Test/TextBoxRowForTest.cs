using System.Collections.Generic;
using System.Web.Mvc;
using System.Web.Routing;
using Twitter.Bootstrap.HtmlHelpers.Test.Models;
using Xunit;

namespace Twitter.Bootstrap.HtmlHelpers.Test
{
	public class TextBoxRowForTest
	{
		private HtmlHelper<Person> helper;

		public TextBoxRowForTest()
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
			var html = helper.TextBoxRowFor(t => t.Name).ToHtmlString();

			// Assert
			Assert.Contains(@"<div class=""control-group"">", html);
			Assert.Contains(@"<label class=""control-label"" for=""Name"">Name</label>", html);
			Assert.Contains(@"<input id=""Name"" name=""Name"" type=""text"" value=""Chorn Sokun"" />", html);
		}

		[Fact]
		public void TextBoxRowFor_should_generate_nest_property_id()
		{
			// Arrange
			var emp = new Person()
				{
					Gender = new Gender() { Text ="Male" }
				};
	
			// Act			
			var html = helper.TextBoxRowFor(t => t.Gender.Text).ToHtmlString();

			// Assert
			Assert.Contains(@"<input id=""Gender_Text"" name=""Gender.Text"" type=""text"" value=""Male"" />", html);
		}

		[Fact]
		public void TextBoxRowFor_should_generate_Unobtrusive_Validation_Attributes()
		{
			helper.ViewContext.ClientValidationEnabled = true;
			helper.ViewContext.UnobtrusiveJavaScriptEnabled = true;
	
			// Act
			var html = helper.TextBoxRowFor(t => t.Id, null).ToHtmlString();

			// Assert
			Assert.Contains("data-val-required", html);
		}

		[Fact]
		public void TextBoxRowFor_should_generate_validation_message_if_needed()
		{
			helper.ViewContext.ClientValidationEnabled = true;
			helper.ViewContext.UnobtrusiveJavaScriptEnabled = true;

			// Act
			var html = helper.TextBoxRowFor(t => t.Id, true, null).ToHtmlString();

			// Assert
			Assert.Contains("field-validation-valid", html);
		}

		[Fact]
		public void TextBoxRowFor_should_generate_label_using_annotation_setting()
		{
			// Act			
			var html = helper.TextBoxRowFor(t => t.Firstname, null).ToHtmlString();

			// Assert
			Assert.Contains(@"<label class=""control-label"" for=""Firstname"">First Name</label>", html);
		}

		[Fact]
		public void TextBoxRowFor_should_be_able_to_generate_hint()
		{
			// act
			var html = helper.TextBoxRowFor(t => t.Firstname, new { @hints = "This is first name."});

			// assert
			Assert.Contains("<span class=\"help-block\">This is first name.</span>", html.ToHtmlString());
		}

		[Fact]
		public void TextBoxRowFor_should_be_able_to_generate_icon_prepend()
		{
			// act
			var html = helper.TextBoxRowFor(t => t.Firstname, new { @prepend = "icon-envelope" }).ToHtmlString();

			// assert
			Assert.Contains("<div class=\"input-prepend\">", html);
			Assert.Contains("<span class=\"add-on\"><i class=\"icon-envelope\"></i></span>", html);
		}

		[Fact]
		public void TextBoxRowFor_should_be_able_to_gnerate_with_icon_append()
		{
			// act
			var html = helper.TextBoxRowFor(t => t.Firstname, new { @append = "icon-envelope" }).ToHtmlString();

			// assert
			Assert.Contains("<div class=\"input-append\">", html);
			Assert.Contains("<span class=\"add-on\"><i class=\"icon-envelope\"></i></span>", html);
		}
	}
}
