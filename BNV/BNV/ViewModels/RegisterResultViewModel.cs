﻿using System;
using System.Threading.Tasks;
using System.Windows.Input;
using Prism.Navigation;
using Xamarin.Forms;

namespace BNV.ViewModels
{
    public class RegisterResultViewModel : ViewModelBase
    {
        public RegisterResultViewModel(INavigationService navigationService)
            : base(navigationService)
        {
            Title = "Result Page";

            ReturnInitCommand = new Command(async () => await ReturnActionExecute());
            RequestPasswordAgainCommand = new Command(async () => await RequestPasswordAgainActionExecute());
        }

        private async Task ReturnActionExecute()
        {
            await NavigationService.NavigateAsync("NavigationPage/LoginPage");
        }

        public ICommand ReturnInitCommand { get; set; }

        private async Task RequestPasswordAgainActionExecute()
        {
            //Send the email again
        }

        public ICommand RequestPasswordAgainCommand { get; set; }
    }
}