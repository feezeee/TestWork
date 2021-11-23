using App.BLL.DTO;
using App.BLL.Interfaces;
using App.DAL.Interfaces;
using App.DAL.Models;
using System.Collections.Generic;
using System;
using App.BLL.DTOToDAL;
using App.BLL.DALToDTO;
using System.Threading.Tasks;

namespace App.BLL.Services
{
    public class PositionService : IPositionService
    {
        IUnitOfWork Database { get; set; }

        public PositionService(IUnitOfWork unitOfWork)
        {
            Database = unitOfWork;
        }

        public void AddPosition(PositionDTO item)
        {
            PositionDAL position = item.ToDAL();

            Database.Positions.Create(position);
        }

        public IEnumerable<PositionDTO> GetPositionBy(int id = 0, string name = "")
        {
            PositionDAL item = null;
            if (id != 0)
            {
                if (item == null)
                {
                    item = new PositionDAL();
                }
                item.Id = id;
            }
            if (!String.IsNullOrEmpty(name))
            {
                if (item == null)
                {
                    item = new PositionDAL();
                }
                item.Name = name;
            }
            
            var positions = Database.Positions.Find(item);

            foreach (var position in positions)
            {
                PositionDTO myPosition = position.ToDTO();

                yield return myPosition;
            }
        }

        public IEnumerable<PositionDTO> GetPositions()
        {
            var positions = Database.Positions.GetAll();
            foreach (var position in positions)
            {
                yield return position.ToDTO();
            }
        }

        public void UpdatePosition(PositionDTO item)
        {
            PositionDAL position = item.ToDAL();

            Database.Positions.Update(position);
        }

        public void DeletePosition(int id)
        {
            if (Database.Positions.Find(new PositionDAL { Id = id }) != null)
            {
                Database.Positions.Delete(id);
            }
        }

        public async Task AddPositionAsync(PositionDTO item)
        {
            await Task.Run(() => AddPosition(item));
        }

        public async Task<IEnumerable<PositionDTO>> GetPositionsAsync()
        {
            return await Task.Run(() => GetPositions());
        }

        public async Task<IEnumerable<PositionDTO>> GetPositionByAsync(int id = 0, string name = "")
        {
            return await Task.Run(() => GetPositionBy(id, name));
        }

        public async Task UpdatePositionAsync(PositionDTO item)
        {
            await Task.Run(() => UpdatePosition(item));
        }

        public async Task DeletePositionAsync(int id)
        {
            await Task.Run(() => DeletePosition(id));
        }
    }
}
