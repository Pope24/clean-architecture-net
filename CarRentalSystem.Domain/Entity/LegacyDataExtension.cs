using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Crs.Domain.Entity
{
    public class LegacyDataExtension
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Data { get; set; }
    }
}
