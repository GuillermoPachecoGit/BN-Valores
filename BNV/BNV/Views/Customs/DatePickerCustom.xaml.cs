using System;
using System.Collections;
using System.Runtime.CompilerServices;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace BNV.Views.Customs
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class DatePickerCustom : Frame
    {
        public string Title
        {
            get => (string)GetValue(TitleProperty);
            set => SetValue(TitleProperty, value);
        }

        public static readonly BindableProperty TitleProperty = BindableProperty.Create(nameof(Title),
                                                                 typeof(string),
                                                                 typeof(DatePickerCustom), string.Empty);

        public DateTime Value
        {
            get => (DateTime)GetValue(ValueProperty);
            set => SetValue(ValueProperty, value);
        }

        public static readonly BindableProperty ValueProperty = BindableProperty.Create(nameof(Value),
                                                                 typeof(DateTime),
                                                                 typeof(DatePickerCustom), DateTime.Now);

        public IList ItemsSource
        {
            get => (IList)GetValue(ItemsSourceProperty);
            set => SetValue(ItemsSourceProperty, value);
        }

        public static readonly BindableProperty ItemsSourceProperty = BindableProperty.Create(nameof(ItemsSource),
                                                                 typeof(IList),
                                                                 typeof(DatePickerCustom), null);
        public object SelectedItem
        {
            get => GetValue(SelectedItemProperty);
            set => SetValue(SelectedItemProperty, value);
        }

        public static readonly BindableProperty SelectedItemProperty = BindableProperty.Create(nameof(SelectedItem),
                                                                 typeof(object),
                                                                 typeof(DatePickerCustom), null, defaultBindingMode: BindingMode.TwoWay);

        public string ItemDisplayBinding
        {
            get => (string)GetValue(ItemDisplayBindingProperty);
            set => SetValue(ItemDisplayBindingProperty, value);
        }

        public static readonly BindableProperty ItemDisplayBindingProperty = BindableProperty.Create(nameof(ItemDisplayBinding),
                                                                 typeof(string),
                                                                 typeof(DatePickerCustom), null);

        public DatePickerCustom()
        {
        }

        protected override void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            base.OnPropertyChanged(propertyName);
            if (propertyName == TitleProperty.PropertyName)
            {
             
            }
        }

        private void Picker_SelectedIndexChanged(object sender, System.EventArgs e)
        {
           
        }

        private void OpenPicketButton_Clicked(object sender, System.EventArgs e)
        {
          
        }

        void _datePicker_DateSelected(System.Object sender, Xamarin.Forms.DateChangedEventArgs e)
        {
            Title = e.NewDate.ToShortDateString();
            Value = e.NewDate;
        }
    }
}