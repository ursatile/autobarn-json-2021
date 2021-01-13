using Autobarn.Data.Entities;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Autobarn.Data {
	public class InMemoryCarDatabase : ICarDatabase {

		public IList<Car> Cars => cars;
		public IList<Make> Makes => makes;
		public IList<CarModel> Models => models;

		private readonly List<Make> makes = new List<Make>();
		private readonly List<CarModel> models = new List<CarModel>();
		private readonly List<Car> cars = new List<Car>();

		public InMemoryCarDatabase(string dataFilePath) {
			foreach (var filePath in Directory.GetFiles(dataFilePath, "*.json")) {
				var json = File.ReadAllText(filePath);
				var make = JsonConvert.DeserializeObject<Make>(json);
				make.Code = Path.GetFileNameWithoutExtension(filePath);
				makes.Add(make);
			}

			makes.ForEach(make => make.Models.ForEach(model => model.Make = make));
			foreach (var carModel in makes.SelectMany(m => m.Models)) {
				carModel.Cars.ForEach(car => car.CarModel = carModel);
			}

			models = makes.SelectMany(make => make.Models).ToList();
			cars = models.SelectMany(model => model.Cars).ToList();
		}
	}
}