using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using BNV.Models;
using Prism.Navigation;

namespace BNV.ViewModels
{
    public class BonoViewModel : ViewModelBase
    {
        private ObservableCollection<Bono> _bonos;

        public BonoViewModel(INavigationService navigationService)
            : base(navigationService)
        {
            var bonos = new List<Bono>();
            for (int i = 0; i < 15; i++)
            {
                bonos.Add(new Bono());
            }
            Bonos = new ObservableCollection<Bono>(bonos);
        }

        public ObservableCollection<Bono> Bonos
        {
            get { return _bonos; }
            set { SetProperty(ref _bonos, value); }
        }
    }
}
