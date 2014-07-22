using System;
using Xunit;
using System.Text.RegularExpressions;

namespace Twitter.Bootstrap.HtmlHelpers.Test
{
	public class TbPagingExtensionsTest
	{
		// Empty
		[Fact]
		public void When_pageCount_equal_one_should_not_render_anything()
		{
			// arrange
			var helper = MvcHelper.GetHtmlHelper();
			var pageCount = 1;
			var currentPage = 1;

			// act
			var html = helper.TbPaging(pageCount, currentPage).ToHtmlString();

			// assert
			Assert.Equal(string.Empty, html);
		}

		[Fact]
		public void When_last_pageIndex_selected_should_not_generate_link()
		{
			// arrange
			var helper = MvcHelper.GetHtmlHelper();
			var pageCount = 3;
			var currentPage = 3;

			// act
			var html = helper.TbPaging(pageCount, currentPage).ToHtmlString();

			// assert
			Assert.Contains("<span>3</span>", html);
		}

		[Fact]
		public void When_pageIndex_less_than_zero_make_default_one()
		{
			// arrange
			var helper = MvcHelper.GetHtmlHelper();
			var pageCount = 3;
			var currentPage = 0;

			// act
			var html = helper.TbPaging(pageCount, currentPage).ToHtmlString();

			// assert
			Assert.Contains("<span>1</span>", html);
		}

		// [1] 2
		[Fact]
		public void When_pageCount_greater_than_one_should_render_1()
		{
			// arrange
			var helper = MvcHelper.GetHtmlHelper();
			var pageCount = 2;
			var currentPage = 1;

			// act
			var html = helper.TbPaging(pageCount, currentPage).ToHtmlString();

			// assert
			AssertPrevNext(currentPage, pageCount, html);
			Assert.Contains(@"<li class=""active""><span>1</span></li><li><a href=""?PageIndex=2"">2</a></li>", html);
		}


		// [1] 2
		[Fact]
		public void When_pageCount_greater_than_one_should_render_2()
		{
			// arrange
			var helper = MvcHelper.GetHtmlHelper();
			var pageCount = 2;
			var currentPage = 2;

			// act
			var html = helper.TbPaging(pageCount, currentPage).ToHtmlString();

			// assert
			AssertPrevNext(currentPage, pageCount, html);
			Assert.Contains(@"<li><a href=""?PageIndex=1"">1</a></li><li class=""active""><span>2</span></li>", html);
		}
	
		[Fact]
		public void When_pageCount_equal_visiblePage_render_all()
		{
			// arrange
			var helper = MvcHelper.GetHtmlHelper();
			var pageCount = 10;
			var currentPage = 1;

			// act
			var html = helper.TbPaging(pageCount, currentPage).ToHtmlString();

			// assert
			Assert.Contains(@"<li class=""active""><span>1</span></li>", html);
			Assert.DoesNotContain("<span>...</span>", html);
		}

		// 1 2 3 4 5 ... 8 [9] 10 ... 20 21 22 23 24 25
		[Fact]
		public void When_pageCount_larger_than_default_Visible_render_splitter()
		{
			// arrange
			var helper = MvcHelper.GetHtmlHelper();
			var pageCount = 25;
			var currentPage = 9;

			// act
			var html = helper.TbPaging(pageCount, currentPage).ToHtmlString();

			// assert
			AssertPrevNext(currentPage, pageCount, html);

			// assert center split
			Assert.Contains(@"5</a></li><li class=""disabled""><span>...</span></li><li><a href=""?PageIndex=8", html); // left
			Assert.Contains(@"<li class=""active""><span>9</span></li>", html); // current
            Assert.Equal(html.IndexOf(@"<span>1</span>", System.StringComparison.CurrentCulture), 
                html.LastIndexOf(@"<span>1</span>", System.StringComparison.CurrentCulture));
			Assert.Contains(@"10</a></li><li class=""disabled""><span>...</span></li><li><a href=""?PageIndex=20", html); // right
		}

		[Fact]
		public void When_pageCount_larger_than_default_Visible_render_splitter_2()
		{
			// arrange
			var helper = MvcHelper.GetHtmlHelper();
			var pageCount = 25;
			var currentPage = 20;

			// act
			var html = helper.TbPaging(pageCount, currentPage).ToHtmlString();

			Assert.Contains(@"<li class=""active""><span>20</span></li><li><a href=""?PageIndex=21"">21</a></li><li><a href=""?PageIndex=22", html); // current
		}

		// 1 2 3 4 5 [6] 7 8 ... 20 21 22 23 14 25
		[Fact]
		public void When_currentPage_within_three_page_range_from_head_add_to_head_and_dot_middle()
		{
			// arrange
			var helper = MvcHelper.GetHtmlHelper();
			var pageCount = 25;
			var currentPage = 6;

			// act
			var html = helper.TbPaging(pageCount, currentPage).ToHtmlString();

			// assert
			AssertPrevNext(currentPage, pageCount, html);

			// assert center split
			Assert.Contains(@"<li class=""active""><span>6</span></li>", html); // current
			
			// matching blank dots ...
			var found = Regex.Matches(html, @"\.{3}").Count;
			Assert.Equal(1, found);
		}

		private static void AssertPrevNext(int currentPage, int pageCount, string html)
		{
			var prev = currentPage - 1;
			var next = currentPage + 1;

			Assert.Contains(
				prev >= 1
					? string.Format(@"<li><a href=""?PageIndex={0}"">&laquo;</a></li>", prev)
					: @"<li class=""disabled""><span>&laquo;</span></li>", html);

			Assert.Contains(next > currentPage && next <= pageCount
				?string.Format(@"<li><a href=""?PageIndex={0}"">&raquo;</a></li>", next)
				: @"<li class=""disabled""><span>&raquo;</span></li>", html); // Next
		}

		// 1 2 3 ... 16 [17] 18 ... 23 14 25
		[Fact]
		public void When_currentPage_within_three_page_range_from_tail_add_to_append_and_dot_middle()
		{
			// arrange
			var helper = MvcHelper.GetHtmlHelper();
			var pageCount = 25;
			var currentPage = 1;

			// act
			var html = helper.TbPaging(pageCount, currentPage).ToHtmlString();

			// assert
			AssertPrevNext(currentPage, pageCount, html);

			// assert center split
			//Assert.Contains(@"<li><a href=""?PageIndex=16"">16</a></li><li class=""active""><span>17</span></li><li><a href=""?PageIndex=18"">18</a></li>", html); // current
			
			var found = Regex.Matches(html, @"\.{3}").Count;
			Assert.Equal(2, found);
		}

		[Fact]
		public void When_current_page_is_zero_middle_segment_incorrectly_render()
		{
			// arrange
			var helper = MvcHelper.GetHtmlHelper();
			var pageCount = 25;
			var currentPage = 1;

			// act
			var html = helper.TbPaging(pageCount, currentPage).ToHtmlString();

			// assert
			AssertPrevNext(currentPage, pageCount, html);

			//
            Assert.Equal(html.IndexOf(@"<span>1</span>", StringComparison.CurrentCulture),
                html.LastIndexOf(@"<span>1</span>", StringComparison.CurrentCulture));
			
		}
	}
}
