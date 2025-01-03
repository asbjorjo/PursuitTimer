﻿using CommunityToolkit.Mvvm.Messaging.Messages;
using PursuitTimer.Helpers;

namespace PursuitTimer.Pages.Models;

public partial class SettingsPageModel : ObservableObject
{
    [ObservableProperty]
    public partial bool Monochrome { get; set; } = false;

    [ObservableProperty]
    public partial double Target { get; set; }

    [ObservableProperty]
    public partial double ToleranceOver { get; set; }

    [ObservableProperty]
    public partial double ToleranceUnder { get; set; }

    public void InitSettings()
    {
        Monochrome = SettingsHelper.GetMonochrome();
        TimingTarget target = TimingTargetHelper.GetTimingTarget();

        Target = target.Target.TotalSeconds;
        ToleranceOver = target.ToleranceOver.TotalSeconds;
        ToleranceUnder = target.ToleranceUnder.TotalSeconds;
    }

    [RelayCommand]
    public async Task Save()
    {
        TimingTarget target = new()
        {
            Target = TimeSpan.FromSeconds(Target),
            ToleranceOver = TimeSpan.FromSeconds(ToleranceOver),
            ToleranceUnder = TimeSpan.FromSeconds(ToleranceUnder)
        };

        TimingTargetHelper.SaveTimingTarget(target);
        if (Monochrome != SettingsHelper.GetMonochrome())
        {
            SettingsHelper.SaveMonochrome(Monochrome);
            WeakReferenceMessenger.Default.Send(new SettingsChangedMessage(new TimingSettings(Monochrome)));
        }

        WeakReferenceMessenger.Default.Send(new TimingTargetChangedMessage(target));
        await Shell.Current.GoToAsync("//PursuitTimer/Timing");
    }

    [RelayCommand]
    public async Task Cancel()
    {
        await Shell.Current.GoToAsync("//PursuitTimer/Timing");
    }

    internal void Appearing()
    {
        InitSettings();
    }
}
