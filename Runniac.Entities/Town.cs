using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Runniac.Data
{
    public class Town : Entity
    {
        public string Name { get; set; }

        public string PostalCode { get; set; }

        public decimal Latitude { get; set; }

        public decimal Longitude { get; set; }

        public Province Province { get; set; }

        [ForeignKey("Province")]
        public long ProvinceId { get; set; }
    }
}
