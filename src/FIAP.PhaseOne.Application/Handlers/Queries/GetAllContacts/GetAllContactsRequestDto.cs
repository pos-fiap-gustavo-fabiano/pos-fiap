namespace FIAP.PhaseOne.Application.Handlers.Queries.GetAllContacts;

public class GetAllContactsRequestDto : IRequest<GetAllContactsResponseDto>
{
    public int Page { get; set; }
    public int Limit { get; set; }
    public int? DDD { get; set; }
}
