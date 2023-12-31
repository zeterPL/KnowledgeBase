using KnowledgeBase.Data;
using KnowledgeBase.Data.Models;
using KnowledgeBase.Web.Configuration;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using NLog;
using NLog.Web;

var logger = NLog.LogManager.Setup().LoadConfigurationFromAppSettings().GetCurrentClassLogger();
try
{
    var builder = WebApplication.CreateBuilder(args);

	//AZURE CONNECTION
	var connectionString = builder.Configuration.GetConnectionString("AzureConnection");

	builder.Services.AddDbContext<KnowledgeDbContext>(options =>
		options.UseSqlServer(connectionString));

	builder.Services.AddDatabaseDeveloperPageExceptionFilter();

    builder.Services.AddDefaultIdentity<User>(options => options.SignIn.RequireConfirmedAccount = true)
        .AddEntityFrameworkStores<KnowledgeDbContext>();
    LogManager.Configuration.Variables["ConnectionStrings"] = connectionString;

    builder.Services.AddServices();
    builder.Services.AddRepositories();
    builder.Services.AddAutoMapper();

    builder.Services.AddPermissions();

    builder.Services.AddControllersWithViews();

    builder.Logging.ClearProviders();
    builder.Host.UseNLog();

    builder.Services.AddSwaggerGen(c =>
    {
        c.SwaggerDoc("v2", new OpenApiInfo { Title = "KnowledgeBase.Web.Api", Version = "v2" });
    });

    var app = builder.Build();

    using (var scope = app.Services.CreateScope())
    {
        var services = scope.ServiceProvider;

        var context = services.GetRequiredService<KnowledgeDbContext>();
        context.Database.EnsureCreated();
    }

    // Configure the HTTP request pipeline.
    if (app.Environment.IsDevelopment())
    {
        app.UseMigrationsEndPoint();
        app.UseSwagger();
        app.UseSwaggerUI(c =>
        {
            c.SwaggerEndpoint("/swagger/v2/swagger.json", "KnowledgeBase.Web.Api");
        });
    }
    else
    {
        app.UseExceptionHandler("/Error");
        // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
        app.UseHsts();
    }

    app.UseHttpsRedirection();
    app.UseStaticFiles();

    app.UseRouting();

    app.UseAuthentication();
    app.UseAuthorization();

    app.MapRazorPages();

    app.MapControllerRoute(
        name: "default",
        pattern: "{controller}/{action=Index}/{id?}",
        new { controller = "Home", action = "Index"});

    app.Run();
}
catch (Exception ex)
{
    logger.Error(ex);
    throw;
}
finally
{
    NLog.LogManager.Shutdown();
}