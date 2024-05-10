using SportCenter.State.Modals;

namespace SportCenter.ViewModels;

public class LoadingModalViewModel : ViewModelBase
{
    private readonly IModalService _modalService;

    private string _message = "Loading...";

    public string Message
    {
        get => _message;
        set
        {
            _message = value;
            OnPropertyChanged(nameof(Message));
        }
    }

    public LoadingModalViewModel(IModalService modalService)
    {
        _modalService = modalService;
    }
}