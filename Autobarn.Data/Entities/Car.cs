using System;
using System.Collections.Generic;

namespace Autobarn.Data.Entities {
	public class Car {
		public string Registration { get; set; }
		public int Year { get; set; }
		public string Color { get; set; }
		public CarModel CarModel { get; set; }
		public List<Comment> Comments { get; set; } = new List<Comment>();
	}

	public class Comment {
		public DateTimeOffset PostedAt { get; set; }
		public string User { get; set; }
		public string Text { get; set; }
		public List<Comment> Replies { get; set; } = new List<Comment>();
	}
}