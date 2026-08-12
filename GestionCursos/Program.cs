using Microsoft.EntityFrameworkCore;
using GestionCursos;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<GestionCursosDbContext>(options =>
    options.UseSqlite("Data Source=GestionCursos.db"));

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<GestionCursosDbContext>();
    db.Database.Migrate();
}

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.Run();