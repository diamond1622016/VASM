using BELibrary.Core.Entity.Repositories;
using BELibrary.DbContext;
using BELibrary.Persistence.Repositories;

namespace BELibrary.Core.Entity
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly CinemaOnlineDbContext _context;

        public UnitOfWork(CinemaOnlineDbContext context)
        {
            _context = context;
            Admins = new AdminRepository(_context);  
            Roles = new RoleRepository(_context); 
            Users = new UserRepository(_context); 
        }

        public IAdminRepository Admins { get; private set; }
 
        public IRoleRepository Roles { get; private set; }
     

        public IUserRepository Users { get; private set; }

      

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