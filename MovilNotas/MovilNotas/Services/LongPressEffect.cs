using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace MovilNotas.Services.Effects
{
    public class LongPressEffect : RoutingEffect
    {
        public event EventHandler LongPressed;

        public void HandleLongPressed()
        {
            LongPressed?.Invoke(this, EventArgs.Empty);
        }

        public LongPressEffect() : base("MovilNotas.LongPressEffect")
        {
        }
    }

}
