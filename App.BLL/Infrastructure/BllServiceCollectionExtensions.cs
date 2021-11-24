using App.BLL.Interfaces;
using App.BLL.Services;
using App.DAL.Interfaces;
using App.DAL.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace App.BLL.Infrastructure
{
    public static class BllServiceCollectionExtensions
    {        
        /// <summary>
        /// Метод расширения для подключения необходимых сервисов
        /// </summary>
        /// <param name="services"></param>
        /// <returns></returns>
        public static IServiceCollection AddBll(this IServiceCollection services)
        {

            services.AddTransient<IUnitOfWork, MyUnitOfWork>();
            services.AddTransient<IWorkerService, WorkerService>();
            services.AddTransient<IPositionService, PositionService>();
            services.AddTransient<ICompanyService, CompanyService>();
            services.AddTransient<IFormTypeService, FormTypeService>();
            services.AddTransient<IManagerServices, ManagerServices>();
            return services;
        }
    }
}
