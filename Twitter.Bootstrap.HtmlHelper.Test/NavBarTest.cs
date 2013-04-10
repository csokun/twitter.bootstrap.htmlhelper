using System.Collections.Generic;
using System.Web.Mvc;
using Twitter.Bootstrap.HtmlHelpers.ViewModels;
using Xunit;

namespace Twitter.Bootstrap.HtmlHelpers.Test
{
	public class NavBarTest
	{
		private HtmlHelper helper;

		public NavBarTest()
		{
			helper = MvcHelper.GetHtmlHelper();
		}

		[Fact]
		public void NavBar_should_generate_valid_dom()
		{
			// arrange
			var navBar = new NavBar()
				{
					Brand = "Advance Payment",
					Inverse = true,
					Collapsible = true
				};
			var collapsible = @"
						<button type=""button"" class=""btn btn-navbar"" data-toggle=""collapse"" data-target="".nav-collapse"">
							<span class=""icon-bar""></span>
							<span class=""icon-bar""></span>
							<span class=""icon-bar""></span>
						</button>";
			// act
			var html = helper.NavBar(navBar, null).ToHtmlString();

			// assert
			Assert.Contains("<div class=\"navbar navbar-inverse\">", html);
			Assert.Contains("<div class=\"navbar-inner\">", html);
			Assert.Contains(string.Format("<a class=\"brand\" href=\"#\">{0}</a>", navBar.Brand), html);
			Assert.Contains(collapsible, html);
		}

		[Fact]
		public void NavBar_should_generate_menu_items()
		{
			// arrange
			var navBar = new NavBar()
				{
					Items = new List<TbMenuTree>()
						{
							new TbMenuTree() { Text =  "Home" },
							null,
							new TbMenuTree() { Text = "About", Selected = true }
						}
				};

			// act
			var html = helper.NavBar(navBar, null).ToHtmlString();

			// assert
			Assert.Contains("<li class=\"divider\"></li>", html);
			Assert.Contains("<li class=\"active\"><a href=", html);
			Assert.DoesNotContain("<ul class=\"dropdown-menu\"></ul>", html);
		}

		[Fact]
		public void NavBar_should_not_generate_empty_child_menu()
		{
			// arrange
			var navBar = new NavBar()
			{
				Items = new List<TbMenuTree>()
						{
							new TbMenuTree() {Text = "Home"},
							null,
							new TbMenuTree()
								{
									Text = "About", 
									Action = "About", 
									Selected = true,
									Items = new List<TbMenuTree>()
								}
						}
			};

			// act
			var html = helper.NavBar(navBar, null).ToHtmlString();

			// assert
			Assert.DoesNotContain("<ul class=\"dropdown-menu\"></ul>", html);
		}

		[Fact]
		public void NavBar_should_generate_sub_menu()
		{
			// arrange
			var navBar = new NavBar()
			{
				Items = new List<TbMenuTree>()
						{
							new TbMenuTree() {Text = "Home"},
							null,
							new TbMenuTree()
								{
									Text = "About", 
									Action = "About", 
									Selected = true,
									Items = new List<TbMenuTree>()
										{
											new TbMenuTree() { Text = "About 1" },
											new TbMenuTree() { Text = "About 2" },
											new TbMenuTree() { Text = "About 3" }
										}
								}
						}
			};

			// act
			var html = helper.NavBar(navBar, null).ToHtmlString();

			// assert
			Assert.Contains("<ul class=\"dropdown-menu\"><li>", html);
		}

		[Fact]
		public void NavBar_support_fix_style()
		{
			// arrange
			var navBar = new NavBar() {Fixed = NavBarDock.Top};

			// act
			// act
			var html = helper.NavBar(navBar, null).ToHtmlString();

			// assert
			Assert.Contains("navbar navbar-fixed-top", html);
		}

		[Fact]
		public void NavBar_support_inverse_fixed_styling()
		{
			// arrange
			var navBar = new NavBar() { Fixed = NavBarDock.Top, Inverse = true};

			// act
			// act
			var html = helper.NavBar(navBar, null).ToHtmlString();

			// assert
			Assert.Contains("navbar navbar-inverse navbar-fixed-top", html);
		}

		[Fact]
		public void NavBar_should_not_render_invisible_TbMenuItem()
		{
			// arrange
			var navBar = new NavBar()
			{
				Items = new List<TbMenuTree>()
						{
							new TbMenuTree() {Text = "Home", Visible = false}
						}
			};

			// act
			var html = helper.NavBar(navBar, null).ToHtmlString();

			// assert
			Assert.DoesNotContain("Home", html);
		}
	}
}
