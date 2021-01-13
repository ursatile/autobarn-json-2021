using GraphQL;
using GraphQL.Instrumentation;
using GraphQL.Types;
using GraphQL.SystemTextJson;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Autobarn.Website.GraphQL {

	public class GraphQLRequest {
		[JsonConverter(typeof(ObjectDictionaryConverter))]
		public Dictionary<string, object> Variables { get; set; }
		public string OperationName { get; set; }
		public string Query { get; set; }
	}

	public class GraphQLSettings {
		public PathString GraphQLPath { get; set; }
		public Func<HttpContext, IDictionary<string, object>> BuildUserContext { get; set; }
		public bool EnableMetrics { get; set; }
		public bool ExposeExceptions { get; set; }
	}

	public class GraphQLMiddleware {
		private readonly RequestDelegate next;
		private readonly GraphQLSettings settings;
		private readonly IDocumentExecuter executer;
		private readonly IDocumentWriter writer;
		private readonly JsonSerializerOptions jsonOptions = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };

		public GraphQLMiddleware(RequestDelegate next, IOptions<GraphQLSettings> options, IDocumentExecuter executer, IDocumentWriter writer) {
			this.next = next;
			settings = options.Value;
			this.executer = executer;
			this.writer = writer;
		}

		public async Task Invoke(HttpContext context, ISchema schema) {
			if (!IsGraphQLRequest(context)) {
				await next(context);
				return;
			}
			await ExecuteAsync(context, schema);
		}

		private bool IsGraphQLRequest(HttpContext context) {
			return context.Request.Method == "POST" && context.Request.Path.StartsWithSegments(settings.GraphQLPath);
		}

		private async Task ExecuteAsync(HttpContext context, ISchema schema) {
			var request = await JsonSerializer.DeserializeAsync<GraphQLRequest>(context.Request.Body, jsonOptions);
			var result = await executer.ExecuteAsync(options => {
				options.Schema = schema;
				options.Query = request.Query;
				options.OperationName = request.OperationName;
				options.Inputs = request.Variables.ToInputs();
				options.UserContext = settings.BuildUserContext?.Invoke(context);
				options.EnableMetrics = settings.EnableMetrics;
				options.RequestServices = context.RequestServices;
			});
			await WriteResponseAsync(context, result);
		}

		private async Task WriteResponseAsync(HttpContext context, ExecutionResult result) {
			context.Response.ContentType = "application/json";
			context.Response.StatusCode = 200;
			await writer.WriteAsync(context.Response.Body, result);
		}
	}
}
