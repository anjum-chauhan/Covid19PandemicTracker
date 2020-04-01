using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace Covid19PandemicTracker.CustomControls
{
    public class CustomPicker: Picker
    {
        public static readonly BindableProperty ImageProperty =
            BindableProperty.Create(nameof(Icon), typeof(string), typeof(CustomPicker), string.Empty);
        public string Icon
        {
            get => (string)GetValue(ImageProperty);
            set => SetValue(ImageProperty, value);
        }
    }
}
