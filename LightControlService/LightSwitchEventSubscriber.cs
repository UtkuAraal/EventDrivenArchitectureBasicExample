using Events;
using MassTransit;

namespace LightControlService;

public class LightSwitchEventSubscriber : IConsumer<LightSwitchEvent>
{
    public async Task Consume(ConsumeContext<LightSwitchEvent> context)
    {
        var lightEvent = context.Message;

        var isSuccessful = await ControlLightsAsync(lightEvent);

        if (isSuccessful)
            Console.WriteLine($"Lights switched {lightEvent.State} successfully.");
        else
            Console.WriteLine($"Failed to control lights: {lightEvent.State}");
    }

    private async Task<bool> ControlLightsAsync(LightSwitchEvent lightEvent)
    {
        try
        {
            await Task.Delay(TimeSpan.FromSeconds(1));

            if (lightEvent.State == LightState.ON)
                Console.WriteLine("Turning ligth ON...");
            else
                Console.WriteLine("Turning ligth OFF...");

            return true;
        }
        catch (Exception ex) 
        {
            Console.WriteLine($"Error controlling lights: {ex.Message}");
            return false;
        }
    }
}
