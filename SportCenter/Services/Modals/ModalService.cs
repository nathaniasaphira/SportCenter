namespace SportCenter.Services.Modals;

public class ModalService : IModalService
{
    public event Action<ModalType> ShowModal = delegate { };

    public event Action HideModal = delegate { };

    public bool IsModalOpen { get; set; }

    public void RaiseShowModal(ModalType modalType)
    {
        if (modalType is ModalType.None)
        {
            RaiseHideModal();
            return;
        }

        IsModalOpen = true;

        ShowModal?.Invoke(modalType);
    }

    public void RaiseHideModal()
    {
        IsModalOpen = false;

        HideModal?.Invoke();
    }
}