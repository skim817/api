using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;

namespace PlanYourDate.Model
{
    public class MapperProfile : Profile
    {
        public MapperProfile()
        {
            CreateMap<Places, PlaceDTO>();
            CreateMap<PlaceDTO, Places>();
        }
    }
}
