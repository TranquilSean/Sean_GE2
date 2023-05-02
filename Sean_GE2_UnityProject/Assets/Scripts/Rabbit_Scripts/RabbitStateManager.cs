using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RabbitStateManager : MonoBehaviour
{
    private RabbitStateBase currentState;

    public IdleState IdleState = new IdleState();
    public FleeState FleeState = new FleeState();
    public ScavengeState ScavengeState = new ScavengeState();


    private void Start()
    {
        ChangeState(new IdleState());
    }

    private void Update()
    {
        if (currentState != null)
        {
            currentState.UpdateState(this);
        }
    }

    public void ChangeState(RabbitStateBase newState)
    {
        if (currentState != null)
        {
            currentState.ExitState(this);
        }

        currentState = newState;
        currentState.EnterState(this);
    }

    public void Die()
    {
        Destroy(this.gameObject);
    }
}
