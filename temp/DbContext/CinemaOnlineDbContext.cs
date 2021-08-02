namespace BELibrary.DbContext
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;
    using BELibrary.Entity;

    public partial class CinemaOnlineDbContext : DbContext
    {
        public CinemaOnlineDbContext()
            : base("name=CinemaOnlineDbContext")
        {
            this.Configuration.LazyLoadingEnabled = false;
        }

        public CinemaOnlineDbContext(string connectString) : base(connectString)
        {
        }

        public static CinemaOnlineDbContext Create()
        {
            return new CinemaOnlineDbContext();
        }

        public virtual DbSet<Admin> Admins { get; set; }
      

        public virtual DbSet<Role> Roles { get; set; }
 
        public virtual DbSet<User> Users { get; set; }
         
    }
}