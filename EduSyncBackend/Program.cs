var builder = WebApplication.CreateBuilder(args);

// Добавяме базата данни
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection")));

// Разрешаваме CORS (за React)
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll",
        policy => policy.AllowAnyOrigin()
                        .AllowAnyMethod()
                        .AllowAnyHeader());
});

// Добавяме контролерите
builder.Services.AddControllers();

var app = builder.Build();

// Активиране на CORS
app.UseCors("AllowAll");

if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}

// Добавяме маршрутизация
app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();
