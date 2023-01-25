using DG.Tweening;
using StateMachine;
using UnityEngine;

public class AttachStartState : State
{
    private readonly IQueuing queuing;

    private IQueue controller;
    public bool IsReadyToLeave;

    public AttachStartState(IQueuing queuing)
    {
        this.queuing = queuing;
    }

    public void SetAttachCarQueueController(IQueue attachCarQueueController)
    {
        controller = attachCarQueueController;
    }

    public override void OnStateEnter()
    {
        if (controller != null)
        {
            Debug.Log("1");
            var currentTransform = queuing.TransformObject;
            currentTransform.DOScale(Vector3.zero, 1.1f);
            currentTransform.DOLocalRotate(new Vector3(0, 360, 0), 0.4f, RotateMode.FastBeyond360)
                .SetRelative(true)
                .SetEase(Ease.Linear).SetLoops(3).OnComplete(() =>
                {
                    if (controller.IsCanBeAttach)
                    {
                        controller.Attach(queuing);
                    }

                    currentTransform.transform.localScale = Vector3.one;
                    currentTransform.transform.DOShakeScale(0.4f);
                    IsReadyToLeave = true;
                });
        }
        else
        {
            Debug.Log("Attach skipped, controller==null");
        }
    }

    public override void OnStateExit()
    {
        controller = null;
        IsReadyToLeave = true;
    }
}