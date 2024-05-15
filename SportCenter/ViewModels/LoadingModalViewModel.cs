using SportCenter.Services.Modals;
using SportCenter.Services.Navigators;
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

    public async void LoadAuthentication()
    {
        await Task.Delay(TimeSpan.FromSeconds(2));

        // TODO (5/16/2024) Nicky: Implement authentication logic.
        const bool authenticated = true;
        if (authenticated)
        {
            _modalService.RaiseHideModal();
        }
        else
        {
            _modalService.RaiseShowModal(ModalType.Login);
        }
    }
}