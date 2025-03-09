using System;
using Xamarin.Forms;

namespace MovilNotas.Effects
{
    public class TouchEffect : RoutingEffect
    {
        public event EventHandler LongPressed;

        public void HandleLongPressed()
        {
            LongPressed?.Invoke(this, EventArgs.Empty);
        }

        public TouchEffect() : base("MovilNotas.TouchEffect") { }
    }
}
