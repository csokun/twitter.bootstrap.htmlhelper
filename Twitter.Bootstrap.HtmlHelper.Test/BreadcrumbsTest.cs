using System.Collections.Generic;
using System.Web.Mvc;
using Twitter.Bootstrap.HtmlHelpers.ViewModels;
using Xunit;

namespace Twitter.Bootstrap.HtmlHelpers.Test
{
	public class BreadcrumbsTest
	{
		private HtmlHelper helper;

		public BreadcrumbsTest()
		{
			helper = MvcHelper.GetHtmlHelper();
		}

		[Fact]
		public void Should_generate_path_casecade()
		{
			// arrange
			var menu = new List<TbMenuItem>()
				{
					new TbMenuItem() { Text = "Home" },
					new TbMenuItem() { Text = "About" }
				};

			// act
			var html = helper.Breadcrumbs(menu).ToHtmlString();

			// assert
			Assert.Contains(@"class=""breadcrumb""", html);
			Assert.Contains("<li class=\"active\">About</li>", html);
		}
	}
}
