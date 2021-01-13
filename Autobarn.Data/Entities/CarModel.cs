using System.Collections.Generic;

namespace Autobarn.Data.Entities {
	public class CarModel {
		public Make Make { get; set; }
		public string Code { get; set; }
		public string Name { get; set; }
		public List<Car> Cars { get; set; } = new List<Car>();
		public override string ToString() => $"{Make.Name} {this.Name}";
	}
}

