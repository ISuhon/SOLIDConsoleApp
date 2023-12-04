using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity.ModelConfiguration;
using SOLIDConsoleApp.Client;

namespace SOLIDConsoleApp.DataBase.Mapping
{
    internal class ClientMap //: EntityTypeConfiguration<ClientData>
    {
        public ClientMap()
        {
            //this.HasKey(x => x.Id);

            //this.ToTable("Clients");
            //this.Property(x => x.Id).HasColumnName("Id");
            //this.Property(x => x.FirstName).HasColumnName("FirstName");
            //this.Property(x => x.MiddleName).HasColumnName("MiddleName");
            //this.Property(x => x.LastName).HasColumnName("LastName");
            //this.Property(x => x.PhoneNumber).HasColumnName("PhoneNumber");
            //this.Property(x => x.Email).HasColumnName("Id");
        }
    }
}
