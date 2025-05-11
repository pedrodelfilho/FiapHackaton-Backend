using Api.Extensions;
using Hellang.Middleware.ProblemDetails;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Logging;

IdentityModelEventSource.ShowPII = true;

var builder = WebApplication.CreateBuilder(args);

// Configurações de serviços
builder.Services.AddCors();
builder.Services.AddApiProblemDetails();
builder.Services.AddControllers();
builder.Services.AddRouting(options => options.LowercaseUrls = true);
builder.Services.AddVersioning();
builder.Services.AddSwagger();
builder.Services.EmailConfiguration(builder.Configuration);
builder.Services.ResolveLog();
builder.Services.AddAuthentication(builder.Configuration);
builder.Services.RegisterServices(builder.Configuration);

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();
    await DependencyInjection.CreateRoles(roleManager);
}

// Pipeline da aplicação
app.UseProblemDetails();
app.UseSwaggerUI();
app.UseHttpsRedirection();

app.UseCors(builder => builder
    .SetIsOriginAllowed(origin => true)
    .AllowAnyMethod()
    .AllowAnyHeader()
    .AllowCredentials());

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
