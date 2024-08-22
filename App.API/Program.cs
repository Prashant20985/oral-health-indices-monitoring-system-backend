using App.API.Extensions;
using App.API.Middleware;
using Azure.Identity;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddHttpContextAccessor();

// Add services to the container.

builder.Services.AddControllers();

// Add Azure App Configuration
builder.Configuration.AddAzureAppConfiguration(options =>
{
    var appUri = new Uri(builder.Configuration.GetConnectionString("AppConfigUri"));
    var credentials = new DefaultAzureCredential();
    options.Connect(appUri, credentials);
});

// Serilog 
string logsConnectionString = builder.Configuration["LogsDatabaseSettings:ConnectionString"];
string collectionName = builder.Configuration["LogsDatabaseSettings:CollectionName"];

builder.Host.UseSerilog((context, configuration) =>
{
    configuration
        .ReadFrom.Configuration(context.Configuration)
        .WriteTo.MongoDB(logsConnectionString, collectionName);
});

// Extensions
builder.Services.AddApplicationServices(builder.Configuration);
builder.Services.AddIdentityServices(builder.Configuration);

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
else
{
    app.Use(async (context, next) =>
    {
        context.Response.Headers.Append("Strict-Transport-Security", "max-age=31536000");
        await next.Invoke();
    });
}

app.UseCors("CorsPolicy");

app.UseSerilogRequestLogging();

app.UseMiddleware<ExceptionMiddleware>();

app.UseXContentTypeOptions();

app.UseReferrerPolicy(opt => opt.NoReferrer());

app.UseXXssProtection(opt => opt.EnabledWithBlockMode());

app.UseXfo(opt => opt.Deny());

// Add Content Security Policy
app.UseCsp(opt => opt
    .BlockAllMixedContent()
    .StyleSources(s => s.Self().CustomSources("https://fonts.googleapis.com"))
    .FontSources(s => s.Self().CustomSources("https://fonts.gstatic.com", "data:"))
    .FormActions(s => s.Self())
    .FrameAncestors(s => s.Self())
    .ImageSources(s => s.Self().CustomSources("data:"))
    .ScriptSources(s => s.Self())
);

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
