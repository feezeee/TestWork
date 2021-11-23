using System.Collections.Generic;

namespace App.BLL.DTO
{
    public class CompanyDTO
    {
        public int Id { get; set; }

        public string Name { get; set; }


        public int FormTypeId { get; set; }

        public FormTypeDTO FormType { get; set; }

        public virtual List<WorkerDTO> Workers { get; set; }

        public CompanyDTO()
        {
            Workers = new List<WorkerDTO>();
        }
    }
}
