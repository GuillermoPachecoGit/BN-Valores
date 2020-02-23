using System;
using System.ComponentModel;

namespace BNV.Validator
{
    public abstract class BaseViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        // Indicates that the ViewModel is busy
        public bool IsBusy { get; protected set; }
    }
}
