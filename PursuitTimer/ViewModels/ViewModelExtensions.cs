﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PursuitTimer.ViewModels
{
    public static class ViewModelExtensions
    {
        public static MauiAppBuilder ConfigureViewModels(this MauiAppBuilder builder)
        {
            builder.Services.AddTransient<SummaryViewModel>();
            builder.Services.AddTransient<TimerSetupViewModel>();
            builder.Services.AddTransient<TimerViewModel>();

            return builder;
        }
    }
}
