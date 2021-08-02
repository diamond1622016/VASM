namespace BELibrary.DbContext
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;
    using BELibrary.Entity;

    public partial class ScientistDbContext : DbContext
    {
        public ScientistDbContext()
            : base("name=ScientistDbContext")
        {
            this.Configuration.LazyLoadingEnabled = false;
        }

        public ScientistDbContext(string connectString) : base(connectString)
        {
        }

        public static ScientistDbContext Create()
        {
            return new ScientistDbContext();
        }

        public virtual DbSet<Account> Accounts { get; set; }
        public virtual DbSet<Scientist> Scientists { get; set; }
        public virtual DbSet<Organization> Organization { get; set; } // Buoc 2 sau tao Entity
        public virtual DbSet<Paper> Paper { get; set; } // Buoc 2 sau tao Entity
    }
}