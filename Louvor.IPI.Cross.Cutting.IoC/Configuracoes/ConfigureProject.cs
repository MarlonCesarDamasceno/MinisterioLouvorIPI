






using Louvor.IPI.Core.Service;
using Louvor.IPI.Domain.EnviaMail;
using Louvor.IPI.Domain.Interface.Context;
using Louvor.IPI.Domain.Interface.EnviaMail;
using Louvor.IPI.Domain.Interface.Repository;
using Louvor.IPI.Domain.Interface.Service;
using Louvor.IPI.Persistence.Context;
using Louvor.IPI.Persistence.Repository;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace Louvor.IPI.Cross.Cutting.IoC.Configuracoes
{
    public static class ConfigureProject
    {

        public static IServiceCollection ConfigurarDependencias(this IServiceCollection service, IConfiguration configuration)
        {
            //base de dados
            service.AddDbContext<LouvorContext>(options => options.UseMySql(ConnectionString(configuration), ServerVersion.AutoDetect(ConnectionString(configuration))));

            //services
            service.AddScoped<IMusicaService, MusicaService>();
            service.AddScoped<IUsuariosService, UsuariosService>();


            //Repository
            service.AddScoped<IUsuariosRepository, UsuariosRepository>();
            service.AddScoped<IMusicaRepository, MusicasRepository>();
            service.AddScoped<ILouvorContext, LouvorContext>();

            service.AddTransient<ICentralComunicacaoMail, CentralMailComunicacao>();
            return service;
        }

        public static string ConnectionString(IConfiguration configuration)
        {
            var MYSQL = $"Server={configuration.GetSection("ConfigDb:Server").Value};database={configuration.GetSection("ConfigDb:DataBase").Value}; uid={configuration.GetSection("ConfigDb:User").Value}; pwd={configuration.GetSection("ConfigDb:Pass").Value}";
            return MYSQL;
        }
    }
}
