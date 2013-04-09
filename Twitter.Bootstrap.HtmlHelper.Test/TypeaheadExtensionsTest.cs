using System.Web.Mvc;
using Twitter.Bootstrap.HtmlHelpers.Test.Models;
using Xunit;

namespace Twitter.Bootstrap.HtmlHelpers.Test
{
	public class TypeaheadExtensionsTest
	{
		private readonly HtmlHelper<Person> _helper;

		public TypeaheadExtensionsTest()
		{
			_helper = MvcHelper.GetHtmlHelper(new ViewDataDictionary<Person>(new Person()));
		}

		[Fact]
		public void Should_generate_input_with_autocomplete_off()
		{
			// arrange
			const string autocompleteOff = @"autocomplete=""off""";
			const string dataprovider = @"data-provider=""typeahead""";

			// act
			var html = _helper.TypeaheadRowFor(t => t.Firstname, null).ToHtmlString();

			// assert
			Assert.Contains(autocompleteOff, html);
			Assert.Contains(dataprovider, html);
		}

	}
}
