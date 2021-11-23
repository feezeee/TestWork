using System.Collections.Generic;

namespace App.BLL.DTO
{
    public class FormTypeDTO
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public virtual List<CompanyDTO> Companies { get; set; }

        public FormTypeDTO()
        {
            Companies = new List<CompanyDTO>();
        }
    }
}
