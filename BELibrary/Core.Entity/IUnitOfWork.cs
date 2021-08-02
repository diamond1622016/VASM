using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using BELibrary.Core.Entity.Repositories;

namespace BELibrary.Core.Entity
{
    public interface IUnitOfWork : IDisposable
    {
        IAccountRepository Accounts { get; }
        IScientistRepository Scientists { get; }
         
        int Complete();
    }
}