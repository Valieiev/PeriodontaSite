using AutoMapper;
using PeriodontalSite1.Models;
using PeriodontalSite1.Models.Users;
using PeriodontalSite1.ViewModel;
using PeriodontalSite1.ViewModel.Admin;
using PeriodontalSite1.ViewModel.Appointment;
using PeriodontalSite1.ViewModel.AppointmentsResult;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PeriodontalSite1.AutoMapper.MapProfilers
{
    public class AppointmentMapProfile : Profile
    {
        public AppointmentMapProfile()
        {
            
                
            CreateMap<ApplicationUser, AdminViewModel>()
                .ReverseMap();

            CreateMap<ApplicationUser, AccountEditViewModel>()
                .ReverseMap();

            CreateMap<ApplicationUser, AdminEditViewModel>()
                //.ForMember(dest => dest.TypeUser, opt => opt.MapFrom(src => src.TypeUser))
                .ForMember(dest => dest.Roles, opt => opt.Ignore())
                .ReverseMap();


            CreateMap<Patients, PatientsViewModel>()
                .ReverseMap();

            CreateMap<Appointments, AppointmentsViewModel>()
                .ReverseMap();


            CreateMap<Appointments, AppointmentCreateViewModel>()
                .ForMember(x => x.Users, x => x.MapFrom(m => m.UserId))
                .ForMember(x => x.Patients, x => x.MapFrom(m => m.PatientId))
                .ReverseMap();


            CreateMap<AppointmentResult, AppointmentResultViewModel>()
                .ReverseMap();

            CreateMap<AppointmentResult, AppointmentResultCreateViewModel>()
               .ForMember(x => x.Price, x => x.MapFrom(m => m.PriceId))
               .ForMember(x => x.Appoitment, x => x.MapFrom(m => m.AppoitmentId))
               .ReverseMap();
        }
    }
}