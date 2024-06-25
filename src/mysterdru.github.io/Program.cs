using BlazorStatic;
using mysterdru.github.io;
using mysterdru.github.io.Components;

var builder = WebApplication.CreateBuilder(args);


builder.WebHost.UseStaticWebAssets();

builder.Services.AddBlazorStaticService(opt =>
{
    var root = "dist";

    opt.OutputFolderPath = $"../../{root}/";

    opt.PagesToGenerate.Add(new($"/{root}", $"index.html"));
    opt.PagesToGenerate.Add(new($"/{root}/404", $"404/index.html"));
    opt.PagesToGenerate.Add(new($"/{root}/sessions", $"sessions/index.html"));
    opt.PagesToGenerate.Add(new($"/{root}/sessions/kcdc-2024", $"sessions/kcdc-2024/index.html"));
});

#if DEBUG
builder.Services.AddSassCompiler();
#endif

// Add services to the container.
builder.Services.AddRazorComponents();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

// this is only used for debugging, and not the static site generator. static assets will be published to /dist
// directory, but that path is not easily setup for the scoped asset file or the site index page when debugging locally in the IDD
app.MapRedirect("/", "/dist");
app.MapRedirect("/dist/mysterdru.github.io.styles.css", "/mysterdru.github.io.styles.css");
app.MapRedirect("/dist/scripts.js", "/scripts.js");
app.MapRedirect("/dist/images/profile.png", "/images/profile.png");
app.MapRedirect("/dist/css/app.css", "/css/app.css");
app.MapRedirect("/dist/css/lib/styles.css", "/css/lib/styles.css");
app.MapRedirect("/dist/css/lib/styles.css.map", "/css/lib/styles.css.map");


app.UseHttpsRedirection();

app.UseStaticFiles();
app.UseAntiforgery();

app.MapRazorComponents<App>();

app.UseBlazorStaticGenerator(shutdownApp: !app.Environment.IsDevelopment());

app.Run();