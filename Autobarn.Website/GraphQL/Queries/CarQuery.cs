using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Autobarn.Data;
using Autobarn.Data.Entities;
using GraphQL.Types;

namespace Autobarn.Website.GraphQL.Queries {
	public class CarQuery : ObjectGraphType {
		public CarQuery(ICarDatabase db) {
			Field<ListGraphType<CarGraphType>>("cars", "Query to retrieve all cars", resolve: context => db.Cars);
		}
	}

	public sealed class CarGraphType : ObjectGraphType<Car> {
		public CarGraphType() {
			Name = "car";
			Field(c => c.Registration, false);
			Field(c => c.Color, false);
			Field(c => c.Year, false);
		}
	}

	public class AutobarnSchema : Schema {
		public AutobarnSchema(ICarDatabase db) => Query = new CarQuery(db);
	}
}
