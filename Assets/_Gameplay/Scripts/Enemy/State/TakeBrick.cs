using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TakeBrick : IState<Enemy>
{
    public void OnEnter(Enemy t)
    {
        t.FindNearestBrick();
        t.ChangeAnim(Constain.ANIM_RUN);
    }

    public void OnExecute(Enemy t)
    {
        t.Moving();
    }

    public void OnExit(Enemy t)
    {
        
    }
}
