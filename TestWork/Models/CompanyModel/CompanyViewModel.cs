using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace TestWork.Models
{
    [Table("Companies")]
    public class CompanyViewModel
    {
        [Column("Id")]
        [Remote(action: "CheckId", controller: "Company", AdditionalFields = "predId", ErrorMessage = "Компания с таким индентификатором уже существует!", HttpMethod = "POST")]
        [RegularExpression(@"^[1-9]\d*$", ErrorMessage = "Некорректный индентификатор")]
        [Required(ErrorMessage = "Поле должно быть установлено")]
        public int Id { get; set; }

        [Column("Name")]
        [StringLength(32, ErrorMessage = "Длина строки должна быть до 32 символов")]
        [Required(ErrorMessage = "Поле должно быть установлено")]
        public string Name { get; set; }


        [Column("FormTypeId")]
        [Required(ErrorMessage = "Поле должно быть установлено")]
        public int FormTypeId { get; set; }

        public FormTypeViewModel FormType { get; set; }

        public virtual List<WorkerViewModel> Workers { get; set; } 

        public CompanyViewModel()
        {
            Workers = new List<WorkerViewModel>();
        }

    }
}
