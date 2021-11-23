using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace App.DAL.Models
{
    [Table("workers")]
    public class Worker
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
        public Position Position { get; set; }


        [Column("CompanyId")]
        [Required(ErrorMessage = "Поле должно быть установлено")]
        public int CompanyId { get; set; }
        public Company Company { get; set; }
    }
}
