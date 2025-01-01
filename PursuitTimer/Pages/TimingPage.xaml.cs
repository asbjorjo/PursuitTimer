namespace PursuitTimer.Pages;

public partial class TimingPage : ContentPage
{
    TimingPageModel model => (TimingPageModel)BindingContext;

	public TimingPage()
	{
		InitializeComponent();
	}

    protected override void OnBindingContextChanged()
    {
        model.InitSession();
    }

    protected override void OnSizeAllocated(double width, double height)
    {
        base.OnSizeAllocated(width, height);

        model.Size(width, height);
    }

    protected override void OnAppearing()
    {
        model.Appearing();
        this.ShowChanges();
    }

    protected override void OnDisappearing()
    {
        model.Disappearing();
    }
}