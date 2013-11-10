using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SQLSAT257DataModels
{
    public partial class CRMDataModel
    {
        static CRMDataModel()
        {
            Database.SetInitializer<CRMDataModel>(null);
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Customer>().ToTable("crm.Customers");
            modelBuilder.Entity<ActivityType>().ToTable("crm.ActivityTypes");
            modelBuilder.Entity<Activity>().ToTable("crm.Activities");
        }
    }
}
