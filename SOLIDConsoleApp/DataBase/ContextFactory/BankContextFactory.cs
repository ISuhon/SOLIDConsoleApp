using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SOLIDConsoleApp.DataBase.ContextFactory
{
    internal class BankContextFactory
    {
        public BankContext CreateDBContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<BankContext>();

            IConfigurationRoot config = Configurations.GetConfigurationBuilder().Build();

            string? connectionString = config.GetConnectionString("BankContext");
            optionsBuilder.UseSqlServer(connectionString);
            return new BankContext(optionsBuilder.Options);
        }
    }
}
