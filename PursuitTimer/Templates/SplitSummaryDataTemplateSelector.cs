using PursuitTimer.Model;

namespace PursuitTimer.Templates;

public class SplitSummaryDataTemplateSelector : DataTemplateSelector
{
    public DataTemplate PositiveSplit { get; set; }
    public DataTemplate NegativeSplit { get; set; }
    public DataTemplate NeutralSplit { get; set; }

    protected override DataTemplate OnSelectTemplate(object item, BindableObject container)
    {
        SplitTime splitTime = (SplitTime)item;

        if  (splitTime.DeltaPrevious > TimeSpan.Zero)
        {
            return PositiveSplit;
        } 
        else if (splitTime.DeltaPrevious < TimeSpan.Zero)
        {
            return NegativeSplit;
        }
        else
        {
            return NeutralSplit;
        }
    }
}
