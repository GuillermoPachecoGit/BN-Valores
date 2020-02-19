using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using BNV.Models;
using Prism.Navigation;

namespace BNV.ViewModels
{
    public class ChangeTypeViewModel : ViewModelBase
    {
        private ObservableCollection<ChangeType> _types;

        public ChangeTypeViewModel(INavigationService navigationService)
            : base(navigationService)
        {
            var reports = new List<ChangeType>();
            for (int i = 0; i < 15; i++)
            {
                reports.Add(new ChangeType());
            }
            Types = new ObservableCollection<ChangeType>(reports);
        }

        public ObservableCollection<ChangeType> Types
        {
            get { return _types; }
            set { SetProperty(ref _types, value); }
        }
    }
}
