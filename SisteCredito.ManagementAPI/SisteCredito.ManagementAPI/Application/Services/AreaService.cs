using AutoMapper;
using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;
using SisteCredito.ManagementAPI.Application.Dto;
using SisteCredito.ManagementAPI.Application.Interfaces;
using SisteCredito.ManagementAPI.Domain.Models;
using static SisteCredito.ManagementAPI.Domain.Interfaces.IRepository;

namespace SisteCredito.ManagementAPI.Application.Services
{
    public class AreaService : IAreaService
    {
       
        private readonly IRepository<Area> _repository;
        private readonly IRepository<Employee> _repositoryEmployee;
        private readonly IMapper _mapper;

        public AreaService(IRepository<Area> repository, IRepository<Employee> repositoryEmployee, IMapper mapper)
        {
            _repository = repository;
            _repositoryEmployee = repositoryEmployee;
            _mapper = mapper;
        }

        public async Task<IEnumerable<Area>> GetAll()
        {
            return await _repository.GetAll();
        }

        public async Task<Area> GetById(int id)
        {
            return await _repository.GetById(id);
        }

        public async Task Add(Area area)
        {
            await _repository.Add(area);
        }

        public async Task Update(Area area)
        {
            await _repository.Update(area);
        }

        public async Task Delete(int id)
        {
            await _repository.Delete(id);
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
                ISheet sheet = workbook.GetSheetAt(0);

                var headerRow = sheet.GetRow(0);
                int cellCount = headerRow.LastCellNum;

                var processArea = new List<EmployeeDTO>();

                await ProcessFile(sheet, headerRow, cellCount, processArea);


            }
            return true;
        }
        private async Task ProcessFile(ISheet sheet, IRow headerRow, int cellCount, List<EmployeeDTO> employeesDTO)
        {
            for (int i = 1; i <= sheet.LastRowNum; i++)
            {
                IRow row = sheet.GetRow(i);

                if (row != null)
                {
                    var areasDTO = new AreaDTO();
                    for (int j = 0; j < cellCount; j++)
                    {
                        if (row.GetCell(j) != null)
                        {
                            SetAreaFields(headerRow, row, areasDTO, j);
                        }
                    }
                    var employees = await _repositoryEmployee.GetAll();

                    areasDTO.IdHumangestor = employees.Where(x => x.Document == areasDTO.HumanGestorDocument).Select(x => x.Id).FirstOrDefault();
                    areasDTO.IdSupervisor = employees.Where(x => x.Document == areasDTO.SupervisorDocument).Select(x => x.Id).FirstOrDefault();

                    var area = _mapper.Map<Area>(areasDTO);
                    await _repository.Add(area);
                }
                else { break; }
            }
        }
        private static void SetAreaFields(IRow headerRow, IRow row, AreaDTO area, int j)
        {
            var currentValue = row.GetCell(j).ToString();
            if (!string.IsNullOrEmpty(currentValue))
            {
                area.IdHumangestor = null;
                area.IdSupervisor = null;
                switch (headerRow.GetCell(j).ToString())
                {
                    case "DocumentoGestor":
                        area.HumanGestorDocument = currentValue;
                        break;

                    case "DocumentoLider":
                        area.SupervisorDocument = currentValue;
                        break;

                    case "Codigo":
                        area.Code = currentValue;
                        break;

                    case "Nombre":
                        area.Name = currentValue;
                        break;

                    default:
                        break;
                }
            }
        }
    }
}
