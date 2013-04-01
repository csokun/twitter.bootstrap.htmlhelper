using System.Text;
using System.Web;
using System.Web.Mvc;

namespace Twitter.Bootstrap.HtmlHelpers
{
	public static class TbPagingExtensions
	{
		/// <summary>
		/// Page size options
		/// </summary>
		private static readonly int[] PageSizes = new int[] { 10, 20, 30, 50, 100, 200 };

		/// <summary>
		/// Number of visiable page
		/// </summary>
		private static readonly int VisiblePages = 6;

		public static IHtmlString TbPaging(this HtmlHelper html, int pageCount, int currentPage)
		{
			// 1. should support both GET & POST
			// 2. value should be store in hidden field (required JavaScript ???)
			var httpRequest = HttpContext.Current.Request;
			var defaultUrl = "";

			var wrap = new TagBuilder("div");
			wrap.AddCssClass("pagination");

			var ul = new TagBuilder("ul");
			
			// generate prev
			ul.InnerHtml += (currentPage <= 1)
				                ? @"<li class=""disabled""><span>&laquo;</span</li>"
				                : string.Format(@"<li><a href=""{0}&PageIndex={1}"">&laquo;</a></li>", defaultUrl, currentPage - 1);

			// generate page sprite
			if (pageCount < VisiblePages)
			{
				for (var i = 0; i < pageCount; i++)
				{
					if (i == currentPage)
					{
						ul.InnerHtml += string.Format(@"<li class=""active""><span>{0}</span></li>", currentPage);
						continue;
					}

					ul.InnerHtml += string.Format(@"<li class=""active""><span>{0}</span></li>", currentPage); ;
				}
			}
			else
			{
				// split ...
				// <li class="disabled"><span>&laquo;</span></li>
			}

			// generate next
			ul.InnerHtml += currentPage >= pageCount
				                ? @"<li class=""disable""><span>&raquo;</span></li>"
				                : string.Format(@"<li><a href=""{0}&PageIndex={1}"">&raquo;</a></li>", defaultUrl, currentPage + 1);

			wrap.InnerHtml = ul.ToString();
			return MvcHtmlString.Create(wrap.ToString());
		}

		public static IHtmlString TbPaging(this HtmlHelper html, string url, int pageCount)
		{
			// current page can be detect from url
			return MvcHtmlString.Create(string.Empty);
		}

		//public static IHtmlString TbPaging(this HtmlHelper html, object model, int pageCount)
		//{
		//	return MvcHtmlString.Create(string.Empty);
		//}

		#region Helpers

		private static string RebuildQueryString()
		{
			var httpRequest = HttpContext.Current.Request;
			var url = httpRequest.Path;

			var queryString = httpRequest.QueryString;
			if (queryString.Count == 0) queryString = httpRequest.Form;

			var queryStringBuilder = new StringBuilder();

			for (var i = 0; i < queryString.Keys.Count; i++)
			{
				var key = queryString.Keys[i];
				var value = queryString[i];

				if ("pageindex" == key.ToLower() || "pagesize" == key.ToLower()) continue;

				queryStringBuilder.AppendFormat("{0}={1}&", key, value);
			}

			var queryStringLength = queryStringBuilder.Length;

			if (queryStringLength > 0)
				url += "?" + queryStringBuilder.ToString(0, queryStringLength - 1);

			return url;
		}

		/// <summary>
		/// Get ToPage example 3 4 5 6 7 => ToPage = 7
		/// </summary>
		/// <param name="pageCount"></param>
		/// <param name="numberDisplayPage">if 5 means 3 4 5 6 7</param>
		/// <param name="pageIndex"></param>
		/// <returns></returns>
		private static long GetFromPage(int pageCount, int numberDisplayPage, int pageIndex)
		{
			var fromPage = 1L;
			if (pageCount < numberDisplayPage)
				return fromPage;

			if (pageIndex > (numberDisplayPage/2))
				fromPage = pageIndex - (numberDisplayPage/2);

			if ((fromPage + numberDisplayPage - 1) > pageCount)
				fromPage = pageCount - (numberDisplayPage - 1);
			return fromPage;
		}

		/// <summary>
		/// get From page example 3 4 5 6 7 => FromPage = 3
		/// </summary>
		/// <param name="totalPage"></param>
		/// <param name="numberDisplayPage"></param>
		/// <param name="pageIndex"></param>
		/// <returns></returns>
		private static long GetToPage(int totalPage, int numberDisplayPage, int pageIndex)
		{
			var toPage = GetFromPage(totalPage, numberDisplayPage, pageIndex) + numberDisplayPage - 1;
			if (toPage > totalPage)
				toPage = totalPage;
			return toPage;
		}

		#endregion

	}
}
