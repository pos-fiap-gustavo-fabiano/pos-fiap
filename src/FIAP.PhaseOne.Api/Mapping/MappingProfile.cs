using AutoMapper;
using FIAP.PhaseOne.Api.Dto;
using FIAP.PhaseOne.Application.Handlers.Commands.AddContact;

namespace FIAP.PhaseOne.Api.Mapping;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<ContactDto, AddContactRequest>().ReverseMap();
        CreateMap<ContactDto, Application.Dto.ContactDto>().ReverseMap();
        CreateMap<ContactWithIdDto, Application.Dto.ContactDto>().ReverseMap();
        CreateMap<ContactWithIdDto, Application.Dto.ContactWithIdDto>().ReverseMap();
        CreateMap<ContactDto, Application.Handlers.Commands.UpdateContact.Dto.ContactForUpdateDto>().ReverseMap();
        CreateMap<PhoneDto, Application.Dto.PhoneDto>().ReverseMap();
        CreateMap<AddressDto, Application.Dto.AddressDto>().ReverseMap();
    }
}
