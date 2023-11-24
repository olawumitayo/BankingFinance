using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using TheCoreBanking.Finance.Data;
using System.IdentityModel.Tokens.Jwt;
using TheCoreBanking.Finance.Data.Contracts;
using TheCoreBanking.Finance.Data.Repository;
using System.Reflection;
using TheCoreBanking.Finance.Models;
using Microsoft.IdentityModel.Tokens;
using IdentityModel;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using System.Security.Claims;
using Newtonsoft.Json.Linq;
using TheCoreBanking.Fiance.Data.Repository;
using TheCoreBanking.Finance.Services;
using Microsoft.Extensions.FileProviders;
using System.IO;
using TheCorebanking.Finance.Culture;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication.Cookies;

namespace TheCoreBanking.Finance
{
    public class Startup
    {
        private IConfigurationRoot _configurationRoot;

        
       
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            var connectionString = Configuration.GetConnectionString("TheCoreBanking");
            var migrationsAssembly = typeof(Startup).GetTypeInfo().Assembly.GetName().Name;



            services.AddDbContext<ApplicationDbContext>(builder =>
            builder.UseSqlServer(connectionString, sqlOptions => sqlOptions.MigrationsAssembly(migrationsAssembly)));

            services
             .AddIdentity<ApplicationUser, IdentityRole>()
             .AddEntityFrameworkStores<ApplicationDbContext>()
             .AddDefaultTokenProviders();

            //services.AddIdentity<ApplicationUser, IdentityRole>(opt =>
            //{
            //    //previous code removed for clarity reasons

            //    opt.Lockout.AllowedForNewUsers = false;
            //    opt.Lockout.DefaultLockoutTimeSpan =new TimeSpan(24,0,0);
            //    opt.Lockout.MaxFailedAccessAttempts = 3;
            //})
            // .AddEntityFrameworkStores<ApplicationDbContext>()           
            // .AddDefaultTokenProviders();


            services.AddMemoryCache();
            services.AddSession();
            services.AddMvc(options =>
            {
                options.EnableEndpointRouting = false;
                var policy = new AuthorizationPolicyBuilder()

                               .RequireAuthenticatedUser()
                               .Build();
                options.Filters.Add(new AuthorizeFilter(policy));
            }).AddXmlSerializerFormatters();


            services.AddAuthorization(options =>
            {

                // inline policies
                options.AddPolicy("User", policy =>
                {

                    //policy.RequireAuthenticatedUser();
                    //policy.RequireClaim("user", "finance");
                    policy.RequireClaim("User", "Register");
                    policy.RequireRole("finance");
                });
            });
            //services.ConfigureApplicationCookie(o =>
            //{
            //    o.ExpireTimeSpan = TimeSpan.FromHours(1);
            //    o.SlidingExpiration = true;
            //    o.AccessDeniedPath = "/account/accessdenied";
            //    o.LoginPath = "/account/login";
            //    // o.ReturnUrlParameter = "RedirectUrl";
            //    o.LogoutPath = "/account/logout";

            //});
            JwtSecurityTokenHandler.DefaultInboundClaimTypeMap.Clear();
        
            services.AddAuthentication(options =>
            {
                options.DefaultScheme = "Main.Cookies";
                options.DefaultChallengeScheme = "oidc";
                options.DefaultAuthenticateScheme = "oidc";
            })
           .AddCookie("Main.Cookies", options => { options.AccessDeniedPath = "/account/accessdenied"; })
           .AddOpenIdConnect("oidc", options =>
           {
               options.Authority = "https://fintrakbankingmmbs.azurewebsites.net/";
               options.RequireHttpsMetadata = true;
               options.ClientId = "TheCoreBanking.Finance";
               //options.SignedOutCallbackPath = "/signout-callback-oidc";
               
               options.SignInScheme = "Main.Cookies";
               options.ResponseType = "code id_token";
               options.Scope.Clear();
               options.Scope.Add("openid");
               options.Scope.Add("profile");
               options.Scope.Add("email");
               options.Scope.Add("roles");
               options.GetClaimsFromUserInfoEndpoint = true;
               options.SaveTokens = true;
               options.ClientSecret = "secret";

               options.Events = new OpenIdConnectEvents()
               {
                   OnTokenValidated = tokenValidatedContext =>
                   {
                       return Task.FromResult(0);
                   },
                   OnUserInformationReceived = (context) =>
                   {
                       ClaimsIdentity claimsId = context.Principal.Identity as ClaimsIdentity;
                       try
                       {

                           dynamic userClaim = JObject.Parse(context.User.ToString());
                           var roles = userClaim.role;
                           foreach (string role in roles)
                           {
                               claimsId.AddClaim(new Claim("role", role));
                           }
                       }
                       catch (Exception)
                       {
                           //Users does not have roles
                       }
                       return Task.FromResult(0);
                   }

               };

               options.TokenValidationParameters = new TokenValidationParameters
               {
                   NameClaimType = JwtClaimTypes.Name,
                   RoleClaimType = JwtClaimTypes.Role
               };
           });
            



            services.AddDbContext<TheCoreBankingContext>(options => options.UseSqlServer("Server=(fintraksqlmmbs.database.windows.net;Database=TheCoreBankingAzure;user id=fintrak;password=Password20$;"), ServiceLifetime.Scoped);

            services.AddTransient<IEmailSender, EmailSender>();
            services.AddTransient<ICurrencyRepository, CurrencyRepository>();
            services.AddTransient<IServiceSetupRepository, ServiceSetupRepository>();
            services.AddTransient<ISectorRepository, SectorRepository>();
            services.AddTransient<IModelofIdRepository, ModelofIdRepository>();
            services.AddTransient<ICasaRepository, CasaRepository>();
            services.AddScoped<ISetupUnitOfWork, SetupUnitOfWork>();
            services.AddScoped<IRepositoryProvider, RepositoryProvider>();
            services.AddSingleton<RepositoryFactories>();

            services.AddSingleton<IFileProvider>(
                new PhysicalFileProvider(
                    Path.Combine(Directory.GetCurrentDirectory(), "wwwroot")));
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        [Obsolete]
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                //app.UseDatabaseErrorPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Index");
            }
            app.UseAuthentication();
            app.UseAuthorization();
            app.UseHttpsRedirection();
            app.UseStaticFiles();
          
            
            
            app.UseHsts();
            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=FinanceSetup}/{action=Index}/{id?}");
            });
        }


    }
}
