using System.Globalization;
using API_UploadFiles_SendEmail.Models.Options;
using API_UploadFiles_SendEmail.Models.Services.Application;
using API_UploadFiles_SendEmail.Models.Services.Infrastructure;
using MailKit.Custom.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Localization;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;

namespace API_UploadFiles_SendEmail;

public class Startup
{
    public Startup(IConfiguration configuration)
    {
        Configuration = configuration;
    }

    public IConfiguration Configuration { get; }

    public void ConfigureServices(IServiceCollection services)
    {
        services.AddControllers();
        services.AddSwaggerGen(c =>
        {
            c.SwaggerDoc("v1", new OpenApiInfo { Title = "API_UploadFiles_SendEmail", Version = "v1" });
        });

        services.AddTransient<IUploadFilesService, UploadFilesService>();
        services.AddTransient<IEmailSenderService, MailKitEmailSender>();

        // Options
        services.Configure<SmtpOptions>(Configuration.GetSection("Smtp"));
    }

    public void Configure(WebApplication app)
    {
        IWebHostEnvironment env = app.Environment;
        
        if (env.IsEnvironment("Development"))
        {
            app.UseDeveloperExceptionPage();
            app.UseSwagger();
            app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "API_UploadFiles_SendEmail v1"));
        }

        CultureInfo appCulture = new("it-IT");

        app.UseRequestLocalization(new RequestLocalizationOptions
        {
            DefaultRequestCulture = new RequestCulture(appCulture),
            SupportedCultures = new[] { appCulture }
        });
            
        app.UseHttpsRedirection();
        app.UseRouting();

        app.UseEndpoints(endpoints =>
        {
            endpoints.MapControllers();
        });
    }
}