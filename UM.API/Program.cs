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

// todo cringe
builder.Services.AddSingleton<IVersionRepository>(c => new VersionRepository(new DatabaseOptions(){ConnectionString = conf.GetConnectionString("um_files")}));
builder.Services.AddScoped<IVersionService, VersionService>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors();

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.UseStaticFiles();

app.Run();