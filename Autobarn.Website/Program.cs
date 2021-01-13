using System;
using System.IO;
using System.Net;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Server.Kestrel.Core;
using Microsoft.Extensions.Hosting;

namespace Autobarn.Website {
	public class Program {
		public static void Main(string[] args) {
			CreateHostBuilder(args).Build().Run();
		}
		public static IHostBuilder CreateHostBuilder(string[] args) =>
		Host.CreateDefaultBuilder(args)
		.ConfigureWebHostDefaults(webBuilder => {
			webBuilder.ConfigureKestrel(options => {
				options.ListenAnyIP(5000, listenOptions => listenOptions.Protocols = HttpProtocols.Http1AndHttp2);
				var pfxPassword = Environment.GetEnvironmentVariable("UrsatilePfxPassword");
				options.UseSslIfFileExists(5001, @"d:\workshop.ursatile.com\certificate.pfx", pfxPassword);
			});
			webBuilder.UseStartup<Startup>();
		});
	}

	public static class KestrelServerExtensions {
		public static void UseSslIfFileExists(this KestrelServerOptions options, int port, string pfxFilePath, string password = null) {
			if (File.Exists(pfxFilePath)) {
				options.Listen(IPAddress.Any, port, listen => listen.UseHttps(pfxFilePath, password));
			}
		}
	}
}
