using PursuitTimer.Shared.Extensions;
using PursuitTimer.Shared.Model;

namespace PursuitTimer.ViewModels
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
