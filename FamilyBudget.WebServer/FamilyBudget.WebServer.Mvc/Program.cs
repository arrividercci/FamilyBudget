using FamilyBudget.WebServer.Data;
using FamilyBudget.WebServer.Data.Entities;
using FamilyBudget.WebServer.Data.Repositories;
using FamilyBudget.WebServer.Mvc.Services.RabbitMq;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddTransient<IUserRepository, UserRepository>();
builder.Services.AddTransient<IFamilyRepository, FamilyRepository>();
builder.Services.AddScoped<IRabbitMQService, RabitMQService>();

var rabbitMqConfig = builder.Configuration
    .GetSection("RabbitMqConfiguration")
    .Get<RabbitMqConfiguration>();

builder.Services.AddSingleton(rabbitMqConfig);

builder.Services.AddDbContext<FamilyBudgetDbContext>((serviceProvider, dbContextOptionsBuilder) =>
{
    dbContextOptionsBuilder.UseNpgsql(
        serviceProvider.GetRequiredService<IConfiguration>().GetConnectionString("FamilyBudget"),
        npgsqlOptionsBuilder =>
            npgsqlOptionsBuilder.MigrationsAssembly(typeof(FamilyBudgetDbContext).Assembly.FullName));
});

builder.Services.AddIdentity<User, IdentityRole>().AddEntityFrameworkStores<FamilyBudgetDbContext>().AddDefaultTokenProviders();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}
else
{
    using var scope = app.Services.CreateScope();
    var dbContext = scope.ServiceProvider.GetRequiredService<FamilyBudgetDbContext>();
    await dbContext.Database.MigrateAsync();
}    

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Accounts}/{action=Login}/{id?}");

app.Run();
