using Application.Ships.Commands.CreateShip;
using Domain.Entities;
using FluentAssertions;

namespace Application.IntegrationTests.Ships.Commands;
using static Testing;

public class CreateShipTests : TestBase
{
    [Test]
    public async Task ShouldCreateTodoList()
    {

        var command = new CreateShipCommand
        {
            Name = "Ship"
        };

        var id = await SendAsync(command);

        var ship = await FindAsync<Ship>(id);

        ship.Should().NotBeNull();
        ship.Name.Should().Be(command.Name);
    }
}
