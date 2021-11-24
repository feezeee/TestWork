using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace TestWork.Models
{
    
    public class FormTypeViewModel
    {
        
        public int Id { get; set; }

        
        [Required(ErrorMessage = "Поле должно быть установлено")]
        [StringLength(32, ErrorMessage = "Длина строки должна быть до 32 символов")]
        [Remote(action: "CheckFormType", controller: "FormType", AdditionalFields = "Id", ErrorMessage = "ОПФ с таким наименованием уже существует!", HttpMethod = "POST")]
        public string Name { get; set; }

        public virtual List<CompanyViewModel> Companies { get; set; }

        public FormTypeViewModel()
        {
            Companies = new List<CompanyViewModel>();
        }
    }
}
