using MassTransit;
using ThermostatControlService;

var busControl = Bus.Factory.CreateUsingRabbitMq(cfg => 
{
    cfg.ReceiveEndpoint("thermostat", e =>
    {
        e.Consumer<ThermostatEventSubscriber>();
    });
});

await busControl.StartAsync();

Console.WriteLine("Thermostat control service is runnig. Press any key to exit.");
Console.ReadLine();

await busControl.StopAsync();