using API.Extensions.ServiceCollection;
using Business.Data;
using Business.Helpers;
using Business.Identity;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

// Controllers 
builder.Services.AddControllers();

// API Endpoints Explorer
builder.Services.AddEndpointsApiExplorer();

// Swagger
builder.Services.AddSwaggerDocumentation();

// AutoMapper 
builder.Services.AddAutoMapper(typeof(MappingProfiles));

// Enable CORS
builder.Services.AddCors(opt =>
{
    opt.AddPolicy("ClientPolicy", policy =>
    {
        policy.WithOrigins("https://localhost:4200")
              .AllowAnyMethod()
              .AllowAnyHeader()
              .AllowCredentials();
    });
});

// Contexts 
builder.Services.AddDbContext<MusicDbContext>(x =>
    x.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
builder.Services.AddDbContext<AppIdentityDbContext>(x =>
    x.UseSqlServer(builder.Configuration.GetConnectionString("IdentityConnection")));

// Repositories 
builder.Services.AddRepositories();

// App Services 
builder.Services.AddServices();

// Identity
builder.Services.AddIdentityServices(builder.Configuration);

var app = builder.Build();

app.UseHttpsRedirection();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwaggerDocumentation();
    app.UseDeveloperExceptionPage();
}

app.UseRouting();

app.UseCors("ClientPolicy");

app.UseAuthentication();

app.UseAuthorization();

app.UseEndpoints(endpoints =>
{
    endpoints.MapControllers();
});

app.Run();