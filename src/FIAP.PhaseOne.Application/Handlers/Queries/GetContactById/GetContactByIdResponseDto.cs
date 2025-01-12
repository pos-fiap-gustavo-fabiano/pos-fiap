using FIAP.PhaseOne.Application.Dto;

namespace FIAP.PhaseOne.Application.Handlers.Queries.GetContactById;

public class GetContactByIdResponseDto
{
    public required ContactDto Contact { get; set; }
}
