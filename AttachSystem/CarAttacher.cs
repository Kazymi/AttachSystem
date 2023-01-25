using System;
using UnityEngine;

public class CarAttacher : MonoBehaviour
{
    [SerializeField] private AttachCarQueueController attachCarQueueController;
    [SerializeField] private TriggerListener triggerListener;

    private void OnEnable()
    {
        triggerListener.OnTriggerEnterEvent += TriggerEnter;
    }

    private void OnDisable()
    {
        triggerListener.OnTriggerEnterEvent -= TriggerEnter;
    }

    private void TriggerEnter(Transform other)
    {
        if (attachCarQueueController.IsCanBeAttach == false)
        {
            return;
        }

        var queuing = other.GetComponent<IQueuing>();
        if (queuing != null && queuing.IsCanBeAttach)
        {
            queuing.AttachToNewQueue(attachCarQueueController);
        }
    }
}