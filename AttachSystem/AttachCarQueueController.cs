using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class AttachCarQueueController : MonoBehaviour, IQueue
{
    [SerializeField] private int maxAmout;
    [SerializeField] private Transform firstObjectPosition;

    private List<IQueuing> attachedObject = new List<IQueuing>();

    public bool IsCanBeAttach => attachedObject.Count <= maxAmout;

    public void Attach(IQueuing attachObject)
    {
        if (attachedObject.Contains(attachObject) || IsCanBeAttach == false)
        {
            Debug.Log("Error, you cannot attach an already attached vehicle or attachedObject > maxAmount");
            return;
        }
        
        var pos = attachedObject.Count == 0 ? firstObjectPosition : attachedObject.Last().PositionBehind;
        attachObject.TransformObject.position = pos.position;
        attachObject.TransformObject.rotation = pos.rotation;
        attachedObject.Add(attachObject);
        attachObject.Attach(pos);
    }

    public void Detach(IQueuing detachObject)
    {
        if (attachedObject.Contains(detachObject) == false)
        {
            Debug.Log("Error, detach object not found");
            return;
        }

        detachObject.Detach();
        attachedObject.Remove(detachObject);
    }
}