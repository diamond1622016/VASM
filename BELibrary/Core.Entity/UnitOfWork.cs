using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using BELibrary.Core.Entity.Repositories;
using BELibrary.DbContext;
using BELibrary.Entity;
using BELibrary.Persistence.Repositories;

namespace BELibrary.Core.Entity
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ScientistDbContext _context;

        public UnitOfWork(ScientistDbContext context)
        {
            _context = context;
            Accounts = new AccountRepository(_context);
            Scientists = new ScientistRepository(_context);
            Organization = new OrganizationRepository(_context);
            Paper = new PaperRepository(_context);
        }

        public IAccountRepository Accounts { get; private set; }
        public IScientistRepository Scientists { get; private set; }
        public IOrganizationRepository Organization { get; private set; }
        public IPaperRepository Paper { get; private set; }
        public int Complete()
        {
            return _context.SaveChanges();
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}