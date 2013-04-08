using System;
using System.Collections.Specialized;
using System.Web.Mvc;
using Twitter.Bootstrap.HtmlHelpers.MvcBinder;
using Twitter.Bootstrap.HtmlHelpers.Test.Models;
using Xunit;

namespace Twitter.Bootstrap.HtmlHelpers.Test
{
	public class DatepickerModelBinderTest
	{
		private ControllerContext controllerContext = new ControllerContext();
	
		[Fact]
		public void Should_be_able_to_bind_iso_format()
		{
			// Arrange
			var bindingContext = GetModelBindingContext("yyyy-MM-dd", "2013-01-12");
			var b = new DatepickerModelBinder();

			// Act
			var result = (DateTime)b.BindModel(controllerContext, bindingContext);

			// Assert
			Assert.Equal(DateTime.Parse("2013-01-12"), result);
		}

		private static ModelBindingContext GetModelBindingContext(string format, string value)
		{
			var formCollection = new NameValueCollection
				{
					{"Birthdate.DateFormat", format},
					{"Birthdate", value}
				};

			var valueProvider = new NameValueCollectionValueProvider(formCollection, null);
			var modelMetadata = ModelMetadataProviders.Current.GetMetadataForType(null, typeof (Person));

			var bindingContext = new ModelBindingContext
				{
					ModelName = "Birthdate",
					ValueProvider = valueProvider,
					ModelMetadata = modelMetadata
				};
			return bindingContext;
		}

		[Fact]
		public void Should_be_able_to_bind_ddMMyyy_format()
		{
			// Arrange
			var bindingContext = GetModelBindingContext("dd/MM/yyyy", "12/01/2013");
			var b = new DatepickerModelBinder();

			// Act
			var result = (DateTime)b.BindModel(controllerContext, bindingContext);

			// Assert
			Assert.Equal(DateTime.Parse("2013-01-12"), result);
		}
	}
}
