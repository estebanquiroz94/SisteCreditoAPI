using AutoMapper;
using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;
using SisteCredito.ManagementAPI.Application.Dto;
using SisteCredito.ManagementAPI.Application.Interfaces;
using SisteCredito.ManagementAPI.Domain.Models;
using System.Reflection.Metadata.Ecma335;
using static SisteCredito.ManagementAPI.Domain.Interfaces.IRepository;

namespace SisteCredito.ManagementAPI.Application.Services
{
    public class EmployeeService : IEmployeeService
    {
        private readonly IRepository<Employee> _repository;
        private readonly IMapper _mapper;

        public EmployeeService(IRepository<Employee> repository, IMapper mapper)
        {
            _repository = repository;
            this._mapper = mapper;
        }

        public async Task<IEnumerable<EmployeeDTO>> GetAll()
        {
            var employees = await _repository.GetAllAsync();
            return _mapper.Map<List<EmployeeDTO>>(employees);
        }

        public async Task<EmployeeDTO> GetById(int id)
        {
            var employee = await _repository.GetByIdAsync(id);
            return _mapper.Map<EmployeeDTO>(employee);
        }
        public async Task<EmployeeDTO> GetEmployeeByDocumentsAsync(List<int> documents)
        {
            var employee = await _repository.GetByDocumentsAsync(documents);
            return _mapper.Map<EmployeeDTO>(employee);
        }
        public async Task Add(EmployeeDTO employee)
        {

            await _repository.AddAsync(_mapper.Map<Employee>(employee));
        }

        public async Task Update(EmployeeDTO employee)
        {
            await _repository.UpdateAsync(_mapper.Map<Employee>(employee));
        }

        public async Task Delete(int id)
        {
            await _repository.DeleteAsync(id);
        }

        public async Task<bool> Homologate(IFormFile file)
        {
            if (file == null || file.Length == 0)
            {
                return false;
            }

            using (var stream = new MemoryStream())
            {
                await file.CopyToAsync(stream);
                stream.Position = 0;

                IWorkbook workbook = new XSSFWorkbook(stream);
                ISheet sheet = workbook.GetSheetAt(1);

                var headerRow = sheet.GetRow(0);
                int cellCount = headerRow.LastCellNum;

                var processEmployee = new List<EmployeeDTO>();
                var employeesToUpdate = new List<Employee>();

                await ProcessFile(sheet, headerRow, cellCount, processEmployee, employeesToUpdate);

                foreach (var item in processEmployee.Where(x => x.SupervisorDocument != null))
                {
                    var idSupervisor = employeesToUpdate.Where(x => x.Document == item.SupervisorDocument).FirstOrDefault().Id;
                    var employee = employeesToUpdate.Where(x => x.Document == item.Document).FirstOrDefault();

                    if (employee != null)
                    {
                        employee.IdSupervisor = idSupervisor;
                        await _repository.UpdateAsync(employee);
                    }
                }

            }
            return true;
        }

        private async Task ProcessFile(ISheet sheet, IRow headerRow, int cellCount, List<EmployeeDTO> employeesDTO, List<Employee> employeesToUpdate)
        {
            for (int i = 1; i <= sheet.LastRowNum; i++)
            {
                IRow row = sheet.GetRow(i);

                if (row != null)
                {
                    var employeeDTO = new EmployeeDTO();
                    for (int j = 0; j < cellCount; j++)
                    {
                        if (row.GetCell(j) != null)
                        {
                            SetEmployeeFields(headerRow, row, employeeDTO, j);
                        }
                    }
                    var employee = _mapper.Map<Employee>(employeeDTO);
                    await _repository.AddAsync(employee);


                    employeesDTO.Add(employeeDTO);
                    employeesToUpdate.Add(employee);
                }
                else { break; }
            }
        }

        private static void SetEmployeeFields(IRow headerRow, IRow row, EmployeeDTO employee, int j)
        {
            var currentValue = row.GetCell(j).ToString();
            if (!string.IsNullOrEmpty(currentValue))
            {
                employee.IdSupervisor = null;
                switch (headerRow.GetCell(j).ToString())
                {
                    case "DocumentoLider":
                        employee.SupervisorDocument = currentValue.ToString();
                        break;

                    case "Cargo":
                        employee.IdCharge = int.Parse(currentValue.Split("-")[0]);
                        break;

                    case "Nombre":
                        employee.Name = currentValue;
                        break;

                    case "Documento":
                        employee.Document = currentValue;
                        break;

                    case "Telefono":
                        employee.Phone = currentValue;
                        break;

                    default:
                        break;
                }
            }
        }
    }
}
