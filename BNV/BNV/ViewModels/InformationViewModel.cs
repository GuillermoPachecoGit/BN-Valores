using System;
using System.Threading.Tasks;
using System.Windows.Input;
using Prism.Navigation;
using Syncfusion.XForms.Buttons;
using Xamarin.Forms;

namespace BNV.ViewModels
{
    public class InformationViewModel : ViewModelBase
    {
        public InformationViewModel(INavigationService navigationService)
            : base(navigationService)
        {
            Init();
        }

        private void Init()
        {
            Average = "4.36";
            Maximum = "2.08";
            Minimum = "5.7";
            VolumenMax = "N.D.";
            VolumenMin = "N.D.";
            ValueRendimiento = "3.70";
            PercentageRendimiento = "Rendimiento: "+"3.55%";
            ValueVolumen = "13.0%";
            PercentageVolumen = "Volumen: " + "N.D.";

            ActionCommand = new Command(async () => await ActionExecute());

        }



        private async Task ActionExecute()
        {
            Random rnd = new Random();
            Average = rnd.Next(100).ToString();
            Maximum = "2.08";
            Minimum = rnd.Next(100).ToString();
            VolumenMax = "N.D.";
            VolumenMin = rnd.Next(100).ToString();
            ValueRendimiento = "3.70";
            PercentageRendimiento = "Rendimiento: "+ rnd.Next(100).ToString() + "%";
            ValueVolumen = rnd.Next(100).ToString() +"%";
            PercentageVolumen = "Volumen: " + "N.D.";
        }

        public ICommand ActionCommand { get; set; }

        private string _average;
        public string Average
        {
            get { return _average; }
            set { _average = value; RaisePropertyChanged(); }
        }

        private string _maximum;
        public string Maximum
        {
            get { return _maximum; }
            set { _maximum = value; RaisePropertyChanged(); }
        }

        private string _minimum;
        public string Minimum
        {
            get { return _minimum; }
            set { _minimum = value; RaisePropertyChanged(); }
        }

        private string _volumenMax;
        public string VolumenMax
        {
            get { return _volumenMax; }
            set { _volumenMax = value; RaisePropertyChanged(); }
        }

        private string _volumenMin;
        public string VolumenMin
        {
            get { return _volumenMin; }
            set { _volumenMin = value; RaisePropertyChanged(); }
        }

        private string _valueRendimiento;
        public string ValueRendimiento
        {
            get { return _valueRendimiento; }
            set { _valueRendimiento = value; RaisePropertyChanged(); }
        }

        private string _percentageRendimiento;
        public string PercentageRendimiento
        {
            get { return _percentageRendimiento; }
            set { _percentageRendimiento = value; RaisePropertyChanged(); }
        }

        private string _valueVolumen;
        public string ValueVolumen
        {
            get { return _valueVolumen; }
            set { _valueVolumen = value; RaisePropertyChanged(); }
        }

        private string _percentageVolumen;
        public string PercentageVolumen
        {
            get { return _percentageVolumen; }
            set { _percentageVolumen = value; RaisePropertyChanged(); }
        }


        private SfChip _seletedItem;
        public SfChip SelectedItem
        {
            get { return _seletedItem; }

            set { SetProperty(ref _seletedItem, value); ActionCommand.Execute(null); }
        }
    }
}
