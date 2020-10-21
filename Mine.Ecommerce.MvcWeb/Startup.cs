using Grpc.Net.Client;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Mine.Ecommerce.MvcWeb.Services;
using Mine.Ecommerce.Web.Data;
using System;
using System.Net.Http;
using Grpc.Net.Client.Web;

namespace Mine.Ecommerce.Web
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            var identityInternal = Configuration.GetSection("Identity:Url")?.Value;
            var clientName = Configuration.GetSection("Identity:ClientName")?.Value;
            var clientSecret = Configuration.GetSection("Identity:ClientSecret")?.Value;

            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(
                    Configuration.GetConnectionString("DefaultConnection")));
            services.AddDefaultIdentity<IdentityUser>()
                .AddEntityFrameworkStores<ApplicationDbContext>();

            /*services.AddAuthentication(options =>
            {
                options.DefaultScheme = "Cookies";
                options.DefaultChallengeScheme = "oidc";
            })
            .AddCookie("Cookies")
            .AddOpenIdConnect("oidc", options =>
            {
                options.Authority = $"{identityInternal}";
                options.ClientId = clientName;
                options.ClientSecret = clientSecret;
                options.ResponseType = "code";
                options.SaveTokens = true;
                options.RequireHttpsMetadata = false;
                options.GetClaimsFromUserInfoEndpoint = true;
            });*/

            services.AddRazorPages();

            AppContext.SetSwitch("System.Net.Http.SocketsHttpHandler.Http2UnencryptedSupport", true);
            var channel = GrpcChannel.ForAddress(Configuration.GetSection("ProductService:Url")?.Value,
                 new GrpcChannelOptions
                 {
                     HttpHandler = new GrpcWebHandler(new HttpClientHandler())
                 });

            services.AddScoped<IProductService, ProductService>(
                (service) => new ProductService(new Commerce.V1.Products.ProductsClient(channel)));
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseDatabaseErrorPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            //app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapRazorPages();
            });
        }
    }
}
