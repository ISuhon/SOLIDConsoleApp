using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;

namespace SOLIDConsoleApp.DataBase.ContextFactory
{
    internal class Configurations
    {
        private static ConfigurationBuilder builder = new ConfigurationBuilder();
        
        static Configurations()
        {
            builder.SetBasePath(Directory.GetCurrentDirectory());
            builder.AddJsonFile("appsettings.json");
        }

        public static ConfigurationBuilder GetConfigurationBuilder()
        {
            return builder;
        }
    }
}
