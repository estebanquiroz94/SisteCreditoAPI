using AutoMapper;
using MathNet.Numerics.Distributions;
using SisteCredito.ManagementAPI.Application.Dto;
using SisteCredito.ManagementAPI.Domain.Models;

namespace SisteCredito.ManagementAPI.Application
{
    public class MapperProfile : Profile
    {
        public MapperProfile() {
            CreateMap<Employee, EmployeeDTO>().ReverseMap();
            CreateMap<Area, AreaDTO>().ReverseMap();
                   
        }

    }
}
