using Presentation;
using Presentation.Common.Middleware;
using Presentation.Components;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.ConfigureServices();
var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    app.UseHsts();
}
app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseAntiforgery();
app.UseMiddleware<RedirectMiddleware>();
app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();
app.Run();
