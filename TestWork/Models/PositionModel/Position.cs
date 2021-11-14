using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
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
        [StringLength(32, ErrorMessage = "Длина строки должна быть до 32 символов")]
        [Required(ErrorMessage = "Поле должно быть установлено")]
        [Remote(action: "CheckPosition", controller: "Position", AdditionalFields = "Id", ErrorMessage = "Должность с таким наименованием уже существует!", HttpMethod = "POST")]
        public string Name { get; set; }

        public virtual List<Worker> Workers { get; set; }

        public Position()
        {
            Workers = new List<Worker>();
        }

    }
}
