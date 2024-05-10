namespace SportCenter.State.Modals;

public class ModalService : IModalService
{
    public event Action ShowModal = delegate { };
    public event Action HideModal = delegate { };

    public void RaiseShowModal()
    {
        ShowModal?.Invoke();
    }

    public void RaiseHideModal()
    {
        HideModal?.Invoke();
    }
}