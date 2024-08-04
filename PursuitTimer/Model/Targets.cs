namespace PursuitTimer.Model
{
    public class Targets
    {
        public TimeSpan Target { get; set; } = TimeSpan.Zero;
        public TimeSpan Positive { get; set; } = TimeSpan.Zero;
        public TimeSpan Negative { get; set; } = TimeSpan.Zero;
    }
}
