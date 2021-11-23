using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace App.DAL.Models
{
    [Table("positions")]
    public class PositionDAL
    {
        [Column("Id")]
        public int Id { get; set; }

        [Column("Name")]
        [StringLength(32, ErrorMessage = "Длина строки должна быть до 32 символов")]
        [Required(ErrorMessage = "Поле должно быть установлено")]
        public string Name { get; set; }

        public virtual List<WorkerDAL> Workers { get; set; }

        public PositionDAL()
        {
            Workers = new List<WorkerDAL>();
        }

    }
}
