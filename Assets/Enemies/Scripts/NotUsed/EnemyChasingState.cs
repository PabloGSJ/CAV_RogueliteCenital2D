using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyChasingState : EnemyBaseState
{
    private EnemyAI enemyAI;
    public EnemyChasingState(EnemyStateMachine currentState, EnemyStateFactory enemyStateFactory, EnemyAI ai)
    : base(currentState, enemyStateFactory)
    {
        enemyAI = ai;
    }

    public override void EnterState()
    {
        Debug.Log("Enter the chasing state");

        enemyAI.StopCoroutine(enemyAI.ChasingRoutine());

        enemyAI.StartCoroutine(enemyAI.ChasingRoutine());

        ctx.IsChasing = true;
    }

    public override void ExitState()
    {
        throw new System.NotImplementedException();
    }

    public override void UpdateState()
    {
        CheckSwitchStates();
    }

    public override void InitializeSubState()
    {
        throw new System.NotImplementedException();
    }

    public override void CheckSwitchStates()
    {
        
    }
}
