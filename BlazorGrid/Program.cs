using Fluxor;
using System;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace BlazorGrid
{
	public class Program
	{
		public static async Task Main(string[] args)
		{
			var builder = WebAssemblyHostBuilder.CreateDefault(args);
			builder.RootComponents.Add<App>("#app");

			builder.Services.AddScoped(
				sp => new HttpClient {BaseAddress = new Uri(builder.HostEnvironment.BaseAddress)});
			
			builder.Services.AddFluxor(o => o
				.ScanAssemblies(typeof(Program).Assembly));
			
			builder.Services.AddSingleton<ISignalRService, DotnetSignalRService>();

			builder.Services.AddLogging(o => o.SetMinimumLevel(LogLevel.Debug));

			await builder.Build().RunAsync();
		}
	}
}