using FIAP.PhaseOne.Application.Dto;

namespace FIAP.PhaseOne.Application.Handlers.Queries.GetAllContacts;

public class GetAllContactsResponseDto
{
    public required PaginationDto<ContactWithIdDto> Contacts { get; set; }
}
