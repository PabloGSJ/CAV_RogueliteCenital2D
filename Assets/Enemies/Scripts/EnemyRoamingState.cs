using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyRoamingState : EnemyBaseState
{
    private EnemyAI enemyAI;

    public EnemyRoamingState(EnemyStateMachine currentState, EnemyStateFactory enemyStateFactory, EnemyAI ai)
    : base(currentState, enemyStateFactory) 
    {
        enemyAI = ai;
    }

    public override void EnterState()
    {
        Debug.Log("Enter the roaming state");

        enemyAI.StopCoroutine(enemyAI.RoamingRoutine());

        enemyAI.StartCoroutine(enemyAI.RoamingRoutine());

        ctx.IsRoaming = true;
    }

    public override void ExitState()
    {
        Debug.Log("Exit the roaming state");

        ctx.IsRoaming = false;

        enemyAI.StopCoroutine(enemyAI.RoamingRoutine());
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

        Debug.Log(ctx.CloseToPlayer);

        // if the enemy is close to the player
        if (ctx.CloseToPlayer)
        {
            ExitState();
            SwitchState(factory.Chasing());
        }
    }
}
