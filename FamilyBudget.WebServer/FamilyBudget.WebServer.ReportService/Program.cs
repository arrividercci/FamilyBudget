using Aspose.Cells.Charts;
using FamilyBudget.WebServer.ReportService;
using FamilyBudget.WebServer.ReportService.Services.Email;
using FamilyBudget.WebServer.ReportService.Services.Excel;
using Microsoft.Extensions.Configuration;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddHostedService<RabbitMQListener>();

var emailConfig = builder.Configuration
        .GetSection("EmailConfiguration")
        .Get<EmailConfiguration>();

builder.Services.AddSingleton(emailConfig);

var rabbitMqConfig = builder.Configuration
        .GetSection("RabbitMqConfiguration")
        .Get<RabbitMqConfiguration>();

builder.Services.AddSingleton(rabbitMqConfig);


builder.Services.AddTransient<EmailSender>();
builder.Services.AddScoped<UserExcelService>();
builder.Services.AddScoped<FamilyExcelService>();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    /*app.UseSwagger();
    app.UseSwaggerUI();*/
}

app.UseHttpsRedirection();

app.Run();
