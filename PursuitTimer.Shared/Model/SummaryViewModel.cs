using PursuitTimer.Shared.Extensions;

namespace PursuitTimer.Shared.Model
{
    public class SummaryViewModel
    {
        public SummaryViewModel()
        {
            SplitTimes = new();
        }

        public SummaryViewModel(List<SplitTime> splitTimes)
        {
            SplitTimes = splitTimes;
        }

        public List<SplitTime> SplitTimes
        {
            get;
        }

        public TimeSpan SumTimes
        {
            get => SplitTimes.Select(x => x.Split).Sum();
        }
    }
}
