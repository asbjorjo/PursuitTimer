using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using PursuitTimer.Pages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PursuitTimer.ViewModels
{
    public partial class ViewModelBase : ObservableObject
    {
        [RelayCommand]
        Task GoToMain() => Shell.Current.GoToAsync($"{nameof(HomePage)}");
    }
}
