using Core.Interfaces;
using Core.Middleware;
using Core.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddSingleton<ICourseService, CourseService>();
builder.Services.AddSingleton<IStudentService, StudentService>();
builder.Services.AddControllers();
builder.Services.AddSwaggerGen();
builder.Services.AddAuthorization();

var app = builder.Build();
app.UseErrorHandling();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseSemesterClosed();

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();
