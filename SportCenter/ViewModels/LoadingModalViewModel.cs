using SportCenter.State.Modals;
using SportCenter.Utils;

namespace SportCenter.ViewModels;

public class LoadingModalViewModel : ViewModelBase
{
    private readonly IModalService _modalService;

    private string _message = "LoadingMessage".ConvertResourceToMessage();

    public LoadingModalViewModel(IModalService modalService)
    {
        _modalService = modalService;
    }

    public string Message
    {
        get => _message;
        set
        {
            _message = value;
            OnPropertyChanged(nameof(Message));
        }
    }
}