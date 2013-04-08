using System;
using System.Globalization;
using System.Web.Mvc;

namespace Twitter.Bootstrap.HtmlHelpers.MvcBinder
{
	public class DatepickerModelBinder : IModelBinder
	{
		public object BindModel(ControllerContext controllerContext, ModelBindingContext bindingContext)
		{
			if (bindingContext == null)
			{
				throw new ArgumentNullException("bindingContext");
			}
			
			var pattern = bindingContext.ValueProvider.GetValue(bindingContext.ModelName + ".DateFormat");
			var value = bindingContext.ValueProvider.GetValue(bindingContext.ModelName);

			try
			{
				return DateTime.ParseExact(value.AttemptedValue, pattern.AttemptedValue, null, DateTimeStyles.None);
			}
			catch
			{
				return null;
			}
		}

	}
}
