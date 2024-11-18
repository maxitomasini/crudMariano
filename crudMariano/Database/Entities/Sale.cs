using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace crudMariano.Database.Entities
{
    public class Sale
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public DateTime Date {  get; set; }
        public double Total { get; set; }
    }
}
