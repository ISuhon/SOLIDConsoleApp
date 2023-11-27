using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SOLIDConsoleApp.DataBase.ContextFactory
{
    internal class ClientContextFactory
    {
        public ClientContext CreateDBContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<ClientContext>();

            IConfigurationRoot config = Configurations.GetConfigurationBuilder().Build();

            string? connectionString = config.GetConnectionString("ClientContext");
            optionsBuilder.UseSqlServer(connectionString);
            return new ClientContext(optionsBuilder.Options);
        }
    }
}
