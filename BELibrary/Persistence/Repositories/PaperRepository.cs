using BELibrary.Core.Entity.Repositories;
using BELibrary.DbContext;
using BELibrary.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BELibrary.Persistence.Repositories
{
    class PaperRepository:Repository<Paper>, IPaperRepository
    {
        public PaperRepository(ScientistDbContext context)
            : base(context)
        {
        }
        public ScientistDbContext ScientistDbContext
        {
            get { return Context as ScientistDbContext; }
        }

    }
}
