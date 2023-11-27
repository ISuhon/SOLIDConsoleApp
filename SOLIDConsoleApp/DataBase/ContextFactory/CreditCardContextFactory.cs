using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SOLIDConsoleApp.DataBase.ContextFactory
{
    internal class CreditCardContextFactory
    {
        public CreditCardContext CreateDBContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<CreditCardContext>();

            IConfigurationRoot config = Configurations.GetConfigurationBuilder().Build();

            string? connectionString = config.GetConnectionString("CreditCardContext");
            optionsBuilder.UseSqlServer(connectionString);
            return new CreditCardContext(optionsBuilder.Options);
        }
    }
}
