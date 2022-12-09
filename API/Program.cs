using API.Extensions;
using Application.Helpers.MappingProfiles;
using Microsoft.EntityFrameworkCore;
using Persistence;
using MediatR;
using Application.Users;
using API.Middlewares;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddAutoMapper(typeof(MappingProfiles));
builder.Services.AddMediatR(typeof(CreateUser.CreateUserCommand).Assembly);

builder.Services.AddDbContext<DataContext>(options => 
{
    options.UseNpgsql(builder.Configuration.GetConnectionString("Postgres"));
});
// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddApplicationServicesExtensions();
builder.Services.AddIdentityServicesExtensions(builder.Configuration);

builder.Services.AddCors();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseCors(options => 
{
    options.AllowAnyOrigin()
	    .AllowAnyMethod()
	    .AllowAnyHeader();
});
app.UseAuthentication();
app.UseAuthorization();
app.UseMiddleware<ErrorHandlingMiddleware>();
app.UseMiddleware<AuthenticatedUserGetterMiddleware>();
app.MapControllers();

app.Run();
