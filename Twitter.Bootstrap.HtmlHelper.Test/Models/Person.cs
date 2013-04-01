using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Twitter.Bootstrap.HtmlHelpers.Test.Models
{
	public class Person
	{
		[Required]
		public long Id { get; set; }

		[Display(Name = "First Name")]
		public string Firstname { get; set; }

		public DateTime Birthdate { get; set; }

		public string Name { get; set; }

		public Gender Gender { get; set; }

		public int OccupationId { get; set; }

		public string Password { get; set; }

		public bool IsActive { get; set; }

		public IList<Occupation> Occupations { get; set; }
	}
}
