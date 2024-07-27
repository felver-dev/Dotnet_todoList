
using todo_back.infrastructure.extensions;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddControllers();
builder.Services.AddContextService(builder.Configuration);

builder.Services.AddCors(options =>
	{
		options.AddPolicy("Cors", policy =>
		{
			policy
				.AllowAnyHeader()
				.AllowAnyMethod()
				.AllowAnyOrigin();

		});
	}
);
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
	app.UseSwagger();
	app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.MapControllers();

app.UseCors("Cors");
app.Run();
