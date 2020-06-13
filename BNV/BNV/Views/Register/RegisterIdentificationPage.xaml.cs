using System;
using System.Collections.Generic;

using Xamarin.Forms;

namespace BNV.Views.Register
{
    public partial class RegisterIdentificationPage : ContentPage
    {
        private const string Mask15 = "###############";

        public RegisterIdentificationPage()
        {
            InitializeComponent();
        }

        void comboBox2_SelectionChanged(System.Object sender, Syncfusion.XForms.ComboBox.SelectionChangedEventArgs e)
        {
            IdentValue.Text = string.Empty;
            switch (e.Value)
            {
                case "Cédula de identidad":
                    IdentValue.Placeholder = "0#-####-####";
                    break;
                case "Cédula de residencia":
                    IdentValue.Placeholder = Mask15;// 15 characters
                    break;
                case "Pasaporte":
                    IdentValue.Placeholder = Mask15;
                    break;
                case "Carné de refugiado":
                    IdentValue.Placeholder = Mask15;
                    break;
                case "Carné de pensionado":
                    IdentValue.Placeholder = Mask15;
                    break;
                case "ID físico extranjero":
                    IdentValue.Placeholder = Mask15; // falta definicion
                    break;
                case "DIMEX":
                    IdentValue.Placeholder = "1###########"; // 12 characters
                    break;
                case "DIDI":
                    IdentValue.Placeholder = "5###########";
                    break;
                default:
                    break;
            }
        }
    }
}
