
using AutoMapper;
using GestorEvento.Api.Servicios;
using GestorEvento.Application.DTOs;
using GestorEvento.Application.Interfaces;
using GestorEvento.Application.Services;
using GestorEvento.Infrastructure.Core;
using GestorEvento.Infrastructure.Interfaces;
using GestorEvento.Infrastructure.Persistence;
using GestorEvento.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;



namespace GestorEvento.Api
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            var mapperConfig = Mappers.MapperConfiguration(); 
            IMapper mapper = mapperConfig.CreateMapper();
            builder.Services.AddSingleton(mapper);

            builder.Services.AddScoped<UnitOfWork>();

            builder.Services.AddScoped<IUsuarioRepository, UsuarioRepository>();
            builder.Services.AddScoped<UsuarioService>();
            builder.Services.AddScoped<ISalaRepository, SalaRepository>();
            builder.Services.AddScoped<SalaService>();
           
            builder.Services.AddScoped<IParticipanteRepository, ParticipanteRepository>();
            builder.Services.AddScoped<ParticipanteService>();
           
            builder.Services.AddScoped<IEventoRepository, EventoRepository>();
            builder.Services.AddScoped<EventoService>();

            builder.Services.AddScoped<IRegistrarParticipanteService, RegistrarParticipanteService>();


            builder.Services.AddTransient<IServicioEmail, ServicioEmail>();

            builder.Services.AddDbContext<GestorDbcontext>(options =>
             options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"))
             );


            var app = builder.Build();

            using (var scope = app.Services.CreateScope())
            {
                var dataContext = scope.ServiceProvider.GetRequiredService<GestorDbcontext>();
                dataContext.Database.Migrate();
            }

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
