using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleState : IState<Enemy>
{
    float timer;
    float randomTime;

    public void OnEnter(Enemy t)
    {
        t.StopMoving();
        t.ChangeAnim(Constain.ANIM_IDLE);
        timer = 0f;
        randomTime = Random.Range(.5f, 1f);
    }

    public void OnExecute(Enemy t)
    {
        timer += Time.deltaTime;
        if(timer > randomTime)
        {
            t.ChangeState(new TakeBrick());
        }

    }

    public void OnExit(Enemy t)
    {
        
    }
}
