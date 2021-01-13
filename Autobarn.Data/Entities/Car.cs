namespace Autobarn.Data.Entities {
	public class Car {
		public string Registration { get; set; }
		public int Year { get; set; }
		public string Color { get; set; }
		public CarModel CarModel { get; set; }
	}
}