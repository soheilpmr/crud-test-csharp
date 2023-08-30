
using Mc2.Crud.Services;
using Mc2.Crud.Services.UnitOfWork;
using Mc2.CrudTest.Client.Context;
using Mc2.CrudTest.Presentation.Shared.BaseInfra.UnitOfWork;
using Mc2.CrudTest.Presentation.Shared.BaseServicesImplemetion;
using MC2.Crud.Services.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using System.Configuration;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

//swagger
builder.Services.AddSwaggerGen(
    c =>
    {
        c.SwaggerDoc("v1", new OpenApiInfo { Title = "MC2.Crud.Client", Version = "v1" });

        var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
        var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
        c.IncludeXmlComments(xmlPath);
    });

// Write Site 
builder.Services.AddDbContext<WriteDBContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("WriteDb"));
});

// Read-Side
builder.Services.AddDbContext<ReadDBContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("ReadDb"));
});

builder.Services.AddScoped<IDateTimeProviderService, DateTimeProviderService>();
builder.Services.AddScoped(typeof(IServerReadUnitOfWork), typeof(ServerReadUnitOfWork<ReadDBContext>));
builder.Services.AddScoped(typeof(IServerWriteUnitOfWork), typeof(ServerWriteUnitOfWork<WriteDBContext>));
builder.Services.AddScoped<CustomerService, CustomerService>();


var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var dataContext = scope.ServiceProvider.GetRequiredService<WriteDBContext>();
    dataContext.Database.Migrate();

    var dataContext2 = scope.ServiceProvider.GetRequiredService<ReadDBContext>();
    dataContext2.Database.Migrate();
}

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
    app.UseSwagger();
    app.UseSwaggerUI();
}


app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Customer}/{action=Index}/{id?}");

app.Run();

