using LightControlService;
using MassTransit;

var busControl = Bus.Factory.CreateUsingRabbitMq(cfg => 
{
    cfg.ReceiveEndpoint("lights", e =>
    {
        e.Consumer<LightSwitchEventSubscriber>();
    });
});

await busControl.StartAsync();

Console.WriteLine("Light control service is runnig. Press any key to exit.");
Console.ReadLine();

await busControl.StopAsync();