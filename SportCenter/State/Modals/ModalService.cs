using SportCenter.State.Navigators;

namespace SportCenter.State.Modals;

public class ModalService : IModalService
{
    public event Action<ModalType> ShowModal = delegate { };

    public event Action HideModal = delegate { };

    public bool IsModalVisible { get; set; }

    public void RaiseShowModal(ModalType modalType)
    {
        if (modalType is ModalType.None)
        {
            RaiseHideModal();
            return;
        }

        IsModalVisible = true;

        ShowModal?.Invoke(modalType);
    }

    public void RaiseHideModal()
    {
        IsModalVisible = false;

        HideModal?.Invoke();
    }
}