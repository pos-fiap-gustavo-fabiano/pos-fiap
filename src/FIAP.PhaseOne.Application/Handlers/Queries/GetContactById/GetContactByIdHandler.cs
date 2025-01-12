using AutoMapper;
using FIAP.PhaseOne.Application.Dto;
using FIAP.PhaseOne.Domain.ContactAggregate;

namespace FIAP.PhaseOne.Application.Handlers.Queries.GetContactById;

public class GetContactByIdHandler(
    IContactRepository contactRepository,
    IMapper mapper) : IRequestHandler<GetContactByIdRequestDto, GetContactByIdResponseDto?>
{
    public async Task<GetContactByIdResponseDto?> Handle(
        GetContactByIdRequestDto request,
        CancellationToken ct)
    {
        var contact = await contactRepository.GetById(request.Id, ct);

        if (contact is null) return null;

        var contactMapped = mapper.Map<ContactDto>(contact);

        return new GetContactByIdResponseDto { Contact = contactMapped };
    }
}
