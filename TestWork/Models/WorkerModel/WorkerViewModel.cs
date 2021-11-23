using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace TestWork.Models
{
    [Table("workers")]
    public class WorkerViewModel
    {
        [Column("Id")]
        public int Id { get; set; }

        [Column("LastName")]
        [StringLength(32, ErrorMessage = "Длина строки должна быть до 32 символов")]
        [Required(ErrorMessage = "Поле должно быть установлено")]
        public string LastName { get; set; }

        [Column("FirstName")]
        [StringLength(32, ErrorMessage = "Длина строки должна быть до 32 символов")]
        [Required(ErrorMessage = "Поле должно быть установлено")]
        public string FirstName { get; set; }

        [Column("MiddleName")]
        [StringLength(32, ErrorMessage = "Длина строки должна быть до 32 символов")]
        [Required(ErrorMessage = "Поле должно быть установлено")]
        public string MiddleName { get; set; }

        [Column("DateEmployment")]
        [Required(ErrorMessage = "Поле должно быть установлено")]
        public DateTime DateEmployment { get; set; }


        [Column("PositionId")]
        [Required(ErrorMessage = "Поле должно быть установлено")]
        public int PositionId { get; set; }
        public PositionViewModel Position { get; set; }


        [Column("CompanyId")]
        [Required(ErrorMessage = "Поле должно быть установлено")]
        public int CompanyId { get; set; }
        public CompanyViewModel Company { get; set; }
    }
}
