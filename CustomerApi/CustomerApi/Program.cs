using Microsoft.EntityFrameworkCore;
using CustomerApi.Data;
using CustomerManagement.Repositories;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

// Configure DbContext with InMemoryDatabase for simplicity
builder.Services.AddDbContext<CustomerDbContext>(options =>
    options.UseInMemoryDatabase("CustomerDb"));
builder.Services.AddScoped<ICustomerRepository, CustomerRepository>();

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

WebApplication app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();
