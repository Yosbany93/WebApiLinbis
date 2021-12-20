using AutoMapper;
using Services.DTOs;
using Services.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Services.Utilities
{
    public class AutoMapperProfiles : Profile
    {
        public AutoMapperProfiles()
        {
            CreateMap<ProjectCreationDTO, Project>();
            CreateMap<DeveloperCreationDTO, Developer>();
        }
    }
}
