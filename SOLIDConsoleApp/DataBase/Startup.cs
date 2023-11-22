using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;

namespace SOLIDConsoleApp.DataBase
{
    
    internal class Startup : DbContext
    {
        private readonly IConfiguration _configuration;

        Startup(IConfiguration configuration)
        {
            this._configuration = configuration;
        }
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<ClientContext>(
                option => option.UseSqlServer(this._configuration.GetConnectionString("ClientContext")));

            services.AddDbContext<ClientContext>(
                option => option.UseSqlServer(this._configuration.GetConnectionString("BalanceContext")));

            services.AddDbContext<ClientContext>(
                option => option.UseSqlServer(this._configuration.GetConnectionString("CreditCardContext")));

            services.AddDbContext<ClientContext>(
                option => option.UseSqlServer(this._configuration.GetConnectionString("CreditContext")));

            services.AddDbContext<ClientContext>(
                option => option.UseSqlServer(this._configuration.GetConnectionString("TransactionContext")));
        }
    }
}
