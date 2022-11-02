using Microsoft.EntityFrameworkCore;
using ProEventos.Application.Application;
using ProEventos.Application.Interfaces;
using ProEventos.Persistence.Data;
using ProEventos.Persistence.Interfaces;
using ProEventos.Persistence.Persistencia;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers().AddNewtonsoftJson(
    x => x.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore
    );

builder.Services.AddCors();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<ProEventosContext>(
    context => context.UseSqlServer("Server=(localhost);Database=ProEventos;Trusted_Connection=True;MultipleActiveResultSets=true")
    );

builder.Services.AddScoped<IEventoService, EventoService>();
builder.Services.AddScoped<IBasePersistence, BasePersistence>();
builder.Services.AddScoped<IEventoPersistence, EventoPersistence>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

//app.UseHttpsRedirection();

app.UseAuthorization();
app.UseCors(cors => cors.AllowAnyHeader()
                        .AllowAnyMethod()
                        .AllowAnyOrigin());

app.MapControllers();

app.Run();
