using System.Collections.ObjectModel;
using BNV.Models;
using Prism.Events;
using Prism.Navigation;

namespace BNV.ViewModels
{
    public class ReportViewModel : ViewModelBase
    {
        private ObservableCollection<Report> _reports;

        public ReportViewModel(INavigationService navigationService, IEventAggregator ea)
            : base(navigationService)
        {
            
        }

    }
}

