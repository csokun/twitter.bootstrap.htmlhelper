using System;
using System.Web.Mvc;

namespace Twitter.Bootstrap.HtmlHelpers.MvcFilters
{
	[AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, Inherited = false, AllowMultiple = true)]
	public class MenuItemKeyAttribute : ActionFilterAttribute
	{
		public override void OnActionExecuting(ActionExecutingContext filterContext)
		{
			if(filterContext.IsChildAction) return;

			base.OnActionExecuting(filterContext);

			filterContext.Controller.ViewBag.MenuItemKey = Key;
		}

		public string Key { get; set; }
	}
}