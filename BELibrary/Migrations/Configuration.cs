using BELibrary.Core.Utils;

namespace BELibrary.Migrations
{
    using BELibrary.DbContext;
    using BELibrary.Entity;
    using BELibrary.Utils;
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Diagnostics;

    internal sealed class Configuration : DbMigrationsConfiguration<ScientistDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }
        protected override void Seed(ScientistDbContext context)
        {
            //Role role = new Role
            //{
            //    Id = Guid.NewGuid(),
            //    RoleEnum = RoleKey.Admin,
            //    Name = RoleKey.GetRole(RoleKey.Admin)
            //};
            //context.Roles.AddOrUpdate(c => c.RoleEnum, role);

            //context.SaveChanges();
            //var passwordFactory = VariableExtensions.DefautlPassword + VariableExtensions.KeyCrypto;
            //var passwordCrypto = CryptorEngine.Encrypt(passwordFactory, true);
            //context.Accounts.AddOrUpdate(c => c.UserName, new Account()
            //{
            //    Id = Guid.NewGuid(),
            //    FullName = "Admin",
            //    Phone = "0973 642 032",
            //    UserName = "admin",
            //    Password = passwordCrypto,
            //    Role = 1,
            //    Gender = true,
            //    LinkAvatar = ""
            //}); 
            
        }
    }
}