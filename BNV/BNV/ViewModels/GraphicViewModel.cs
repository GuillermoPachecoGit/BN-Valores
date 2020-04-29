using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows.Input;
using BNV.Models;
using Prism.Navigation;
using Syncfusion.XForms.Buttons;
using Xamarin.Forms;

namespace BNV.ViewModels
{
    public class GraphicViewModel : ViewModelBase
    {
        public GraphicViewModel(INavigationService navigationService)
            : base(navigationService)
        {
            Data = new ObservableCollection<Model>()
            {
                new Model("5 Jun", 873.8, 878.85, 855.5, 860.5),
                new Model("6 Jun", 861, 868.4, 835.2, 843.45),
                new Model("7 Jun", 846.15, 853, 838.5, 847.5),
                new Model("8 Jun", 846, 860.75, 841, 855),
                new Model("9 Jun", 841, 845, 827.85, 838.65)
            };

            Periods = new ObservableCollection<string>()
            {
                "1 mes" ,
                "3 meses",
                "6 meses" ,
                "1 año" ,
                "2 años"
            };

            ValueRendimiento = "3.70";
            PercentageRendimiento = "Rendimiento: " + "3.55%";
            ValueVolumen = "13.0%";
            PercentageVolumen = "Volumen: " + "N.D.";
            ActionCommand = new Command(async () => await ActionExecute());

        }

        private async Task ActionExecute()
        {
            Random rnd = new Random();
            ValueRendimiento = "3.70";
            PercentageRendimiento = "Rendimiento: " + rnd.Next(100).ToString() + "%";
            ValueVolumen = rnd.Next(100).ToString() + "%";
            PercentageVolumen = "Volumen: " + "N.D.";

            Data = new ObservableCollection<Model>()
            {
                new Model("5 Jun", rnd.Next(820,880), rnd.Next(820,880), rnd.Next(820,880), rnd.Next(820,880)),
                new Model("6 Jun", rnd.Next(820,880), rnd.Next(820,880), rnd.Next(820,880), rnd.Next(820,880)),
                new Model("7 Jun", rnd.Next(820,880), rnd.Next(820,880), rnd.Next(820,880), rnd.Next(820,880)),
                new Model("8 Jun", rnd.Next(820,880), rnd.Next(820,880), rnd.Next(820,880), rnd.Next(820,880)),
                new Model("9 Jun", rnd.Next(820,880), rnd.Next(820,880), rnd.Next(820,880), rnd.Next(820,880))
            };
        }

        public ICommand ActionCommand { get; set; }

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

        public ObservableCollection<Model> Data { get; set; }

        public ObservableCollection<string> Periods { get; set; }
    }

    public class Model
    {
        public string Year { get; set; }

        public double High { get; set; }

        public double Low { get; set; }

        public double Open { get; set; }

        public double Close { get; set; }

        public Model(string xValue, double open, double high, double low, double close)
        {
            Year = xValue;
            Open = open;
            High = high;
            Low = low;
            Close = close;
        }
    }
}
