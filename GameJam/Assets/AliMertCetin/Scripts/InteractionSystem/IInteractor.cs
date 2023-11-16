namespace AliMertCetin.Scripts.InteractionSystem
{
    public interface IInteractor
    {
        InteractorSettings GetInteractorSettings();
        T GetComponent<T>();
    }
}