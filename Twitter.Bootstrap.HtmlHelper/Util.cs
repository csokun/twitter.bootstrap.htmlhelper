using System;
using System.Collections.Generic;
using System.Web.Routing;

namespace Twitter.Bootstrap.HtmlHelpers
{
	internal static class Util
	{
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

		internal static T Get<T>(this IDictionary<string, object> attributes, string key, T defaultValue) where T : struct
		{
			if (!attributes.ContainsKey(key)) return defaultValue;

			if (attributes[key].GetType() != typeof(T))
			{
				throw new InvalidCastException("Expected type of " + typeof(T) + " but " + attributes[key].GetType() + " was given.");
			}
			
			return (T) attributes[key];
		}
	}
}
