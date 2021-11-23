using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace App.DAL.Models
{
    [Table("Companies")]
    public class Company
    {
        [Column("Id")]
        [RegularExpression(@"\d*", ErrorMessage = "Некорректный индентификатор")]
        [Required(ErrorMessage = "Поле должно быть установлено")]
        public int Id { get; set; }

        [Column("Name")]
        [StringLength(32, ErrorMessage = "Длина строки должна быть до 32 символов")]
        [Required(ErrorMessage = "Поле должно быть установлено")]
        public string Name { get; set; }


        [Column("FormTypeId")]
        [Required(ErrorMessage = "Поле должно быть установлено")]
        public int FormTypeId { get; set; }

        public FormType FormType { get; set; }

        public virtual List<Worker> Workers { get; set; } 

        public Company()
        {
            Workers = new List<Worker>();
        }

    }
}
