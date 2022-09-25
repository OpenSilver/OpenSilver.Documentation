var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

const string corsApp = "corsapp";
//services cors
builder.Services.AddCors(p => p.AddPolicy(corsApp, b =>
{
    b.WithOrigins("http://localhost:55591/").AllowAnyMethod().AllowAnyHeader();
}));

var app = builder.Build();

//app cors
app.UseCors(corsApp);

// Configure the HTTP request pipeline.

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
