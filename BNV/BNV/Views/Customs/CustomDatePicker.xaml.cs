using System;
using System.Collections.Generic;
using Xamarin.Forms;

namespace BNV.Views.Customs
{
    public partial class CustomDatePicker : ContentView
    {
        public CustomDatePicker()
        {
            InitializeComponent();
        }

        void TapGestureRecognizer_Tapped(System.Object sender, System.EventArgs e)
        {
            _datePicker.Focus();
        }

        void _datePicker_DateSelected(System.Object sender, Xamarin.Forms.DateChangedEventArgs e)
        {
            _label.Text = e.NewDate.ToShortDateString();
            Date = _label.Text;
        }

        public static readonly BindableProperty DateProperty = BindableProperty.Create(nameof(Date), typeof(string), typeof(CustomDatePicker), default(string), BindingMode.TwoWay);
        public string Date
        {
            get
            {
                return (string)GetValue(DateProperty);
            }

            set
            {
                SetValue(DateProperty, value);
            }
        }
    }
}
