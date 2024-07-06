using SisteCredito.ManagementAPI.Domain.Models;

namespace SisteCredito.ManagementAPI.Application.Interfaces
{
    public interface IExtraHourService
    {
        Task<IEnumerable<ExtraHour>> GetAll();
        Task<ExtraHour> GetById(int id);
        Task Add(ExtraHour employee);
        Task Update(ExtraHour employee);
        Task Delete(int id);
        Task ApproveRequest();
    }
}
