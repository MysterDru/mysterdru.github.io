using BlazorStatic;
using mysterdru.github.io.Components;

var builder = WebApplication.CreateBuilder(args);


builder.WebHost.UseStaticWebAssets();

builder.Services.AddBlazorStaticService(opt =>
{
	opt.OutputFolderPath = "../../docs/d";
	
	opt.PagesToGenerate.Add(new($"/", "index.html"));
	opt.PagesToGenerate.Add(new($"/sessions", "sessions/index.html"));
	opt.PagesToGenerate.Add(new($"/sessions/kcdc-2024", "sessions/kcdc-2024/index.html"));
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

app.UseHttpsRedirection();

app.UseStaticFiles();
app.UseAntiforgery();

app.MapRazorComponents<App>();

app.UseBlazorStaticGenerator(shutdownApp: !app.Environment.IsDevelopment());

app.Run();
