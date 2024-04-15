using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildBridge : IState<Enemy>
{
    public void OnEnter(Enemy t)
    {
        t.EnemyBuildBridge();
    }

    public void OnExecute(Enemy t)
    {
        if(t.GetListBrickCharacter() == 0)
        {
            t.ChangeState(new TakeBrick());
        }    
    }

    public void OnExit(Enemy t)
    {
        
    }
}
