using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStateFactory
{
    private EnemyStateMachine context;
    private EnemyAI enemyAI;

    public EnemyStateFactory(EnemyStateMachine currentContext, EnemyAI ai)
    {
        context = currentContext;
        enemyAI = ai;  
    }

    public EnemyBaseState Roaming()
    {

        return new EnemyRoamingState(context, this, enemyAI);
    }
    public EnemyBaseState Chasing()
    {
        return new EnemyChasingState(context, this, enemyAI);
    }
}
