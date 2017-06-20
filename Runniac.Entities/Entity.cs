using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Runniac.Data
{
    public class Entity : IEntity
    {
        [Key]        
        public long Id { get; set; }
    }
}
