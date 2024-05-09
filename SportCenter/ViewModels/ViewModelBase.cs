using System.Collections;
using System.ComponentModel;
using CommunityToolkit.Mvvm.ComponentModel;
using Microsoft.Extensions.DependencyInjection;
using SportCenter.Views;

namespace SportCenter.ViewModels;

public abstract class ViewModelBase : ObservableObject, INotifyDataErrorInfo
{
    public virtual void Dispose() { }

    public new event PropertyChangedEventHandler? PropertyChanged;

    public bool HasErrors => _errors.Count > 0;

    public event EventHandler<DataErrorsChangedEventArgs>? ErrorsChanged;

    protected static MainWindow MainWindow => App.AppHost!.Services.GetRequiredService<MainWindow>();

    private readonly Dictionary<string, List<string>> _errors = [];

    protected new void OnPropertyChanged(string propertyName)
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

        value.Add(error);
        ErrorsChanged?.Invoke(this, new DataErrorsChangedEventArgs(propertyName));
    }

    protected void RemoveError(string propertyName)
    {
        _errors.Remove(propertyName);
        ErrorsChanged?.Invoke(this, new DataErrorsChangedEventArgs(propertyName));
    }
}