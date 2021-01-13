using Autobarn.Data;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using Microsoft.Extensions.Logging;

namespace Autobarn.Website.Controllers {
	public class CarsController : Controller {
		private readonly ICarDatabase database;
		private readonly ILogger<CarsController> logger;

		public CarsController(ICarDatabase database, ILogger<CarsController> logger) {
			this.database = database;
			this.logger = logger;
		}

		public IActionResult Index() {
			var cars = database.Cars;
			return View(cars);
		}

		public IActionResult Models() {
			var makes = database.Makes;
			return View(makes);
		}

		public IActionResult Model(string id) {
			var carModel = database.Models.FirstOrDefault(m => m.Code == id);
			if (carModel == null) return NotFound("Sorry - we don't recognise that model code!");
			return View(carModel);
		}
	}
}
