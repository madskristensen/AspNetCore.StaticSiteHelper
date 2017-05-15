using System.IO;
using System.Collections.Generic;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.StaticFiles;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;

namespace Static.Web
{
    public class Startup
    {
        public static void Main(string[] args)
        {
            var host = new WebHostBuilder()
                .UseKestrel()
                .UseContentRoot(Directory.GetCurrentDirectory())
                .UseIISIntegration()
                .UseStartup<Startup>()
                .Build();

            host.Run();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseBrowserLink();
            }

            LoadMimeTypesFromFile(app, env);
            app.UseFileServer();
        }

        private static void LoadMimeTypesFromFile(IApplicationBuilder app, IHostingEnvironment env)
        {
            string mimeTypeFile = Path.Combine(env.ContentRootPath, "mimetypes.json");

            if (!File.Exists(mimeTypeFile))
            {
                return;
            }

            string mimeTypes = File.ReadAllText(mimeTypeFile);
            var obj = JObject.Parse(mimeTypes);
            var map = JsonConvert.DeserializeObject<Dictionary<string, string>>(mimeTypes);
            var provider = new FileExtensionContentTypeProvider();

            foreach (string ext in map.Keys)
            {
                provider.Mappings[ext] = map[ext];
            }

            var options = new StaticFileOptions
            {
                ContentTypeProvider = provider
            };

            app.UseStaticFiles(options);
        }
    }
}
