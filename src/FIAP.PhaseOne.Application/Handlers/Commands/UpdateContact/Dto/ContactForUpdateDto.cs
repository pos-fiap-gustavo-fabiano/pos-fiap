using FIAP.PhaseOne.Application.Dto;

namespace FIAP.PhaseOne.Application.Handlers.Commands.UpdateContact.Dto;

public record ContactForUpdateDto(string Name, PhoneDto Phone, string Email, AddressDto Address);
