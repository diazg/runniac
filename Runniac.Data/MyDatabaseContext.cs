using Runniac.Data;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Runniac.Data
{
    public class MyDatabaseContext: DbContext
    {
        public IDbSet<Event> Events { get; set; }
        public IDbSet<Comment> Comments { get; set; }
        public IDbSet<Photo> Photos { get; set; }
        public IDbSet<Vote> Votes { get; set; }
        public IDbSet<Province> Provinces { get; set; }
        public IDbSet<Town> Towns { get; set; }

        public MyDatabaseContext()
            : base("RunniacConnection")
        {
            
        }        
       
    }
}
