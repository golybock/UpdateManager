using Microsoft.AspNetCore.HttpOverrides;
using Npgsql.Extension.Options;
using UM.Repositories.Repositories;
using UM.Services.Services;

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

var conf = builder.Configuration;


var options = new DatabaseOptions() {ConnectionString = conf.GetConnectionString("um_files")};

builder.Services.AddSingleton<IVersionRepository>(_ => new VersionRepository(options));
builder.Services.AddScoped<IVersionService, VersionService>();

var app = builder.Build();

app.UseForwardedHeaders(new ForwardedHeadersOptions
{
    ForwardedHeaders = ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto
});

// пока нужен
app.UseSwagger();
app.UseSwaggerUI();

app.UseCors();

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();