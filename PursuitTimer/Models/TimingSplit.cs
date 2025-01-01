namespace PursuitTimer.Models;

public record TimingSplit(DateTime Time, TimeSpan Split, TimeSpan DeltaPrevious, TimeSpan DeltaTarget);