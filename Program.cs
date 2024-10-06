using Lab1.Data;
using Lab1.Data.Repositories;
using Lab1.Data.Repositories.IRepositories;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Lägg till tjänster till containern.
builder.Services.AddDbContext<Lab1DbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
});
builder.Services.AddControllers();
builder.Services.AddScoped<ITableRepository, TableRepository>();
builder.Services.AddScoped<ICustomerRepository, CustomerRepository>();
builder.Services.AddScoped<IReservationRepository, ReservationRepository>();
builder.Services.AddScoped<IMenuItemRepository, MenuItemRepository>();

// Lägg till CORS-policy för att tillåta anrop från React-applikationen
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowReactApp",
        builder =>
        {
            builder.WithOrigins("http://localhost:5173") // URL för din React-applikation
                   .AllowAnyMethod()
                   .AllowAnyHeader();
        });
});

// Lägg till Swagger/OpenAPI för dokumentation (valfritt)
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Konfigurera HTTP-request pipeline
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

// Aktivera CORS-policy för att tillåta anrop från React-frontend
app.UseCors("AllowReactApp");

app.UseAuthorization();

app.MapControllers();

app.Run();
