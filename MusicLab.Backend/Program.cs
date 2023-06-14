using MusicLab.Backend.Automapper;
using MusicLab.Backend.Middleware;
using MusicLab.Repository.Models;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllers();
builder.Services.AddSwaggerGen();
builder.Services.AddAutoMapper(typeof(AutoMapperConfig));
builder.Services.AddDbContext<MusicLabContext>();


var app = builder.Build();

app.UseCustomExceptionHandler();
app.UseHttpsRedirection();
app.UseSwagger();
app.UseSwaggerUI();
app.MapControllers();
app.UseMiddleware<CustomMiddleware>();

app.Run();
