using System;

namespace App.BLL.DTO
{
    public class WorkerDTO
    {
        public int Id { get; set; }
        public string LastName { get; set; }

        public string FirstName { get; set; }

        public string MiddleName { get; set; }

        public DateTime DateEmployment { get; set; }

        public int PositionId { get; set; }
        public PositionDTO Position { get; set; }

        public int CompanyId { get; set; }
        public CompanyDTO Company { get; set; }
    }
}
