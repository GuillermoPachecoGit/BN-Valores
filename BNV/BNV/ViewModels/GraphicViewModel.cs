using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows.Input;
using BNV.Events;
using Prism.Events;
using Prism.Navigation;
using Syncfusion.XForms.Buttons;
using Xamarin.Forms;

namespace BNV.ViewModels
{
    public class GraphicViewModel : ViewModelBase
    {
        public GraphicViewModel(INavigationService navigationService, IEventAggregator ea)
            : base(navigationService)
        {
            Events = ea;

            Events.GetEvent<NavigationColorEvent>().Subscribe(SetColor);

            Data = new ObservableCollection<Model>()
            {
                new Model("5 Jun", 50),
                new Model("6 Jun", 70),
                new Model("7 Jun", 65),
                new Model("8 Jun", 57),
                new Model("9 Jun", 43)
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
                new Model("5 Jun", rnd.Next(10,100)),
                new Model("6 Jun", rnd.Next(10,100)),
                new Model("7 Jun", rnd.Next(10,100)),
                new Model("8 Jun", rnd.Next(10,100)),
                new Model("9 Jun", rnd.Next(10,100))
            };
        }


        private void SetColor(string obj)
        {
            Color = obj;
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

        public IEventAggregator Events { get; }
        public ObservableCollection<Model> Data { get; set; }

        public ObservableCollection<string> Periods { get; set; }

        private string _color;
        public string Color
        {
            get => _color;
            set => SetProperty(ref _color, value);
        }
    }

    public class Model
    {
        public string Month { get; set; }

        public double Target { get; set; }


        public Model(string xValue, double target)
        {
            Target = target;
            Month = xValue;
        }
    }
}
