using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace TestWork.Models
{
    [Table("positions")]
    public class Position
    {
        [Column("Id")]
        public int Id { get; set; }

        [Column("Name")]
        public string Name { get; set; }

        public virtual List<Worker> Workers { get; set; }

        public Position()
        {
            Workers = new List<Worker>();
        }

    }
}
