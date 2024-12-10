namespace Events;
public record LightSwitchEvent
{
    public Guid CorrelationId {  get; init; } 
    public LightState State { get; init; } 
}
