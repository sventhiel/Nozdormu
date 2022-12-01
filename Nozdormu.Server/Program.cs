using Exceptionless;
using LiteDB;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddExceptionless("s2I5hEu9zfDuBYAb9BOTXCFaJiiHbN625kpUbSFv");

// LiteDB
builder.Services.AddSingleton(new ConnectionString(builder.Configuration["ConnectionStrings:Nozdormu"]));

builder.Services.AddAuthentication();

builder.Services.AddMvc(options =>
{
    //options.Filters.Add(new AuthAttribute());
    options.EnableEndpointRouting = false;
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.UseMvc(routes =>
{
    routes.MapRoute(
        name: "default",
        template: "{controller=Home}/{action=Index}/{id?}");
});

app.UseSwagger();
app.UseSwaggerUI(options =>
{
    options.SwaggerEndpoint("/swagger/v1/swagger.json", "v1");
    options.RoutePrefix = "swagger/api";
});

app.UseExceptionless();

app.Run();