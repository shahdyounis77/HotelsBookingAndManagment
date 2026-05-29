using Hotel.Interfaces;
using Hotel.Models;
using Hotel.Repos;
using Hotel.Services;
using Hotel.Services.User;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.SqlServer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;
using System.Threading.Tasks;

namespace Hotel
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers().AddJsonOptions(options =>
            {
                options.JsonSerializerOptions.ReferenceHandler =
                    System.Text.Json.Serialization.ReferenceHandler.IgnoreCycles;
            });
            // Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
            builder.Services.AddOpenApi();

            builder.Services.AddDbContext<Context>(options =>
                options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
            builder.Services.AddIdentity<User, IdentityRole>()
               .AddEntityFrameworkStores<Context>();

            builder.Services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                
            }).AddJwtBearer(options =>
            {
                
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                   ValidateIssuerSigningKey = true,

                ValidIssuer= builder.Configuration["jwt:issuer"],
                    ValidAudience = builder.Configuration["jwt:audience"],

                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["jwt:key"])),
                   
                };
            });



            builder.Services.AddScoped<Regsiter>();
            builder.Services.AddScoped<LoginService>();
            builder.Services.AddScoped<GenerateToken>();
            builder.Services.AddScoped<Context>();
            builder.Services.AddScoped<IHotel, HotelRepo>();
            builder.Services.AddScoped<IRoom, RoomRepo>();
            builder.Services.AddScoped<IBooking, BookingRepo>();
            builder.Services.AddScoped<BookingServices>();
            builder.Services.AddScoped<RoomServices>();
            builder.Services.AddScoped<HotelServices>();

            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            builder.Services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "Hotel API",
                    Version = "v1"
                });

                options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Name = "Authorization",
                    Type = SecuritySchemeType.Http,
                    Scheme = "bearer",
                    BearerFormat = "JWT",
                    In = ParameterLocation.Header,
                    Description = "Enter JWT Token"
                });

                options.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            Array.Empty<string>()
        }
    });
            });


            builder.Services.AddCors(options =>
            {
                options.AddPolicy("AllowFrontend",
                    policy =>
                    {
                        policy.AllowAnyOrigin()
                              .AllowAnyHeader()
                              .AllowAnyMethod();
                    });
            });


            var app = builder.Build();

            

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.MapOpenApi();
                app.UseSwagger();
                app.UseSwaggerUI();
            }


           

            app.UseHttpsRedirection();
            app.UseAuthentication();
            app.UseCors("AllowFrontend");
            app.UseAuthorization();


            app.MapControllers();

           

            app.Run();
        }
    }
}
