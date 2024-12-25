
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using SocialConnect.MappingConfigs;
using SocialConnect.Models;
using SocialConnect.UnitOfWorks;
using System.Text;

namespace SocialConnect
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen(options =>
            {
                // Add Swagger configurations
                options.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo
                {
                    Title = "SocialConnect API",
                    Version = "v1",
                    Description = "API for managing the SocialConnect application.",
                    Contact = new Microsoft.OpenApi.Models.OpenApiContact
                    {
                        Name = "Habiba Mohamed",
                        Email = "habiibamohamed259@gmail.com",
                    }
                });
                options.EnableAnnotations();
            });
            builder.Services.AddDbContext<SocialConnectDBContext>(option => option.UseLazyLoadingProxies(false).UseSqlServer(builder.Configuration.GetConnectionString("con")));
            builder.Services.AddIdentity<IdentityUser, IdentityRole>().AddEntityFrameworkStores<SocialConnectDBContext>();
            builder.Services.AddScoped<UnitOfWork>();
            builder.Services.AddAutoMapper(typeof(MapppingConfig));

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
