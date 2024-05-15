namespace SportCenter.Services.Modals;

public enum ModalType
{
    None,
    Login,
    Loading
}

public interface IModalService
{
    event Action<ModalType> ShowModal;

    event Action HideModal;

    bool IsModalOpen { get; set; }

    void RaiseShowModal(ModalType modalType);

    void RaiseHideModal();
}