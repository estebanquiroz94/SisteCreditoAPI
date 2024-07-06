using SisteCredito.ManagementAPI.Application.Dto;

namespace SisteCredito.ManagementAPI.Application.Interfaces
{
    public interface IEmployeeService
    {
        Task<IEnumerable<EmployeeDTO>> GetAll();
        Task<EmployeeDTO> GetById(int id);
        Task Add(EmployeeDTO employee);
        Task Update(EmployeeDTO employee);
        Task Delete(int id);
        Task<bool> Homologate(IFormFile form);
    }
}
