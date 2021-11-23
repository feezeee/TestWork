using App.DAL.Interfaces;
using App.DAL.Models;
using App.DAL.Repositories;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.BLL.Infrastructure
{
    public static class BllServiceCollectionExtensions
    {        
        public static IServiceCollection AddBll(this IServiceCollection services)
        {
            services.AddTransient<IRepository<Worker>, WorkerRepository>();
            services.AddTransient<IUnitOfWork, MyUnitOfWork>();
            return services;
        }
    }
}
