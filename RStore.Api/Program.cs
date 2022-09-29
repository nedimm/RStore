using Microsoft.EntityFrameworkCore;
using RStore.Api.Configurations;
using RStore.Api.Data;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

var connectionString = builder.Configuration.GetConnectionString("RStoreDbConnection");
builder.Services.AddDbContext<RStoreDbContext>(options => options.UseSqlServer(connectionString));
builder.Services.AddAutoMapper(typeof(MapperConfig));
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Host.UseSerilog((ctx, lc) =>
    lc.WriteTo.Console().ReadFrom.Configuration(ctx.Configuration));
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", b =>
    b.AllowAnyMethod()
    .AllowAnyHeader()
    .AllowAnyOrigin());
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseCors("AllowAll");

app.UseAuthorization();

app.MapControllers();

app.Run();
