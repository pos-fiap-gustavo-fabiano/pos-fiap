using Bogus;
using FIAP.PhaseOne.Application.Behaviors;
using FIAP.PhaseOne.Application.Mapping;
using FIAP.PhaseOne.Application.Shared;
using FIAP.PhaseOne.Domain.ContactAggregate;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using System.Reflection;

namespace FIAP.PhaseOne.Tests.Application;

public abstract class ApplicationTest
{
    protected readonly Faker _faker = new("pt_BR");
    protected readonly CancellationToken _ct = new();
    protected readonly ServiceCollection _services = new();
    protected ISender _mediator;

    protected readonly Mock<IContactRepository> _contactRepositoryMock;


    public ApplicationTest()
    {
        _services.AddMediatR((x) => x.RegisterServicesFromAssembly(Assembly.GetAssembly(typeof(ApplicationServiceRegistration))!));

        _contactRepositoryMock = new Mock<IContactRepository>();
        _services.AddScoped(x => _contactRepositoryMock.Object);

        _services.AddValidatorsFromAssembly(Assembly.GetAssembly(typeof(ApplicationServiceRegistration)));

        _services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));

        _services.AddAutoMapper(typeof(MappingProfile));

        var provider = _services.BuildServiceProvider();

        _mediator = provider.GetRequiredService<ISender>();
    }

    public void Rebuild()
    {
        var provider = _services.BuildServiceProvider();

        _mediator = provider.GetRequiredService<ISender>();
    }
}