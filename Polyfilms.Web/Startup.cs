using System.Collections.Generic;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using AutoMapper;
using PolyFilms.Data.Repositories;
using PolyFilms.Services.Login;
using PolyFilms.Data.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Http;
using PolyFilms.Web;
using Newtonsoft.Json.Serialization;
using PolyFilms.Services.User;
using PolyFilms.Services.Roles;
using PolyFilms.Services.RoleMenuMap;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Localization;
using PolyFilms.Services.Audit;
using PolyFilms.Services.Customer;
using PolyFilms.Services.Product;
using PolyFilms.Services.Jumbo;
using PolyFilms.Services.Order;
using PolyFilms.Services.OrderDetail;
using PolyFilms.Services.Slitting;
using PolyFilms.Services.Dispatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Cors.Internal;
using PolyFilms.Data.CustomModel;
using PolyFilms.Services.Invoice;
using PolyFilms.Services.Consignee;
using System.Globalization;
using System.IO.Compression;
using System.Linq;
using Microsoft.AspNetCore.ResponseCompression;
using PolyFilms.Services.Core;
using PolyFilms.Common;


namespace Polyfilms.Web
{
    public class Startup
    {
        private MapperConfiguration _mapperConfiguration { get; }

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
            _mapperConfiguration = new MapperConfiguration(cfg => { cfg.AddProfile(new AutoMapperConfig()); });
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme).AddCookie(options => { options.LoginPath = "/Login/Login"; });
            services.AddDbContext<PolyFilmsContext>(options => options.UseSqlServer(Configuration.GetConnectionString("PolyFilmsDatabase")));

            CommonHelper.ConnectionString = Configuration.GetConnectionString("PolyFilmsDatabase");

            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddSession();

            services.AddCors(o => o.AddPolicy("TelerikReportingDemoPolicy", builder =>
            {
                builder.AllowAnyOrigin()
                .AllowAnyMethod()
                .AllowAnyHeader()
                .AllowCredentials();
            }));


            services.Configure<RequestLocalizationOptions>(options =>
            {
                options.DefaultRequestCulture = new RequestCulture("en-IN");
                options.SupportedCultures = new List<CultureInfo>()
                {
                    new CultureInfo("en-IN"),
                };
            });

            services.AddMvc().AddJsonOptions(x => { x.SerializerSettings.ContractResolver = new DefaultContractResolver(); });
            //services.AddAutoMapper();
            services.Configure<MvcOptions>(options => {
                options.Filters.Add(new CorsAuthorizationFilterFactory("TelerikReportingDemoPolicy"));
            });

            services.AddSingleton(sp => _mapperConfiguration.CreateMapper());

            services.AddKendo();

            services.AddResponseCompression(options =>
            {
                options.Providers.Add<GzipCompressionProvider>();
                options.MimeTypes = ResponseCompressionDefaults.MimeTypes.Concat(new[] { "image/svg+xml" });
            });

            services.Configure<GzipCompressionProviderOptions>(options =>
            {
                options.Level = CompressionLevel.Fastest;
            });

            RegisterServices(services);
        }

        private void RegisterServices(IServiceCollection services)
        {
            services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
            services.AddTransient<ILoginService, LoginService>();
            services.AddTransient<IUserService, UserService>();
            services.AddTransient<IRolesService, RolesService>();
            services.AddTransient<IRoleMenuMapService, RoleMenuMapService>();
            services.AddTransient<ICustomerService, CustomerService>();
            services.AddTransient<IProductService, ProductService>();
            services.AddTransient<IJumboService, JumboService>();
            services.AddTransient<IOrderService, OrderService>();
            services.AddTransient<IOrderDetailService, OrderDetailService>();
            services.AddTransient<ISlittingService, SlittingService>();
            services.AddTransient<IDispatchService, DispatchService>();
            services.AddTransient<IAuditService, AuditService>();
            services.AddTransient<IInvoiceService, InvoiceService>();
            services.AddTransient<IConsigneeService, ConsigneeService>();
            services.AddTransient<ICoreService, CoreService>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            app.UseCors("TelerikReportingDemoPolicy");

            app.UseRequestLocalization();

            app.UseAuthentication();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseBrowserLink();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }

            app.UseKendo(env);

            app.UseStaticFiles();

            app.UseSession();

            HttpHelper.Configure(app.ApplicationServices.GetRequiredService<IHttpContextAccessor>());

            SettingConfig.InitilizeSettings();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
