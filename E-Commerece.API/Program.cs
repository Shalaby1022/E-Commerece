using Core.Interfaces;
using E_Commerece.API.ExceptionsConfiguration;
using InfraStructure.Data;
using InfraStructure.Repository;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;



var MyAllowSpecificOrigins = "_myAllowSpecificOrigins";
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo
    {
        Version = "v1",
        Title = "Online Shopping",
        Contact = new OpenApiContact
        {
            Name = "Shelby's Contact"
        }

    });
});


builder.Services.AddCors(options =>
{
    options.AddPolicy(name: MyAllowSpecificOrigins,
                      policy =>
                      {
                          policy
                          .AllowAnyOrigin()
                          .AllowAnyMethod()
                          .AllowAnyHeader();

                      });
});


builder.Services.AddDbContext<StoreDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));

});



builder.Services.AddScoped<IProductRepository, ProductRepository>();
builder.Services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));





var app = builder.Build();

using (var serviceScope = app.Services.CreateScope())
{
    var serviceProvider = serviceScope.ServiceProvider;
    try
    {
        var dbContext = serviceProvider.GetRequiredService<StoreDbContext>();
        dbContext.Database.Migrate(); // Ensure database is created and migrated

        // Seed data
        var logger = serviceProvider.GetRequiredService<ILogger<StoreContextSeed>>();
        await StoreContextSeed.SeedingData(dbContext, logger);
    }
    catch (Exception ex)
    {
        var logger = serviceProvider.GetRequiredService<ILogger<Program>>();
        logger.LogError(ex, "An error occurred while seeding the database.");
        throw; // Rethrow the exception to indicate a failure during seeding
    }
}


// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();

}


//app.UseStatusCodePagesWithReExecute("/Error/{0}");

app.UseHttpsRedirection();


app.UseCors(MyAllowSpecificOrigins);

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.AddGlobalErrorHandlingMiddlewares();

app.Run();
