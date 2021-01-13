using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Autobarn.Data;
using Autobarn.Data.Entities;
using GraphQL.Types;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace Autobarn.Website.GraphQL.Queries {
	public class CarQuery : ObjectGraphType {
		public CarQuery(ICarDatabase db) {
			Field<ListGraphType<CarGraphType>>("cars", "Query to retrieve all cars", resolve: context => db.Cars);
		}
	}

	public sealed class CarMakeGraphType : ObjectGraphType<Make> {
		public CarMakeGraphType() {
			Name = "make";
			Field(c => c.Name).Description("The name of the manufacturer, e.g. Tesla, Volkswagen, Ford");
			

		}
	}
	public sealed class CarModelGraphType : ObjectGraphType<CarModel> {
		public CarModelGraphType() {
			Name = "model";
			Field(m => m.Name, false).Description("The name of this model, e.g. Golf, Beetle, 5 Series, Model X");
			Field(m => m.Make, false, typeof(CarMakeGraphType)).Description("The make of this model of car");

		}
	}
	public sealed class CarGraphType : ObjectGraphType<Car> {
		public CarGraphType() {
			Name = "car";
			Field(c => c.CarModel, nullable: false, type: typeof(CarModelGraphType))
				.Description("The model of this particular car");
			Field(c => c.Registration);
			Field(c => c.Color);
			Field(c => c.Year);
		}
	}

	public class AutobarnSchema : Schema {
		public AutobarnSchema(ICarDatabase db) => Query = new CarQuery(db);
	}
}
