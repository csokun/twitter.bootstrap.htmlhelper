using Xunit;

namespace Twitter.Bootstrap.HtmlHelpers.Test
{
	public class TbbPagingExtensionsTest
	{
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
		public void When_pageCount_greater_than_one_should_render()
		{
			// arrange
			var helper = MvcHelper.GetHtmlHelper();
			var pageCount = 2;
			var currentPage = 1;

			// act
			var html = helper.TbPaging(pageCount, currentPage).ToHtmlString();

			// assert
			Assert.Contains(@"<li class=""active""><span>1</span></li><li><a href=""/app/?PageIndex=2"">2</a></li>", html);
		}

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
			Assert.Contains(@"<li><a href=""/app/&PageIndex=""></li><li class=""disabled""><span>...</span></li><li></li>", html);
		}
	}
}
