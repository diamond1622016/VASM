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

    internal sealed class Configuration : DbMigrationsConfiguration<CinemaOnlineDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(CinemaOnlineDbContext context)
        {
            Role role = new Role
            {
                Id = Guid.NewGuid(),
                RoleEnum = RoleKey.Admin,
                Name = RoleKey.GetRole(RoleKey.Admin)
            };
            context.Roles.AddOrUpdate(c => c.RoleEnum, role);

            context.SaveChanges();
            string passwordFactory = VariableExtensions.DefautlPassword + VariableExtensions.KeyCryptor;
            string passwordCryptor = CryptorEngine.Encrypt(passwordFactory, true);
            context.Admins.AddOrUpdate(c => c.UserName, new Admin()
            {
                Id = Guid.NewGuid(),
                Name = "Quản Trị",
                PhoneNumber = "0973 011 012",
                UserName = "admin",
                Password = passwordCryptor,
                RoleId = role.Id,
                IsDelete = false,
                DeletetionTime = null,
                DeleterId = null
            });
        }
    }
}