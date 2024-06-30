using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PursuitTimer.Pages
{
    public static class PagesExtensions
    {
        public static MauiAppBuilder ConfigurePages(this MauiAppBuilder builder)
        {
            builder.Services.AddTransient<SummaryPage>();
            builder.Services.AddTransient<TimerSetupPage>();
            builder.Services.AddTransient<TimerPage>();

            return builder;
        }
    }
}
