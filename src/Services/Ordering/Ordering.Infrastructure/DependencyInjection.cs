using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Ordering.Application.Constracts.Infrastructure;
using Ordering.Application.Constracts.Persistance;
using Ordering.Application.Models;
using Ordering.Infrastructure.Mail;
using Ordering.Infrastructure.Persistance;
using Ordering.Infrastructure.Repositories;

namespace Ordering.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructureService(this IServiceCollection services,IConfiguration configuration )
        {
            services.AddDbContext<OrderContext>(o =>
                o.UseSqlServer(c => configuration.GetConnectionString("OrderingConnectionString")));

            services.AddScoped(typeof(IGenericRepository<>),typeof(RepositoryBase<>));
            services.AddScoped<IOrderRepository,OrderRepository>();

            services.Configure<EmailSettings>(c => configuration.GetSection("EmailSettings"));
            services.AddTransient<IEmailService, EmailService>();

            return services;
        }
    }
}
