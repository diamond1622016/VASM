using System.Linq;
using BELibrary.Core.Entity.Repositories;
using BELibrary.Core.Utils;
using BELibrary.DbContext;
using BELibrary.Entity;
using BELibrary.Utils;

namespace BELibrary.Persistence.Repositories
{
    public class AccountRepository : Repository<Account>, IAccountRepository
    {
        public AccountRepository(ScientistDbContext context)
            : base(context)
        {
        }

        public Account ValidBEAccount(string username, string password)
        {
            var db = ScientistDbContext;

            var passwordFactory = password + VariableExtensions.KeyCrypto;
            var passwordCrypto = CryptorEngine.Encrypt(passwordFactory, true);

            var account =
                  db.Accounts.FirstOrDefault(x =>
                  x.Role == 1
                  && x.UserName.ToLower() == username.ToLower()
                  && x.Password == passwordCrypto);

            return account;
        }

        public Account GetAccountByUsername(string username)
        {
            //need implement ,bad
            var db = ScientistDbContext;
            var account =
                  db.Accounts.FirstOrDefault(x =>
                  x.Role == 1
                  && x.UserName.ToLower() == username.ToLower());

            return account;
        }

        public Account GetAccountFeByUsername(string username)
        {
            var db = ScientistDbContext;
            var account =
                db.Accounts.FirstOrDefault(x =>
                    x.Role == RoleKey.Client
                    && x.UserName.ToLower() == username.ToLower());

            return account;
        }

        public Account ValidFeAccount(string username, string password)
        {
            var db = ScientistDbContext;

            var passwordFactory = password + VariableExtensions.KeyCryptorClient;
            var passwordCrypto = CryptorEngine.Encrypt(passwordFactory, true);

            var account =
                db.Accounts.FirstOrDefault(x =>
                    x.Role == RoleKey.Client
                    && x.UserName.ToLower() == username.ToLower()
                    && x.Password == passwordCrypto);

            return account;
        }

        public ScientistDbContext ScientistDbContext => Context;
    }
}