using SportCenter.Services.Modals;
using SportCenter.Services.Navigators;

namespace SportCenter.ViewModels.Factories;

public class ViewModelFactory(
    CreateViewModel<HomeViewModel> createHomeViewModel,
    CreateViewModel<LoginModalViewModel> createLoginModal,
    CreateViewModel<LoadingModalViewModel> createLoadingModal)
    : IViewModelFactory
{
    public ViewModelBase? CreateViewModel(ViewType viewType)
    {
        return viewType switch
        {
            ViewType.Home => createHomeViewModel(),
            _ => throw new ArgumentException("The ViewType does not have a ViewModel.", nameof(viewType))
        };
    }

    public ViewModelBase? CreateModalViewModel(ModalType modalType)
    {
        return modalType switch
        {
            ModalType.None => null,
            ModalType.Login => createLoginModal(),
            ModalType.Loading => createLoadingModal(),
            _ => throw new ArgumentException("The ModalType does not have a ViewModel.", nameof(modalType))
        };
    }
}