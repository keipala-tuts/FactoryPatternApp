using FactoryPattern.Data;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using FactoryPattern.Samples;
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();
builder.Services.AddSingleton<WeatherForecastService>();
builder.Services.AddTransient<ISample1, Sample1>(); // get a new one every time - whenever we ask for ISample1, we get an implementation (Sample1) of it - dependency injection
builder.Services.AddSingleton<Func<ISample1>>(x => () => x.GetService<ISample1>()!); // Can get rid of null warning by using (!) since we know better than compiler - we know we have an instance of Sample1 due to above line.

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();

app.UseRouting();

app.MapBlazorHub();
app.MapFallbackToPage("/_Host");

app.Run();
