using System.Collections;
using StateMachine;
using StateMachine.Conditions;
using UnityEngine;
using StateMachine = StateMachine.StateMachine;

public class AttachCarController : MonoBehaviour, IQueuing
{
    [SerializeField] private AttachCarConfigurations attachCarConfigurations;
    [SerializeField] private Transform positionToAttachNextCar;
    public Transform TransformObject => transform;
    public Transform PositionBehind => positionToAttachNextCar;
    public bool IsCanBeAttach => currentAttachTransform == null && IsStartAttach == false;

    private bool IsStartAttach = false;
    private Transform currentAttachTransform;
    private Vector3 lastPos;
    private global::StateMachine.StateMachine stateMachine;

    private AttachStartState attachStartState;

    private void Awake()
    {
        gameObject.layer = 7;
        InitializeStateMachine();
    }


    private void InitializeStateMachine()
    {
        var idleState = new IdleState();
        attachStartState = new AttachStartState(this);

        attachStartState.AddTransition(new StateTransition(idleState, new FuncCondition(() =>
        {
            if (!attachStartState.IsReadyToLeave)
            {
                return false;
            }

            IsStartAttach = false;
            return true;
        })));
        stateMachine = new global::StateMachine.StateMachine(idleState);
    }

    public void Attach(Transform attachPos)
    {
        gameObject.layer = 6;
        currentAttachTransform = attachPos;
    }

    public void AttachToNewQueue(IQueue queuing)
    {
        IsStartAttach = true;
        attachStartState.SetAttachCarQueueController(queuing);
        stateMachine.SetState(attachStartState);
    }

    public void Detach()
    {
        gameObject.layer = 7;
        currentAttachTransform = null;
    }

    private void FixedUpdate()
    {
        MoveAndRotate();
    }

    private void MoveAndRotate()
    {
        if (currentAttachTransform != null && currentAttachTransform.position != lastPos)
        {
            Move();
            Rotate();
            lastPos = currentAttachTransform.position;
        }
    }

    private void Move()
    {
        transform.position = Vector3.Lerp(currentAttachTransform.position, transform.position,
            attachCarConfigurations.SpeedLerp);
    }

    private void Rotate()
    {
        var lookPos = currentAttachTransform.position - transform.position;
        lookPos.y = 0;
        var rotation = Quaternion.LookRotation(lookPos);
        transform.rotation = Quaternion.Slerp(transform.rotation, rotation, 5 * Time.fixedDeltaTime);
    }
}