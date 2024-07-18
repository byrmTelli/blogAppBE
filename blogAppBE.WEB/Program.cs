using blogAppBE.DAL.Abstract;
using blogAppBE.DAL.Concrete;
using blogAppBE.SERVICE.Abstract;
using blogAppBE.SERVICE.Concrete;



var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddControllers();

builder.Services.AddScoped<ICategoryService,CategoryService>();
builder.Services.AddScoped<IPostService, PostService>();

builder.Services.AddScoped<ICategoryDal,CategoryDal>();
builder.Services.AddScoped<IPostDal, PostDal>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthentication();
app.MapControllers();


app.Run();

