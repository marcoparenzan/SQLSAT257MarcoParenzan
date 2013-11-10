using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SQLSAT257DataViewModels
{
    public partial class CRMDataViewModel: DbContext
    {
        static CRMDataViewModel()
        {
            Database.SetInitializer<CRMDataViewModel>(null);
        }

        public DbSet<ActivityViewModel> Activities { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            // http://entityframework.codeplex.com/wikipage?title=Code%20First%20Insert%2FUpdate%2FDelete%20Stored%20Procedure%20Mapping
            modelBuilder
                .Entity<ActivityViewModel>()
                .HasKey(xx => xx.ActivityId)
                .ToTable("crm.ActivityViewModels")
                .MapToStoredProcedures(s => {
                    s
                    .Insert(i => 
                        i
                        .HasName("crm.InsertActivity")
                        //.Parameter(xx => xx.CustomerFirstName, "CustomerFirstName")
                        .Result(b => b.ActivityId, "ActivityId")
                    )
                    .Update(u => 
                        u
                        .HasName("crm.UpdateActivity")
                    )
                    .Delete(d => 
                        d
                        .HasName("crm.DeleteActivity")
                    );            
            });
        }
    }
}
