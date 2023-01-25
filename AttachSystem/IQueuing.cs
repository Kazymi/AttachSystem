using UnityEngine;

public interface IQueuing
{
    Transform TransformObject { get; }
    Transform PositionBehind { get; }
    bool IsCanBeAttach { get; }
    void Attach(Transform attachPos);
    void AttachToNewQueue(IQueue queuing);
    void Detach();
}