namespace SportCenter.State.Modals;

public interface IModalService
{
    event Action ShowModal;
    event Action HideModal;

    void RaiseShowModal();
    void RaiseHideModal();
}