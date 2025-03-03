
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Server.Kestrel.Core;
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

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen(options =>
            {
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
            builder.Services.AddDbContext<SocialConnectDBContext>(option => option.UseLazyLoadingProxies().UseSqlServer(builder.Configuration.GetConnectionString("con")));
            builder.Services.AddIdentity<User, IdentityRole>().AddEntityFrameworkStores<SocialConnectDBContext>();
            builder.Services.AddScoped<UnitOfWork>();
            builder.Services.AddAutoMapper(typeof(MapppingConfig));
            builder.Services.AddAuthentication(option => {
                option.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                option.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                option.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
                        })
                .AddJwtBearer(
                op =>
                {
                    //op.SaveToken = true;
                    #region secret key
                    var key = "welcome to my secret key Habiba Mohamed";
                    var secretkey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(key));
                    #endregion
                    op.TokenValidationParameters = new TokenValidationParameters()
                    {
                        IssuerSigningKey = secretkey,
                        ValidateIssuer = false,
                        ValidateAudience = false,
  
                    };
                });

            var app = builder.Build();

            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthentication();
            app.UseAuthorization();


            app.MapControllers();


            app.Run();
        }
    }
}
