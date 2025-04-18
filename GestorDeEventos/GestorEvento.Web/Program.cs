using GestorEvento.Application.Services;
using GestorEvento.Infrastructure.Interfaces;
using GestorEvento.Infrastructure.Repositories;
using GestorEvento.Application.Interfaces;
using Microsoft.EntityFrameworkCore;
using GestorEvento.Api.Servicios;
using GestorEvento.Infrastructure.Core;
using GestorEvento.Infrastructure.Persistence;

namespace GestorEvento.Web
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddScoped<IRegistrarParticipanteService, RegistrarParticipanteService>();
            builder.Services.AddScoped<IParticipanteRepository, ParticipanteRepository>();
            builder.Services.AddScoped<ParticipanteService>();
            builder.Services.AddScoped<IServicioEmail, ServicioEmail>();
            builder.Services.AddScoped<UnitOfWork>();

            builder.Services.AddSingleton<IConfiguration>(builder.Configuration);

            // Add services to the container.
            builder.Services.AddControllersWithViews();

            builder.Services.AddHttpClient();
            builder.Services.AddSession();
            builder.Services.AddHttpContextAccessor();

            builder.Services.AddDbContext<GestorDbcontext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));



            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();
            app.UseSession();

            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=UsuarioLogin}/{action=Index}/{id?}");

            app.Run();
        }
    }
}
