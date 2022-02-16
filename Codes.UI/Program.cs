using System;
using System.Threading.Tasks;
using Codes.ApiClient;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Codes.UI
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebAssemblyHostBuilder.CreateDefault(args);
            builder.RootComponents.Add<App>("#app");

            builder.Services.AddHttpClient<ICodesApiClient, CodesApiClient>((client 
                => client.BaseAddress = new Uri(builder.Configuration.GetValue<string>("ApiUrl"))));
            
            builder.Services.AddAntDesign();
            
            await builder.Build().RunAsync();
        }
    }
}