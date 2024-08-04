namespace PursuitTimer.Extensions
{
    public static class TimeSpanExtension
    {
        public static TimeSpan Sum(this IEnumerable<TimeSpan> timeSpans)
        {
            TimeSpan sumTillNowTimeSpan = TimeSpan.Zero;

            foreach (TimeSpan timeSpan in timeSpans)
            {
                sumTillNowTimeSpan += timeSpan;
            }

            return sumTillNowTimeSpan;
        }
    }
}
