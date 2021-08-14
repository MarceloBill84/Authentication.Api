using Authentication.CrossCutting.Exceptions;
using Authentication.CrossCutting.IoC;
using Authentication.Domain.Models;
using Authentication.Infra;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using Newtonsoft.Json;
using System.Text;

namespace Authentication
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
			services.AddDbContext<AuthenticationContext>(p => p.UseSqlServer(Configuration.GetConnectionString("AuthenticationDB")));
			services.AddScoped<AuthenticationContext, AuthenticationContext>();

			var applicationConfig = new ApplicationConfig
			{
				JwtSecret = Configuration.GetValue<string>("JwtSecret")
			};

			services.AddCors(o => o.AddPolicy("MyPolicy", builder =>
			{
				builder.AllowAnyOrigin()
					   .AllowAnyMethod()
					   .AllowAnyHeader();
			}));

			services.AddSingleton(applicationConfig);

			services.RegisterRepositories();
			services.RegisterServices();
			services.RegisterAppServices();

			services.AddControllers();
			services.AddSwaggerGen(c =>
			{
				c.SwaggerDoc("v1", new OpenApiInfo { Title = "Authentication", Version = "v1" });
			});
		}

		// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
		public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
		{
			if (env.IsDevelopment())
			{
				app.UseDeveloperExceptionPage();
				app.UseSwagger();
				app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Authentication v1"));
			}
			app.UseCors("MyPolicy");
			app.UseHttpsRedirection();

			app.UseRouting();
			app.UseExceptionHandler(builder =>
			{
				builder.Run(async context =>
				{
					var exceptionHandlerFeature = context.Features.Get<IExceptionHandlerFeature>();

					if (exceptionHandlerFeature != null && exceptionHandlerFeature.Error is ExceptionBase)
					{
						var customException = exceptionHandlerFeature.Error as ExceptionBase;

						context.Response.StatusCode = (int)customException.StatusCode;
						context.Response.ContentType = "application/json";

						var json = JsonConvert.SerializeObject(new { customException.Message });
						byte[] result = Encoding.UTF8.GetBytes(json);
						await context.Response.Body.WriteAsync(result, 0, result.Length);
					}
				});
			});
			app.UseAuthorization();

			app.UseEndpoints(endpoints =>
			{
				endpoints.MapControllers();
			});
		}
	}
}
