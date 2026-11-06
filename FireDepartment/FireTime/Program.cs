using FireTime.Interfaces.Repo_Interfaces;
using FireTime.Interfaces.Service_Interfaces;
using FireTime.Models;
using FireTime.Repositories;
using FireTime.Services;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Web;
using Microsoft.IdentityModel.Tokens;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddMicrosoftIdentityWebApi(builder.Configuration.GetSection("AzureAd"));

builder.Services.Configure<JwtBearerOptions>(JwtBearerDefaults.AuthenticationScheme, options =>
{
    options.Events = new JwtBearerEvents
    {
        OnMessageReceived = context =>
        {
            if(string.IsNullOrWhiteSpace(context.Token) && context.Request.Cookies.ContainsKey("access_token"))
            {
                context.Token = context.Request.Cookies["access_token"];
            }            
            return Task.CompletedTask;
        }
    };
});

builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("RequireEmployee", policy=>policy.RequireAuthenticatedUser());
    options.AddPolicy("Admin", policy => policy.RequireRole("Admin"));
    options.AddPolicy("Payroll", policy => policy.RequireRole("Payroll"));
    options.AddPolicy("ChiefAdmin", policy => policy.RequireRole("ChiefAdmin"));
    options.AddPolicy("FF", policy => policy.RequireRole("FF"));    
});

//Database Config
builder.Services.AddDbContext<IisFireTimeContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

//Cors Config
builder.Services.AddCors(options => options.AddPolicy("FireDepartment", policy =>
{
    policy.WithOrigins("http://localhost:4200").AllowAnyHeader().AllowAnyMethod().AllowCredentials();
}));

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();
builder.Services.AddAutoMapper(cfg => cfg.AddMaps(AppDomain.CurrentDomain.GetAssemblies()));

//Repos Registration
builder.Services.AddScoped<IAttendanceRepository, AttendanceRepository>();
builder.Services.AddScoped<IEmployeeRepository, EmployeeRepository>();
builder.Services.AddScoped<IEmployeeAssignmentRepository, EmployeeAssignmentRepository>();
builder.Services.AddScoped<ILookupsRepository, LookupsRepository>();
builder.Services.AddScoped<ILeaveRepository, LeaveRepository>();

//Services Registration
builder.Services.AddScoped<IAttendanceService, AttendanceService>();
builder.Services.AddScoped<IEmployeeService, EmployeeService>();
builder.Services.AddScoped<IEmployeeAssignmentService, EmployeeAssignmentService>();
builder.Services.AddScoped<ILookupsService, LookupsService>();
builder.Services.AddScoped<ILeaveService, LeaveService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if(app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.UseSwaggerUI(options =>
    {
        options.SwaggerEndpoint("/openapi/v1.json", "FireTime API");
        options.RoutePrefix = string.Empty;
    });
}
app.UseCors("FireDepartment");

app.UseHttpsRedirection();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();
