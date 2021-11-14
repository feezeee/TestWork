using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace TestWork.Models
{
    [Table("FormTypes")]
    public class FormType
    {
        [Column("Id")]
        public int Id { get; set; }

        [Column("Name")]
        public string Name { get; set; }

        public virtual List<Company> Companies { get; set; }

        public FormType()
        {
            Companies = new List<Company>();
        }
    }
}
