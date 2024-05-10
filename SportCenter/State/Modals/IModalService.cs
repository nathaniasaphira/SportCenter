using SportCenter.State.Navigators;

namespace SportCenter.State.Modals;

public interface IModalService
{
    event Action<ModalType> ShowModal;

    event Action HideModal;

    bool IsModalVisible { get; set; }

    void RaiseShowModal(ModalType modalType);

    void RaiseHideModal();
}