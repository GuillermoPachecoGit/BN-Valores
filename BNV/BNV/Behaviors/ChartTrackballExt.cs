using System;
using Syncfusion.SfChart.XForms;
using Xamarin.Forms;

namespace BNV.Behaviors
{
    public class ChartTrackballExt : ChartTrackballBehavior
    {
        public double TrackballDuration { get; set; }

        public double ToolTipDuration { get; set; }

        ChartSeries series;

        public ChartTrackballExt()
        {
            //series = _series;
        }

        protected override void OnTouchUp(float x, float y)
        {

            Device.StartTimer(TimeSpan.FromSeconds(TrackballDuration), () =>
            {
                Hide();
                return false;
            });

        }

        //protected async override void OnTouchUp(float x, float y)
        //{
        //    await Task.Delay(4000);
        //    this.Hide();
        //}

        protected override void OnTouchDown(float x, float y)
        {
            Show(x, y);
        }
    }
}
