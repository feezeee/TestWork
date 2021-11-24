using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace TestWork.Models
{

    public class WorkerViewModel
    {

        public int Id { get; set; }


        [StringLength(32, ErrorMessage = "Длина строки должна быть до 32 символов")]
        [Required(ErrorMessage = "Поле должно быть установлено")]
        public string LastName { get; set; }

        
        [StringLength(32, ErrorMessage = "Длина строки должна быть до 32 символов")]
        [Required(ErrorMessage = "Поле должно быть установлено")]
        public string FirstName { get; set; }

        
        [StringLength(32, ErrorMessage = "Длина строки должна быть до 32 символов")]
        [Required(ErrorMessage = "Поле должно быть установлено")]
        public string MiddleName { get; set; }

        
        [Required(ErrorMessage = "Поле должно быть установлено")]
        public DateTime DateEmployment { get; set; }


        
        [Required(ErrorMessage = "Поле должно быть установлено")]
        public int PositionId { get; set; }
        public PositionViewModel Position { get; set; }


        
        [Required(ErrorMessage = "Поле должно быть установлено")]
        public int CompanyId { get; set; }
        public CompanyViewModel Company { get; set; }
    }
}
