using System.Reflection;
using DAL;
using Logic.Utilities;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using System.Text.Json.Serialization;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;
using Logic;
using Resources.Interfaces;

namespace API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddControllers().AddJsonOptions(options =>
            {
                options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.Preserve;
            });

            //DI
            builder.Services.AddScoped<IUserRepository, UserRepository>();
            builder.Services.AddScoped<UserService>();
            builder.Services.AddScoped<IProductRepository, ProductRepository>();
            builder.Services.AddScoped<ProductService>();

            #region CORS Setup

            // CORS setup
            builder.Services.AddCors(options =>
            {
                options.AddPolicy("AllowReactApp", policy =>
                {
                    policy.WithOrigins("http://localhost:3000")
                        .AllowAnyMethod()
                        .AllowAnyHeader();
                });
            });

            #endregion

            #region Configuration Setup

            // Add configuration setup
            var configuration = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json")
                .Build();

            builder.Services.AddSingleton(configuration);
            builder.Services.AddSingleton<AppConfiguration>();

            #endregion

            #region JWT Authentication Setup

            // JWT Authentication setup
            var jwtKey = builder.Configuration["Jwt:Key"] ?? throw new ArgumentNullException(nameof(args));
            var key = Encoding.ASCII.GetBytes(jwtKey);

            builder.Services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = "LoginManager",  // Replace with your issuer
                    ValidAudience = "User",        // Replace with your audience
                    IssuerSigningKey = new SymmetricSecurityKey(key) // Use the same key for validation
                };
            });

            JwtGenerator.Key = jwtKey;

            #endregion

            #region Swagger Setup

            // Swagger setup
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc("v1", new OpenApiInfo
                {
                    Version = "v1",
                    Title = "Individual Project API",
                    Description = "API for the Individual Project, online store",
                    Contact = new OpenApiContact
                    {
                        Name = "Max Delissen",
                        Email = "maxdelissen17@gmail.com",
                    }
                });

                var xmlFilename = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                options.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, xmlFilename));

                // Add Bearer token authorization to Swagger UI
                options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Name = "Authorization",
                    Type = SecuritySchemeType.Http,
                    Scheme = "bearer",
                    BearerFormat = "JWT",
                    In = ParameterLocation.Header,
                    Description = "JWT Authorization header using the Bearer scheme. Example: \"Bearer {token}\""
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
                        new string[] {}
                    }
                });
            });

            #endregion

            var app = builder.Build();

            #region HTTP Request Pipeline

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            // Use Authentication and Authorization
            app.UseAuthentication();
            app.UseAuthorization();

            app.UseCors("AllowReactApp");

            app.MapControllers();
            app.Run();

            #endregion
        }
    }
}
