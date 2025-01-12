namespace FIAP.PhaseOne.Application.Handlers.Commands.DeleteContact;

public class DeleteContactRequest : IRequest<ErrorOr<Deleted>>
{
    public Guid Id { get; set; }
}
