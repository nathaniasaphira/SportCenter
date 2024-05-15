using System.Collections;
using System.ComponentModel;
using CommunityToolkit.Mvvm.ComponentModel;
using SportCenter.Utils;

namespace SportCenter.ViewModels;

public delegate TViewModel CreateViewModel<out TViewModel>() where TViewModel : ViewModelBase;

public abstract class ViewModelBase : ObservableObject, INotifyDataErrorInfo
{
    public virtual void Dispose() { }

    public new event PropertyChangedEventHandler? PropertyChanged;
    
    public bool HasErrors => _errors.Count > 0;
    
    public event EventHandler<DataErrorsChangedEventArgs>? ErrorsChanged;
    
    private readonly Dictionary<string, List<string>> _errors = [];
    
    protected new virtual void OnPropertyChanged(string? propertyName = null)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }

    public IEnumerable GetErrors(string? propertyName)
    {
        if (string.IsNullOrEmpty(propertyName))
        {
            return string.Empty;
        }

        if (!_errors.TryGetValue(propertyName, out List<string>? errors))
        {
            return string.Empty;
        }

        return errors;
    }

    protected void AddError(string propertyName, string error)
    {
        if (!_errors.TryGetValue(propertyName, out List<string>? value))
        {
            value = [];
            _errors[propertyName] = value;
        }

        value.Add(error.ConvertResourceToMessage());
        ErrorsChanged?.Invoke(this, new DataErrorsChangedEventArgs(propertyName));
    }

    protected void RemoveError(string propertyName)
    {
        _errors.Remove(propertyName);
        ErrorsChanged?.Invoke(this, new DataErrorsChangedEventArgs(propertyName));
    }
}