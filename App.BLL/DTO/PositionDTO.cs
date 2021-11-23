using System.Collections.Generic;

namespace App.BLL.DTO
{
    public class PositionDTO
    {
        public int Id { get; set; }
        
        public string Name { get; set; }

        public virtual List<WorkerDTO> Workers { get; set; }

        public PositionDTO()
        {
            Workers = new List<WorkerDTO>();
        }
    }
}
