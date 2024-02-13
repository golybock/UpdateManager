using Microsoft.AspNetCore.HttpOverrides;
using Npgsql.Extension.Options;
using UM.Files.Repositories.Repositories;
using UM.Files.Services.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddCors(cors =>
{
	cors.AddDefaultPolicy(options =>
	{
		options.AllowAnyMethod()
			.AllowAnyHeader()
			.AllowAnyOrigin();
	});
});

// set conf
var conf = builder.Configuration;
var options = new DatabaseOptions() {ConnectionString = conf.GetConnectionString("um_files")!};

// di
builder.Services.AddSingleton<IVersionRepository>(_ => new VersionRepository(options));
builder.Services.AddScoped<IVersionService, VersionService>();

var app = builder.Build();

if (app.Environment.IsProduction())
{
	app.UseForwardedHeaders(new ForwardedHeadersOptions
	{
		ForwardedHeaders = ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto
	});
}

app.Map("/", async (context) =>
{
	await context.Response.WriteAsync("available");
});

app.UseSwagger();
app.UseSwaggerUI();

app.UseCors();

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();