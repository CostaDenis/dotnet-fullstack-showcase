using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi;
using Showcase.Api.Data;
using Showcase.Api.Repositories;
using Showcase.Api.Repositories.Abstractions;
using Showcase.Api.Services;
using Showcase.Api.Services.Abstractions;
using Showcase.Core;

namespace Showcase.Api.Configuration
{
    public static class BuilderExtension
    {

        public static void AddConfiguration(this WebApplicationBuilder builder)
        {
            ConfigurationClass.ConnectionString = builder.Configuration
                .GetConnectionString("DefaultConnection")
                    ?? throw new InvalidOperationException("Connection String não encontrada!");

            ConfigurationClass.JwtKey = builder.Configuration["Jwt:Key"]
                    ?? throw new InvalidOperationException("Chave Jwt não encontrada!");

            ConfigurationClass.JwtIssuer = builder.Configuration["Jwt:Issuer"]
                    ?? throw new InvalidOperationException("Issuer não encontrado!");

            ConfigurationClass.JwtAudience = builder.Configuration["Jwt:Audience"]
                    ?? throw new InvalidOperationException("Audience não encontrado!");

            ConfigurationClass.BackendURL = builder.Configuration["BackendURL"]
                    ?? throw new InvalidOperationException("URL do Backend não encontrada!");

            ConfigurationClass.FrontEndURL = builder.Configuration["FrontendURL"]
                    ?? throw new InvalidOperationException("URl do Frontend não encontrada!");
        }

        public static void AddDbContext(this WebApplicationBuilder builder)
            => builder.Services.AddDbContext<AppDbContext>(options =>
            {
                options.UseNpgsql(ConfigurationClass.ConnectionString);
            });

        public static void AddCors(this WebApplicationBuilder builder)
        {
            builder.Services.AddCors(options =>
            {
                options.AddPolicy("showcase", policy =>
                {
                    policy
                        .WithOrigins(
                            ConfigurationClass.BackendURL,
                            ConfigurationClass.FrontEndURL
                        )
                    .AllowAnyHeader()
                    .AllowAnyMethod();
                });
            });
        }

        public static void AddDocumentation(this WebApplicationBuilder builder)
        {
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen(options =>
            {
                options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Name = "Authorization",
                    Type = SecuritySchemeType.Http,
                    Scheme = "bearer",
                    BearerFormat = "JWT",
                    In = ParameterLocation.Header,
                    Description = "Insira o token Jwt"
                });

                options.AddSecurityRequirement(document => new OpenApiSecurityRequirement
                {
                    [new OpenApiSecuritySchemeReference("Bearer", document)] = []
                });
            });
        }

        public static void AddServices(this WebApplicationBuilder builder)
        {
            builder.Services.AddHttpContextAccessor();

            //repositories
            builder.Services.AddScoped<IEmployeeRepository, EmployeeRepository>();
            builder.Services.AddScoped<ICategoryRepository, CategoryRepository>();
            builder.Services.AddScoped<IProductRepository, ProductRepository>();

            //services
            builder.Services.AddScoped<IAuthService, AuthService>();
            builder.Services.AddScoped<ICategoryService, CategoryService>();
            builder.Services.AddScoped<IEmailService, EmailService>();
            builder.Services.AddScoped<IEmployeeService, EmployeeService>();
            builder.Services.AddScoped<IProductService, ProductService>();
            builder.Services.AddScoped<ITokenService, TokenService>();


            //others
            builder.Services.AddControllers();
        }

        public static void AddSecurity(this WebApplicationBuilder builder)
        {
            builder.Services.AddAuthentication(options =>
            {
                options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateLifetime = true,
                    IssuerSigningKey = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(ConfigurationClass.JwtKey)),
                    ValidateIssuer = false,
                    ValidateAudience = false
                };
            });

            builder.Services.AddAuthorization();
        }
    }
}