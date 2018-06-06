using OutageManagementSystem.Common.Models;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;

namespace OutageManagementSystem.Service.Contexts
{
    class DatabaseContext : DbContext
    {
        public DatabaseContext() : base("name=OutageManagementSystemSQL")
        {
            Configuration.ProxyCreationEnabled = false;
        }

        public virtual DbSet<Outage> Outages { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Outage>()
                .HasMany(o => o.Actions)
                .WithRequired()
                .HasForeignKey(a => a.OutageId);

            modelBuilder.Entity<ExecutedAction>()
                .HasKey(a => new { a.Id, a.OutageId })
                .Property(a => a.Id)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
        }
    }
}
