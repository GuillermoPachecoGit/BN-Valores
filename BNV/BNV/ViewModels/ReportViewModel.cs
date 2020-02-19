using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using BNV.Models;
using Prism.Navigation;
using Xamarin.Forms;

namespace BNV.ViewModels
{
    public class ReportViewModel : ViewModelBase
    {
        private ObservableCollection<Report> _reports;

        public ReportViewModel(INavigationService navigationService)
            : base(navigationService)
        {

            var reports = new List<Report>();
            for (int i = 0; i < 15; i++)
            {
                reports.Add(new Report());
            }
            Reports = new ObservableCollection<Report>(reports);
        }

        public ObservableCollection<Report> Reports
        {
            get { return _reports; }
            set { SetProperty( ref _reports, value); }
        }
    }
}

