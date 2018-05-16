using AutoMapper;
using PeriodontalSite1.Models;
using PeriodontalSite1.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PeriodontalSite1.AutoMapper.MapProfilers
{
    public class PriceMapProfiler : Profile
    {
        public PriceMapProfiler()
        {
            CreateMap<Services, ServicesViewModel>()
                .ReverseMap();

            CreateMap<Services, ServicesCreateViewModel>()
               .ForMember(x => x.Types, x => x.MapFrom(m => m.TypeId))
               .ForMember(x => x.Units, x => x.MapFrom(m => m.UnitId))
               .ReverseMap();

            CreateMap<Services, ServicesEditViewModel>()
               .ForMember(x => x.Types, x => x.MapFrom(m => m.TypeId))
               .ForMember(x => x.Units, x => x.MapFrom(m => m.UnitId))
               .ReverseMap();


            CreateMap<Units, UnitsViewModel>()
                .ReverseMap();


            CreateMap<TypeServices, TypeServicesViewModel>()
            .ReverseMap();

            CreateMap<Prices, PriceViewModel>()
                .ReverseMap();

            CreateMap<Prices, PriceCreateViewModel>()
               .ForMember(x => x.Service, x => x.MapFrom(m => m.ServiceId))
               .ReverseMap();
        }
    }
}