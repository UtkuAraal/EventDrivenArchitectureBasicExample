namespace Events;
public record ThermostatTempChangeEvent
{
    public Guid CorrelationId { get; init; }
    public decimal Temparature { get; init; }
}
