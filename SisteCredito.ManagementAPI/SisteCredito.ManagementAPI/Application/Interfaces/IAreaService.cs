using SisteCredito.ManagementAPI.Domain.Models;

namespace SisteCredito.ManagementAPI.Application.Interfaces
{
    public interface IAreaService
    {
        Task<IEnumerable<Area>> GetAll();
        Task<Area> GetById(int id);
        Task Add(Area area);
        Task Update(Area area);
        Task Delete(int id);
        Task<bool> Homologate(IFormFile file);

    }
}
