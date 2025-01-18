using Bogus;

namespace FIAP.PhaseOne.Tests.Domain;

public abstract class DomainTest
{
    protected readonly Faker _faker = new("pt_BR");
}