using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Html;

namespace Twitter.Bootstrap.HtmlHelpers
{
	public static class NavBarExtensions
	{
		public static IHtmlString Nav(this HtmlHelper html, NavBar navBar)
		{
			// detect current url

			var collapsible = @"
						<button type=""button"" class=""btn btn-navbar"" data-toggle=""collapse"" data-target="".nav-collapse"">
							<span class=""icon-bar""></span>
							<span class=""icon-bar""></span>
							<span class=""icon-bar""></span>
						</button>";
			
			var placeholder = new TagBuilder("div");
			placeholder.AddCssClass("navbar");
			
			if(navBar.Inverse)
				placeholder.AddCssClass("navbar-inverse");

			var container = new TagBuilder("div");
			container.AddCssClass("container");

			if (!string.IsNullOrEmpty(navBar.Brand))
			{
				var brand = new TagBuilder("a");
				brand.AddCssClass("brand");
				brand.SetInnerText(navBar.Brand);

				container.InnerHtml = brand.ToString(TagRenderMode.EndTag);
			}

			var innerNavBar = new TagBuilder("div");
			innerNavBar.AddCssClass("navbar-inner");

			innerNavBar.InnerHtml = container.ToString(TagRenderMode.EndTag);
			placeholder.InnerHtml = innerNavBar.ToString(TagRenderMode.EndTag);

			//<div class="navbar navbar-inverse navbar-fixed-top">
			//	<div class="navbar-inner">
			//		<div class="container">

			//			<a class="brand" href="#">Advance Payment</a>

			//			<div class="nav-collapse collapse">
			//				<ul class="nav">
			//					<li class="active"><a href="#">Home</a></li>
			//					<li><a href="#about">About</a></li>
			//					<li><a href="#contact">Contact</a></li>
			//				</ul>
			//			</div>

			//		</div>
			//	</div>
			//</div>
			
			return MvcHtmlString.Create(placeholder.ToString(TagRenderMode.EndTag));
		}
	}
}
