using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;

namespace Poprigun.ModelEF
{
    public partial class Model1 : DbContext
    {
        public Model1()
            : base("name=Model1")
        {
        }

        public virtual DbSet<Agent> Agent { get; set; }
        public virtual DbSet<AgentType> AgentType { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Agent>()
                .Property(e => e.INN)
                .IsUnicode(false);

            modelBuilder.Entity<Agent>()
                .Property(e => e.KPP)
                .IsUnicode(false);

            modelBuilder.Entity<AgentType>()
                .HasMany(e => e.Agent)
                .WithRequired(e => e.AgentType)
                .WillCascadeOnDelete(false);
        }
    }
}
