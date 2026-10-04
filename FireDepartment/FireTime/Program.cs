using FireTime;
using FireTime.Controllers;
using FireTime.Interfaces.RepoInterfaces;
using FireTime.Interfaces.ServiceInterfaces;
using FireTime.Models;
using FireTime.Repositories;
using FireTime.Services;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Web;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddMicrosoftIdentityWebApi(builder.Configuration.GetSection("AzureAd"));

builder.Services.AddDbContext<IisFireTimeContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")
        ?? throw new InvalidOperationException("DefaultConnection is not configured.")));
builder.Services.AddHttpContextAccessor();
builder.Services.AddScoped<DevelopmentOnlyFilter>();
builder.Services.AddCrudResources();
builder.Services.AddScoped<IAttendanceRepository, AttendanceRepository>();
builder.Services.AddScoped<IAttendanceService, AttendanceService>();
builder.Services.AddScoped<IEmployeeRepository, EmployeeRepository>();
builder.Services.AddScoped<IEmployeeService, EmployeeService>();
builder.Services.AddScoped<ICrudRepository<LeaveRequest, int>, EfCrudRepository<LeaveRequest, int>>();
builder.Services.AddScoped<ICrudRepository<TransferRequest, int>, EfCrudRepository<TransferRequest, int>>();
builder.Services.AddScoped<IWorkflowLookupRepository, WorkflowLookupRepository>();
builder.Services.AddScoped<IRequestWorkflowService, RequestWorkflowService>();

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

var app = builder.Build();

// Configure the HTTP request pipeline.
if(app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
