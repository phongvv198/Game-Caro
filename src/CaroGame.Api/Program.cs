var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Store giữ state dùng chung -> Singleton; service mỗi request -> Scoped.
builder.Services.AddSingleton<CaroGame.Api.Services.IGameStore, CaroGame.Api.Services.GameStore>();
builder.Services.AddScoped<CaroGame.Api.Services.IGameService, CaroGame.Api.Services.GameService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

// Phục vụ giao diện chơi cờ ở wwwroot/index.html.
app.UseDefaultFiles();
app.UseStaticFiles();

app.UseAuthorization();

app.MapControllers();

app.Run();
