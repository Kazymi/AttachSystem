using System;
using UnityEngine;

public class TriggerListener : MonoBehaviour
{
    public event Action<Transform> OnTriggerEnterEvent;
    public event Action<Transform> OnTriggerExitEvent;

    private void OnTriggerEnter(Collider other)
    {
        OnTriggerEnterEvent?.Invoke(other.transform);
    }

    private void OnTriggerExit(Collider other)
    {
        OnTriggerExitEvent?.Invoke(other.transform);
    }
}