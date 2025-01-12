using AutoMapper;
using FIAP.PhaseOne.Domain.ContactAggregate;

namespace FIAP.PhaseOne.Application.Handlers.Commands.DeleteContact;

public class DeleteContactHandler(IContactRepository contactRepository) 
    : IRequestHandler<DeleteContactRequest, ErrorOr<Deleted>>
{
    public async Task<ErrorOr<Deleted>> Handle(
        DeleteContactRequest request,
        CancellationToken ct)
    {
        await contactRepository.Remove(request.Id, ct);

        return Result.Deleted;
    }
}
