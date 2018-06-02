using AutoMapper;
using PeriodontalSite1.Models;
using PeriodontalSite1.ViewModel;
using PeriodontalSite1.ViewModel.Price;
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
               .ForMember(dest => dest.Types, opt => opt.Ignore())
               .ForMember(dest => dest.Units, opt => opt.Ignore())
               .ReverseMap()
               .ForMember(dest => dest.TypeId, opt => opt.MapFrom(src => src.TypeId))
               .ForMember(dest => dest.UnitId, opt => opt.MapFrom(src => src.UnitId))
               .ForMember(dest => dest.Types, opt => opt.Ignore())
               .ForMember(dest => dest.Units, opt => opt.Ignore());

            CreateMap<ResultEdit, PriceEditViewModel>()
                .ForMember(x=>x.Price, x => x.MapFrom(m=>m.Price));

            CreateMap<Units, UnitsViewModel>()
                .ReverseMap();


            CreateMap<TypeServices, TypeServicesViewModel>()
            .ReverseMap();

            CreateMap<Prices, PriceViewModel>()
                .ReverseMap();

            CreateMap<Prices, PriceCreateViewModel>()
               .ForMember(x => x.Service, x => x.MapFrom(m => m.ServicesId))
               .ReverseMap()
               .ForMember(dest => dest.ServicesId, opt => opt.MapFrom(src => src.ServiceSelected))
               .ForMember(x => x.Services, x => x.Ignore());
        }
    }
}