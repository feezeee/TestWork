using App.BLL.DTO;
using System.Collections.Generic;
using TestWork.DTOToViewModels;
using TestWork.Models;

namespace TestWork.IEnumerableExtension
{
    public static class IEnumerableExtension
    {
        #region GetFirst from IEnumberable<TDTO>

        public static WorkerDTO GetFirst (this IEnumerable<WorkerDTO> ts)
        {
            foreach(var t in ts)
            {
                return t;
            }
            return null;
        }
        public static PositionDTO GetFirst(this IEnumerable<PositionDTO> ts)
        {
            foreach (var t in ts)
            {
                return t;
            }
            return null;
        }
        public static CompanyDTO GetFirst(this IEnumerable<CompanyDTO> ts)
        {
            foreach (var t in ts)
            {
                return t;
            }
            return null;
        }
        public static FormTypeDTO GetFirst(this IEnumerable<FormTypeDTO> ts)
        {
            foreach (var t in ts)
            {
                return t;
            }
            return null;
        }
        #endregion

        #region Convert IEnumberable<TDTO> To List<TViewModel>
        public static List<WorkerViewModel> ToList(this IEnumerable<WorkerDTO> ts)
        {
            var workers = new List<WorkerViewModel>();
            foreach(var worker in ts)
            {
                workers.Add(worker.ToViewModel());
            }
            return workers;
        }

        public static List<PositionViewModel> ToList(this IEnumerable<PositionDTO> ts)
        {
            var positions = new List<PositionViewModel>();
            foreach (var position in ts)
            {
                positions.Add(position.ToViewModel());
            }
            return positions;
        }

        public static List<CompanyViewModel> ToList(this IEnumerable<CompanyDTO> ts)
        {
            var companies = new List<CompanyViewModel>();
            foreach (var company in ts)
            {
                companies.Add(company.ToViewModel());
            }
            return companies;
        }

        public static List<FormTypeViewModel> ToList(this IEnumerable<FormTypeDTO> ts)
        {
            var formTypes = new List<FormTypeViewModel>();
            foreach (var form in ts)
            {
                formTypes.Add(form.ToViewModel());
            }
            return formTypes;
        }
        #endregion

    
    }
}
