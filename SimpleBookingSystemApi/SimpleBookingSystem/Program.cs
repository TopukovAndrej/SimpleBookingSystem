namespace SimpleBookingSystem.API
{
    using Microsoft.EntityFrameworkCore;
    using SimpleBookingSystem.Application;
    using SimpleBookingSystem.Contracts;
    using SimpleBookingSystem.Infrastructure.Context;
    using SimpleBookingSystem.Infrastructure.Data.Repositories.ResourceRepository;

    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            string? dbConnectionString = builder.Configuration.GetConnectionString(name: "DefaultConnection");

            builder.Services.AddDbContext<SimpleBookingSystemReadonlyDbContext>(optionsAction: options => options.UseSqlite(connectionString: dbConnectionString)
                                                                                                                 .UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking));
            builder.Services.AddScoped<ISimpleBookingSystemReadonlyDbContext, SimpleBookingSystemReadonlyDbContext>();

            builder.Services.AddDbContext<SimpleBookingSystemDbContext>(optionsAction: options => options.UseSqlite(connectionString: dbConnectionString));
            builder.Services.AddScoped<ISimpleBookingSystemDbContext, SimpleBookingSystemDbContext>();

            builder.Services.AddScoped<IResourceRepository, ResourceRepository>();

            builder.Services.AddCors(setupAction: options =>
            {
                options.AddPolicy(name: "AllowLocalhostClient",
                                  configurePolicy: policy =>
                                  {
                                      policy.WithOrigins("http://localhost:4200")
                                            .AllowAnyMethod()
                                            .AllowAnyHeader();
                                  });
            });

            builder.Services.AddMediatR(configuration: cfg =>
            {
                cfg.RegisterGenericHandlers = true;
                cfg.RegisterServicesFromAssemblies(typeof(ContractsAssemblyMarker).Assembly, typeof(ApplicationAssemblyMarker).Assembly);
            });

            builder.Services.AddControllers();
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            var app = builder.Build();

            app.UseCors(policyName: "AllowLocalhostClient");

            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                //app.UseSwaggerUI();
            }

            app.MapControllers();

            app.Run();
        }
    }
}
