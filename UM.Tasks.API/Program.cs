using Microsoft.AspNetCore.HttpOverrides;
using Npgsql.Extension.Options;
using UM.Tasks.Repositories.Repositories.Task;
using UM.Tasks.Repositories.Repositories.Worker;
using UM.Tasks.Services.Services.Task;
using UM.Tasks.Services.Services.Worker;

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
builder.Services.AddSingleton<ITaskRepository>(_ => new TaskRepository(options));
builder.Services.AddSingleton<IWorkerRepository>(_ => new WorkerRepository(options));

builder.Services.AddScoped<IWorkerService, WorkerService>();
builder.Services.AddScoped<ITaskService, TaskService>();

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