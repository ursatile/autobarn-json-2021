using System.Collections.Generic;

namespace Autobarn.Data.Entities {
	public class Make {
		public string Code { get; set; }
		public string Name { get; set; }
		public List<CarModel> Models { get; set; } = new List<CarModel>();
	}
}