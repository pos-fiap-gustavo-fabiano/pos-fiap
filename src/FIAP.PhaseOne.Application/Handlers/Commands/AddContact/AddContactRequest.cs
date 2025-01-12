using FIAP.PhaseOne.Application.Dto;

namespace FIAP.PhaseOne.Application.Handlers.Commands.AddContact;

public class AddContactRequest : ContactDto, IRequest<ErrorOr<AddContactResponse>>
{
    
}
