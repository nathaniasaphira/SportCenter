using System.ComponentModel;
using CommunityToolkit.Mvvm.ComponentModel;

namespace SportCenter.ViewModels;

public class ViewModelBase : ObservableObject
{
    public string ErrorMessage { get; set; }
    
    public new event PropertyChangedEventHandler PropertyChanged;

    private bool _isBusy;

    public bool IsBusy
    {
        get => _isBusy;
        set
        {
            _isBusy = value;
            OnPropertyChanged(nameof(IsBusy));
        }
    }

    protected new void OnPropertyChanged(string propertyName)
    {
        PropertyChanged.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}