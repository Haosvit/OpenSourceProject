namespace OpenSourceProject.OpenSource
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class VerifyDbContext : DbContext
    {
        public VerifyDbContext()
            : base("VerifyDbContext")
        {
                       
            Database.CreateIfNotExists();
        }

        public virtual DbSet<Verification> Verifications { get; set; }
        public virtual DbSet<User> Users { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
        }


        
    }
}
