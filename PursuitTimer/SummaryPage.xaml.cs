using PursuitTimer.Shared.Extensions;
using PursuitTimer.Shared.Model;
using System.Runtime.CompilerServices;

namespace PursuitTimer;

[QueryProperty(nameof(Splits), "Splits")]
public partial class SummaryPage : ContentPage
{
    public static readonly BindableProperty SplitsProperty =
        BindableProperty.Create("Splits", typeof(List<SplitTime>), typeof(SummaryPage), new List<SplitTime>());

    public List<SplitTime> Splits
    {
        get =>  (List<SplitTime>)GetValue(SplitsProperty);
        set 
        {
            SetValue(SplitsProperty, value);
        }
    }

    public SummaryPage()
	{
        InitializeComponent();
        BindingContext = this;
    }

    private async void OnStartClickedAsync(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync("//TimerPage");
    }
}