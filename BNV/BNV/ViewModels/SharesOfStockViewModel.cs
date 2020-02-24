using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using BNV.Models;
using Prism.Navigation;

namespace BNV.ViewModels
{
    public class SharesOfStockViewModel : ViewModelBase
    {
        private ObservableCollection<ShareOfStock> _shares;

        public SharesOfStockViewModel(INavigationService navigationService)
            : base(navigationService)
        {
            var shares = new List<ShareOfStock>();
            for (int i = 0; i < 14; i++)
            {
                shares.Add(new ShareOfStock());
            }
            Shares = new ObservableCollection<ShareOfStock>(shares);
        }

        public ObservableCollection<ShareOfStock> Shares
        {
            get { return _shares; }
            set { SetProperty(ref _shares, value); }
        }


    }
}
