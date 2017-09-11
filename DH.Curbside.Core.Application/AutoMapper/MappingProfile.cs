using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using DH.Curbside.Core.Domain;
using DH.Curbside.Core.Application.PatientCase.ViewModels;
using DH.Curbside.Core.Application.WhitelistDomain.ViewModels;
using DH.Curbside.Core.Application.WhitelistUser.ViewModels;

namespace DH.Curbside.Core.Application.AutoMapper
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<UserClientApplication, WhitelistUserViewModel>().ReverseMap();
            CreateMap<DomainClientApplication, DomainClientApplicationViewModel>().ReverseMap();
            CreateMap<Domain.PatientCase, PatientCaseViewModel>().ReverseMap();

            CreateMap<User, UserInfo>()
                .ForMember(dest => dest.ClientApplicationId,
                    opt => opt.MapFrom(src => src.UserClientApplication.FirstOrDefault().ClientApplicationId));

            CreateMap<Domain.PatientCase, PatientCaseViewModel>().ReverseMap();
            CreateMap<RequestPatientCaseViewModel, Domain.PatientCase>().ReverseMap();
            CreateMap<UserClientApplication, WhitelistUserViewModel>().ReverseMap();
            CreateMap<DomainClientApplication, DomainClientApplicationViewModel>().ReverseMap();
            CreateMap<User, UserViewModel>().ReverseMap();
            CreateMap<PatientCaseStatusDescription, Status>().ReverseMap();
            CreateMap<RequestUserViewModel, User>()
                .ForMember(d => d.EmailAddress, opt => opt.MapFrom(m => m.Email))
             .ForMember(d => d.UserClientApplication, opt => opt.MapFrom(s => new List<UserClientApplication> { new UserClientApplication { ClientApplicationId = s.AppId, UpdatedBy = "Prokarma", WhitelistedDate = DateTime.Now } }));

            CreateMap<Domain.PatientCase, PatientCaseInfo>()
                .ForMember(dest => dest.ProviderId, src => src.MapFrom(s => s.Provider.ProviderId))
                .ForMember(dest => dest.TenantId, src => src.MapFrom(s => s.Provider.TenantId))
                .ForMember(dest => dest.PatientCaseId, src => src.MapFrom(s => s.PatientCaseId))
                .ForMember(dest => dest.PatientCaseGuid, src => src.MapFrom(s => s.PatientCaseGuid))
                .ForMember(dest => dest.CaseTitle, src => src.MapFrom(s => s.CaseTitle))
                .ForMember(dest => dest.CaseDescription, src => src.MapFrom(s => s.CaseDescription))
                .ForMember(dest => dest.CaseStatus, src => src.MapFrom(s => s.CaseStatus))
                .ForMember(dest => dest.CreatedOn, src => src.MapFrom(s => s.CreatedOn));

            CreateMap<Provider, PatientCaseInfo>()
                .ForMember(dest => dest.ProviderId, src => src.MapFrom(s => s.ProviderId))
                .ForMember(dest => dest.TenantId, src => src.MapFrom(s => s.TenantId))
                .ForMember(dest => dest.PatientCaseId, src => src.MapFrom(s => s.PatientCases.FirstOrDefault().PatientCaseId))
                .ForMember(dest => dest.PatientCaseGuid, src => src.MapFrom(s => s.PatientCases.FirstOrDefault().PatientCaseGuid))
                .ForMember(dest => dest.CaseTitle, src => src.MapFrom(s => s.PatientCases.FirstOrDefault().CaseTitle))
                .ForMember(dest => dest.CaseDescription, src => src.MapFrom(s => s.PatientCases.FirstOrDefault().CaseDescription))
                .ForMember(dest => dest.CaseStatus, src => src.MapFrom(s => s.PatientCases.FirstOrDefault().CaseStatus))
                .ForMember(dest => dest.CreatedOn, src => src.MapFrom(s => s.PatientCases.FirstOrDefault().CreatedOn));

            CreateMap<RequestDomainViewModel, Domain.Domain>()
              .ForMember(d => d.DomainClientApplication, opt => opt.MapFrom(s => new List<DomainClientApplication> { new DomainClientApplication { ClientApplicationId = s.AppId, CreatedBy = "System User", CreatedOn = DateTime.Now } }));

            CreateMap<Domain.PatientCase, CreatePatientCaseResponseViewModel>()
                .ForMember(d => d.CaseId, opt => opt.MapFrom(m => m.PatientCaseId))
                  .ForMember(d => d.status, opt => opt.MapFrom(m => m.CaseStatus))
                  .ForMember(d => d.Title, opt => opt.MapFrom(m => m.CaseTitle))
                  .ForMember(d => d.Description, opt => opt.MapFrom(m => m.CaseDescription));

        }
    }
}
