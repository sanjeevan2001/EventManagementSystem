using EventManagement.Application.Abstraction.Persistences.IRepositories;
using EventManagement.Application.Features.venue.command.createVenue;
using EventManagement.Application.Interfaces.IServices;
using EventManagement.Application.Mapping;
using EventManagement.Application.Services;
using EventManagement.Application.Configuration;
using EventManagement.Infrastrure.Data;
using EventManagement.Infrastrure.Security;
using EventManagement.Infrastrure.Services;
using FluentValidation;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using MediatR;
using System.Text;
using EventManagement.Infrastrure.Persistence.Repository;
using EventManagement.Presentation.Middleware;
using EventManagement.Application.Behaviors;
using System.Net;
using System.Net.Sockets;



namespace EventManagement.Presentation
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add DbContext
            builder.Services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"))
            );

            // Add services to the container.

            builder.Services.AddControllers()
                .AddJsonOptions(options =>
                {
                    options.JsonSerializerOptions.ReferenceHandler = System.Text.Json.Serialization.ReferenceHandler.IgnoreCycles;
                });
            // Register AutoMapper profiles from Application mapping assembly
            builder.Services.AddAutoMapper(typeof(VenueProfile).Assembly);
            // Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
            //builder.Services.AddOpenApi();

            // Configure Email Settings
            builder.Services.Configure<EmailSettings>(builder.Configuration.GetSection("EmailSettings"));
            
            // Configure Cloudinary Settings
            builder.Services.Configure<CloudinarySettings>(builder.Configuration.GetSection("CloudinarySettings"));

          
            builder.Services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
            builder.Services.AddScoped<IVenueRepository, VenueRepository>();
            builder.Services.AddScoped<IPackageRepository, PackageRepository>();
            builder.Services.AddScoped<IEventRepository, EventRepository>();
            builder.Services.AddScoped<IItemRepository, ItemRepository>();
            builder.Services.AddScoped<IBookingRepository, BookingRepository>();
            builder.Services.AddScoped<IAssetRepository, AssetRepository>();
            builder.Services.AddScoped<IBookingPackageRepository, BookingPackageRepository>();
            builder.Services.AddScoped<IBookingItemRepository, BookingItemRepository>();
            builder.Services.AddScoped<IPackageItemRepository, PackageItemRepository>();
            builder.Services.AddScoped<IBookingWorkflowService, BookingWorkflowService>();
            builder.Services.AddScoped<IAuthRepository, AuthRepository>();
            builder.Services.AddScoped<IAuthService, AuthService>();
            builder.Services.AddScoped<ITokenService, TokenService>();
            builder.Services.AddScoped<IEmailService, EmailService>();
            builder.Services.AddScoped<ICloudinaryService, CloudinaryService>();

            // Register MediatR handlers (v11 API)
            builder.Services.AddMediatR(typeof(createVenueCommand).Assembly);

            // Register Validators
            builder.Services.AddValidatorsFromAssembly(typeof(createVenueCommand).Assembly);
            
            // Register Pipeline Behaviors
            builder.Services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));

            builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = new SymmetricSecurityKey(
                            Encoding.UTF8.GetBytes(builder.Configuration["TokenKey"] ?? string.Empty)
                        ),
                        ValidateIssuer = false,
                        ValidateAudience = false
                    };
                });

            builder.Services.AddCors(options =>
            {
                options.AddPolicy("Frontend", policy =>
                {
                    policy
                        // .WithOrigins("http://localhost:4200")
                        .AllowAnyOrigin()
                        .AllowAnyHeader()
                        .AllowAnyMethod();
                });
            });




            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();




            var app = builder.Build();

            // If configured ports are in use, pick fallback free ports to avoid AddressInUse on startup
            try
            {
                // Determine configured urls (from ASPNETCORE_URLS or launchSettings)
                var configuredUrls = builder.Configuration["ASPNETCORE_URLS"] ?? builder.Configuration["applicationUrl"];
                // default fallback ports
                var httpPort = 5241;
                var httpsPort = 7195;

                // try to parse configuredUrls if present
                if (!string.IsNullOrWhiteSpace(configuredUrls))
                {
                    var parts = configuredUrls.Split(new[] { ';' }, StringSplitOptions.RemoveEmptyEntries);
                    foreach (var p in parts)
                    {
                        if (p.StartsWith("http://", StringComparison.OrdinalIgnoreCase))
                        {
                            if (int.TryParse(new Uri(p).Port.ToString(), out var parsed)) httpPort = parsed;
                        }
                        else if (p.StartsWith("https://", StringComparison.OrdinalIgnoreCase))
                        {
                            if (int.TryParse(new Uri(p).Port.ToString(), out var parsed)) httpsPort = parsed;
                        }
                    }
                }

                bool IsPortFree(int port)
                {
                    try
                    {
                        var listener = new TcpListener(IPAddress.Loopback, port);
                        listener.Start();
                        listener.Stop();
                        return true;
                    }
                    catch
                    {
                        return false;
                    }
                }

                var finalHttp = httpPort;
                var finalHttps = httpsPort;

                if (!IsPortFree(httpPort))
                {
                    // find an available port
                    for (int p = 5200; p < 5300; p++)
                    {
                        if (IsPortFree(p))
                        {
                            finalHttp = p;
                            break;
                        }
                    }
                }

                if (!IsPortFree(httpsPort))
                {
                    for (int p = 7100; p < 7200; p++)
                    {
                        if (IsPortFree(p))
                        {
                            finalHttps = p;
                            break;
                        }
                    }
                }

                var urls = $"http://127.0.0.1:{finalHttp};https://127.0.0.1:{finalHttps}";
                Console.WriteLine($"Using URLs: {urls}");
                builder.WebHost.UseUrls(urls);

                // rebuild host with updated urls
                app = builder.Build();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Port check failed: {ex.Message}");
            }

            // Seed Database
            using (var scope = app.Services.CreateScope())
            {
                var db = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
                // DbSeeder.Seed(db);  // call our seeder method
            }





            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                //app.MapOpenApi();
                app.UseSwagger();
                app.UseSwaggerUI(options =>
                {
                    options.SwaggerEndpoint("/swagger/v1/swagger.json", "My API v1");
                    options.RoutePrefix = string.Empty;
                });
            }

            if (!app.Environment.IsDevelopment())
            {
                app.UseHttpsRedirection();
            }

            app.UseRouting();
            app.UseMiddleware<ApiExceptionMiddleware>();
            app.UseCors("Frontend");

            app.UseAuthentication();
            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
