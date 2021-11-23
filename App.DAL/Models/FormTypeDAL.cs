
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace App.DAL.Models
{
    [Table("FormTypes")]
    public class FormTypeDAL
    {
        [Column("Id")]
        public int Id { get; set; }

        [Column("Name")]
        [Required(ErrorMessage = "Поле должно быть установлено")]
        [StringLength(32, ErrorMessage = "Длина строки должна быть до 32 символов")]
        public string Name { get; set; }

        public virtual List<CompanyDAL> Companies { get; set; }

        public FormTypeDAL()
        {
            Companies = new List<CompanyDAL>();
        }
    }
}
