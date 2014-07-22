using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq.Expressions;
using System.Text.RegularExpressions;
using System.Web.Mvc;
using System.Web.Mvc.Html;

namespace Twitter.Bootstrap.HtmlHelpers
{
	internal static class Util
	{
		/// <summary>
		/// 
		/// </summary>
		/// <param name="attributes"></param>
		/// <param name="key"></param>
		/// <param name="haystack"></param>
		internal static void Ensure( this IDictionary<string, object> attributes, string key, string haystack)
		{
			var hasKey = attributes.ContainsKey(key);

			if (hasKey && !attributes[key].ToString().Contains(haystack))
			{
				attributes[key] += " " + haystack;
			}
			else
			{
				attributes.Add(key, haystack);
			}
		}

		/// <summary>
		/// Get T value from a known key 
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="attributes"></param>
		/// <param name="key"></param>
		/// <param name="defaultValue"></param>
		/// <returns></returns>
		internal static T Get<T>(this IDictionary<string, object> attributes, string key, T defaultValue) where T : struct
		{
			if (!attributes.ContainsKey(key)) return defaultValue;

			if (attributes[key].GetType() != typeof(T))
			{
				throw new InvalidCastException("Expected type of " + typeof(T) + " but " + attributes[key].GetType() + " was given.");
			}
			
			return (T) attributes[key];
		}

		/// <summary>
		/// Get string with default value
		/// </summary>
		/// <param name="attributes"></param>
		/// <param name="key"></param>
		/// <param name="defaultValue"></param>
		/// <returns></returns>
		internal static string GetString(this IDictionary<string, object> attributes, string key, string defaultValue)
		{
			return !attributes.ContainsKey(key) ? defaultValue : attributes[key].ToString();
		}

		/// <summary>
		/// 
		/// </summary>
		/// <typeparam name="TModel"></typeparam>
		/// <typeparam name="TProperty"></typeparam>
		/// <param name="html"></param>
		/// <param name="expression"></param>
		/// <param name="attributes"></param>
		/// <param name="inline"></param>
		/// <returns></returns>
		internal static string WriteLabelFor<TModel, TProperty>(this HtmlHelper<TModel> html, 
			Expression<Func<TModel, TProperty>> expression, 
			IDictionary<string, object> attributes,
			bool inline)
		{
			var label = attributes.GetString("label", string.Empty);

			var lbl = html.LabelFor(expression, new
			{
				@class = inline ? "sr-only" : "control-label col-lg-" + attributes.Get<int>("labelcols", 2)
			}).ToHtmlString();
			attributes.Remove("labelcols");

			if (label != string.Empty)
			{
				attributes.Remove("label");

				var regEx = new Regex("(<label.+?>)(.+?)</label>");
				var match = regEx.Match(lbl);

				lbl = string.Format("{0}{1}</label>", match.Groups[1], html.Encode(label));
			}
			return lbl;
		}

		internal static object ToExpando(this IDictionary<string, object> dictionary)
		{
			var eo = new ExpandoObject();
			var eoColl = (ICollection<KeyValuePair<string, object>>)eo;
			
			foreach (var kvp in dictionary)
			{
				eoColl.Add(kvp);
			}
		
			return eo;
		}
	}
}
