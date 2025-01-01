namespace PursuitTimer.Models;

public record TimingTarget
{
    public TimeSpan Target { get; init; } = TimeSpan.Zero;
    public TimeSpan ToleranceOver { get; init; } = TimeSpan.Zero;
    public TimeSpan ToleranceUnder { get; init; } = TimeSpan.Zero;
}
