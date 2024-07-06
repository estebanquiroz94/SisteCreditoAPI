using SisteCredito.ManagementAPI.Application.Interfaces;
using SisteCredito.ManagementAPI.Domain.Models;
using static SisteCredito.ManagementAPI.Domain.Interfaces.IRepository;

namespace SisteCredito.ManagementAPI.Application.Services
{
    public class ExtraHourService : IExtraHourService
    {
        private readonly IRepository<ExtraHour> _repository;

        public ExtraHourService(IRepository<ExtraHour> repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<ExtraHour>> GetAll()
        {
            return await _repository.GetAll();
        }

        public async Task<ExtraHour> GetById(int id)
        {
            return await _repository.GetById(id);
        }

        public async Task Add(ExtraHour extraHour)
        {
            //Validar Cantidad de horas en el mes
            var canRegister = await ValidateMonthHours(extraHour.IdEmployee, extraHour.QuantityHours);
            await _repository.Add(extraHour);
        }

        public async Task Update(ExtraHour extraHour)
        {
            await _repository.Update(extraHour);
        }

        public async Task Delete(int id)
        {
            await _repository.Delete(id);
        }

        public Task ApproveRequest()
        {
            throw new NotImplementedException();
        }

        private async Task<bool> ValidateMonthHours(int idEmployee, int quantityHour)
        {
            //Se valida que la cantidad de horas no supere las 40 mensuales
            return true;
        }
    }
}
