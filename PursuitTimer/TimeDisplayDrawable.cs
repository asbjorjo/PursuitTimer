using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Font = Microsoft.Maui.Graphics.Font;

namespace PursuitTimer
{
    internal class TimeDisplayDrawable : IDrawable
    {
        private const float MinRatio = 3.5F;
        private TimeSpan displayTime;

        public void Draw(ICanvas canvas, RectF dirtyRect)
        {
            float ratio = dirtyRect.Width / dirtyRect.Height;
            float fontSize = ratio < MinRatio ? dirtyRect.Height / MinRatio * ratio : dirtyRect.Height;
            canvas.FontSize = fontSize;
            canvas.Font = Font.DefaultBold;

            canvas.DrawString(displayTime.ToString("ss'.'fff"), 0, 0, dirtyRect.Width, dirtyRect.Height, HorizontalAlignment.Center, VerticalAlignment.Center, TextFlow.ClipBounds);
        }

        public void UpdateTime(TimeSpan time)
        {
            displayTime = time;
        }
    }
}
