using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using MySql.Data.MySqlClient;
using RealmCommander.DataHelpers;
using RealmCommander.Repositories;
using RealmCommander.Services;

namespace RealmCommander
{
  public class Startup
  {

    string _connectionString;

    public Startup(IConfiguration configuration)
    {
      Configuration = configuration;
      _connectionString = Configuration.GetConnectionString("MySql");
    }

    public IConfiguration Configuration { get; }

    // This method gets called by the runtime. Use this method to add services to the container.
    public void ConfigureServices(IServiceCollection services)
    {
      services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);

      services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
        .AddCookie(options =>
        {
          options.LoginPath = "/account/login";
          options.Events.OnRedirectToLogin = (context) =>
          {
            context.Response.StatusCode = 401;
            return Task.CompletedTask;
          };
        });

      services.AddTransient<IDbConnection>(x => CreateDBContext());

      services.AddScoped<AccountRepository>();
      services.AddScoped<AccountService>();

      services.AddTransient<KnightsRepository>();
      services.AddTransient<KnightsService>();

      services.AddTransient<QuestsRepository>();
      services.AddTransient<QuestsService>();
    }

    private IDbConnection CreateDBContext()
    {
      IDbConnection connection = new MySqlConnection(_connectionString);
      connection.Open();
      return connection;
    }

    // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
    public void Configure(IApplicationBuilder app, IHostingEnvironment env)
    {
      if (env.IsDevelopment())
      {
        app.UseDeveloperExceptionPage();

        // #if DEBUG
        //         DbSetup setup = new DbSetup(CreateDBContext());
        //         setup.DropTables();
        //         setup.CreateTables();
        // #endif
      }
      else
      {
        // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
        app.UseHsts();
      }

      app.UseAuthentication();

      app.Use(async (httpContext, next) =>
      {
        AccountService accountService = httpContext.RequestServices.GetService<AccountService>();
        accountService.SetUser(httpContext.User.Identity.Name);
        accountService.SetContext(httpContext);
        await next.Invoke();
      });

      app.UseHttpsRedirection();
      app.UseMvc();
    }
  }
}
