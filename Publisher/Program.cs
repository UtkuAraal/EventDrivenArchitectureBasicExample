using Events;
using MassTransit;

var busControl = Bus.Factory.CreateUsingRabbitMq();

try
{
    await busControl.StartAsync();

    while (true) 
    {
        Console.WriteLine("Choose an action:");
        Console.WriteLine("1. Turn Lights On");
        Console.WriteLine("2. Turn Lights Off");
        Console.WriteLine("3. Adjust Thermostat");

        var choice = Console.ReadLine();

        switch (choice) 
        {
            case "1":
                await ControlLights(busControl, LightState.ON);
                break;
            case "2":
                await ControlLights(busControl, LightState.OFF);
                break;
            case "3":
                Console.WriteLine("Enter new thermostat temperature:");
                if (decimal.TryParse(Console.ReadLine(), out decimal newTemperature))
                {
                    await ControlThermostat(busControl, newTemperature);
                }
                else
                    Console.WriteLine("Invalid temperature input.");
                break;
            default:
                Console.WriteLine("Please select out of the given options.");
                break;
        }
    }
}
finally 
{ 
    await busControl.StopAsync();
}

async Task ControlLights(IBusControl busControl, LightState state) 
{ 
    var endpoint = await busControl.GetSendEndpoint(new Uri("rabbitmq://localhost/lights"));

    await endpoint.Send<LightSwitchEvent>(new LightSwitchEvent { State = state});

    Console.WriteLine($"Sent command to turn the lights {state}");
}

async Task ControlThermostat(IBusControl busControl, decimal newTemperature)
{
    var endpoint = await busControl.GetSendEndpoint(new Uri("rabbitmq://localhost/thermostat"));

    await endpoint.Send<ThermostatTempChangeEvent>(new ThermostatTempChangeEvent { Temparature = newTemperature });

    Console.WriteLine($"Sent command to adjust thermostat to {newTemperature}C");
}

