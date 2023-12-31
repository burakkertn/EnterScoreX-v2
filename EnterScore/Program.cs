using DataAccessLayer.Context;
using BusinessLayer.Container;
using EnterScore.Utils.ConfigOptions;
using EnterScore.Services;
using FluentValidation;
using BusinessLayer.ValidationRules.ContactUs;
using DTOLayer.DTOs.ContactUsDTOs;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddAutoMapper(typeof(Program));
builder.Services.ContainerDependencies();

builder.Services.Configure<GCSConfigOptions>(builder.Configuration);
builder.Services.AddSingleton<ICloudStorageService, CloudStorageService>();

builder.Services.AddDbContext<EnterScoreXContext>();
builder.Services.AddControllersWithViews();
builder.Services.AddMvc();
builder.Services.AddScoped<IValidator<SendMessageDto>, SendContactUsValidator>();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();
app.UseStatusCodePagesWithReExecute("/ErrorPage/ErrorPage"/*, "?code{}"*/);
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Fixture}/{action=Index}/{id?}");

app.MapControllerRoute(
    name: "Areas",
    pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}");


app.Run();
