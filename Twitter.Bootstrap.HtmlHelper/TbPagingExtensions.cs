﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Web;
using System.Web.Mvc;
using System.Web.UI.WebControls;

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
		private const int VisiblePages = 10;

		public static IHtmlString TbPaging(this HtmlHelper html, int pageCount, int currentPage)
		{
			if (pageCount <= 1) return MvcHtmlString.Empty;
			if (currentPage <= 0) currentPage = 1;

			// 1. should support both GET & POST
			// 2. value should be store in hidden field (required JavaScript ???)
			var defaultUrl = RebuildQueryString(html);

			var ul = new TagBuilder("ul");
			ul.AddCssClass("pagination");

			// generate prev
			ul.InnerHtml += (currentPage - 1 < 1)
												? @"<li class=""disabled""><span>&laquo;</span></li>"
												: string.Format(@"<li><a href=""{0}"">&laquo;</a></li>", PageLink(defaultUrl, currentPage - 1));

			// generate page sprite
			if (pageCount <= VisiblePages)
			{
				WritePages(1, pageCount, currentPage, ul, defaultUrl);
			}
			else
			{
				// split ...
				const int segment = VisiblePages/2;
				const int radius = 3;

				var startOfPart2 = (pageCount - segment) + 1;
				var endOfPart1 = segment;

				var segment1InRadius = (currentPage >= 1 && currentPage <= segment + radius);
				var segment2InRadius = (currentPage >= startOfPart2 - radius && currentPage <= pageCount);

				// head
				if (segment1InRadius && currentPage > segment)
				{
					endOfPart1 += radius;
					startOfPart2 += radius;
				}

				if (segment2InRadius && currentPage < startOfPart2)
				{
					startOfPart2 -= radius;
					endOfPart1 -= radius;
				}

				WritePages(1, endOfPart1, currentPage, ul, defaultUrl);
	
				// body -
				if ((segment1InRadius || segment2InRadius) 
					//|| (pageCount - VisiblePages <= segment)
				)
				{
					ul.InnerHtml += @"<li class=""disabled""><span>...</span></li>";
				}
				else
				{
					var middle = currentPage;//pageCount/2;

					ul.InnerHtml += @"<li class=""disabled""><span>...</span></li>";
					WritePages(middle - 1, middle + 1, currentPage, ul, defaultUrl);
					ul.InnerHtml += @"<li class=""disabled""><span>...</span></li>";
				}

				// tail
				WritePages(startOfPart2, pageCount, currentPage, ul, defaultUrl);
			}

			// generate next
			ul.InnerHtml += currentPage + 1 > pageCount
												? @"<li class=""disabled""><span>&raquo;</span></li>"
												: string.Format(@"<li><a href=""{0}"">&raquo;</a></li>", PageLink(defaultUrl, currentPage + 1));

			return MvcHtmlString.Create(ul.ToString());
		}



		private static void WritePages(int startPage, int pageCount, int currentPage, TagBuilder ul, string defaultUrl)
		{
			for (var i = startPage; i <= pageCount; i++)
			{
				if (i == currentPage)
				{
					ul.InnerHtml += string.Format(@"<li class=""active""><span>{0}</span></li>", currentPage);
					continue;
				}

				ul.InnerHtml += string.Format(@"<li><a href=""{0}"">{1}</a></li>", PageLink(defaultUrl, i), i);
			}
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

		private static string PageLink(string url, int pageIndex)
		{
			if (url.EndsWith("?"))
			{
				return url + "PageIndex=" + pageIndex;
			}

			return url + "&PageIndex=" + pageIndex;
		}

		private static string RebuildQueryString(HtmlHelper htmlHelper)
		{
			var httpRequest = htmlHelper.ViewContext.HttpContext.Request;
			var url = httpRequest.Path ?? string.Empty;

			var queryString = httpRequest.QueryString ?? httpRequest.Form;
			if (queryString != null && queryString.Count > 0)
			{
				var queryStringBuilder = new StringBuilder();

				foreach (var query in queryString.AllKeys)
				{
					var key = query;
					var value = queryString[query];

					if ("pageindex" == key.ToLower() || "pagesize" == key.ToLower()) continue;

					queryStringBuilder.AppendFormat("{0}={1}&", key, value);
				}

				var queryStringLength = queryStringBuilder.Length;

				if (queryStringLength > 0)
					url += "?" + queryStringBuilder.ToString(0, queryStringLength - 1);
			}

			if (url.IndexOf('?') == -1) url += "?";

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

			if (pageIndex > (numberDisplayPage / 2))
				fromPage = pageIndex - (numberDisplayPage / 2);

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
