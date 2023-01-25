public interface IQueue
{
    bool IsCanBeAttach { get; }
    void Attach(IQueuing attachObject);
    void Detach(IQueuing detachObject);
}