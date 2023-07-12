using AutoMapper;
using KnowledgeBase.Data;
using KnowledgeBase.Data.Models;
using KnowledgeBase.Data.Repositories;
using KnowledgeBase.Data.Repositories.Interfaces;
using KnowledgeBase.Logic.AutoMapper;
using KnowledgeBase.Logic.Services;
using KnowledgeBase.Logic.Services.Interfaces;
using KnowledgeBase.Web.Configuration;
using Microsoft.EntityFrameworkCore;
using NLog;
using NLog.Web;

var logger = NLog.LogManager.Setup().LoadConfigurationFromAppSettings().GetCurrentClassLogger();
try
{
	var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<KnowledgeDbContext>(options =>
    options.UseSqlServer(connectionString,
        optionsSqlServer => { optionsSqlServer.MigrationsAssembly("KnowledgeBase.Data"); }));
builder.Services.AddDatabaseDeveloperPageExceptionFilter();

	builder.Services.AddDefaultIdentity<User>(options => options.SignIn.RequireConfirmedAccount = true)
		.AddRoles<Role>()
		.AddEntityFrameworkStores<KnowledgeDbContext>();
	LogManager.Configuration.Variables["ConnectionStrings"] = builder.Configuration.GetConnectionString("DefaultConnection");

builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

#region Dependency injection

builder.Services.AddScoped<IProjectRepository, ProjectRepository>();
builder.Services.AddScoped<IProjectService, ProjectService>();
builder.Services.AddScoped<IResourceRepository, ResourceRepository>();
builder.Services.AddScoped<IResourceService, ResourceService>();

builder.Services.AddScoped<IUserProjectPermissionRepository, UserProjectPermissionRepository>();
builder.Services.AddScoped<IPermissionService, PermissionService>();

#endregion

builder.Services.AddPermissions();

builder.Services.AddControllersWithViews();

	builder.Logging.ClearProviders();
	builder.Host.UseNLog();

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
    pattern: "{controller}/{action=Index}/{id?}");

	app.Run();
}
catch (Exception ex)
{
	logger.Error(ex);
	throw (ex);
}
finally
{
	NLog.LogManager.Shutdown();
}