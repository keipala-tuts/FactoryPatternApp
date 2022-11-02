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
// The job of Func is to be a delegate. A delegate is a method can can be  passed around like a variable. Why? Because it allows others to call it.
// You don't the method right here, you let someone else call it later.
// The above line is saying: "Put <Func<ISample1>> into dependency injection. So when you can for a Func of ISample1, you get back an instance of { x => () => x.GetService<ISample1>()! } i.e. 1 method with no parameters, and it's job is that when you call it, it goes to the services and gets an ISample1, using depenency injection.

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
