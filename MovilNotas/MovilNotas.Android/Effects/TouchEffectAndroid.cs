using System;
using Android.Views; // 🔹 Namespace de Android
using Xamarin.Forms; // 🔹 Namespace de Xamarin.Forms
using Xamarin.Forms.Platform.Android;
using MovilNotas.Effects;
using MovilNotas.Droid.Effects;

[assembly: ResolutionGroupName("MovilNotas")]
[assembly: ExportEffect(typeof(TouchEffectAndroid), "TouchEffect")]

namespace MovilNotas.Droid.Effects
{
    public class TouchEffectAndroid : PlatformEffect
    {
        private bool _attached;
        private GestureDetector _gestureDetector;
        private readonly LongPressGestureListener _gestureListener;

        public TouchEffectAndroid()
        {
            _gestureListener = new LongPressGestureListener();
        }

        protected override void OnAttached()
        {
            if (!_attached)
            {
                _gestureDetector = new GestureDetector(Control?.Context, _gestureListener);
                _gestureListener.LongPressed += (s, e) =>
                {
                    if (Element is Xamarin.Forms.View view) // 🔹 Aquí especificamos el namespace correcto
                    {
                        var effect = (TouchEffect)Element.Effects[0];
                        effect?.HandleLongPressed();
                    }
                };

                if (Control != null)
                {
                    Control.Touch += Control_Touch;
                }
                else if (Container != null)
                {
                    Container.Touch += Control_Touch;
                }

                _attached = true;
            }
        }

        private void Control_Touch(object sender, Android.Views.View.TouchEventArgs e) // 🔹 Aquí también especificamos Android.Views.View
        {
            _gestureDetector.OnTouchEvent(e.Event);
        }

        protected override void OnDetached()
        {
            if (_attached)
            {
                if (Control != null)
                {
                    Control.Touch -= Control_Touch;
                }
                else if (Container != null)
                {
                    Container.Touch -= Control_Touch;
                }

                _attached = false;
            }
        }

        private class LongPressGestureListener : GestureDetector.SimpleOnGestureListener
        {
            public event EventHandler LongPressed;
            public override void OnLongPress(MotionEvent e)
            {
                LongPressed?.Invoke(this, EventArgs.Empty);
            }
        }
    }
}
