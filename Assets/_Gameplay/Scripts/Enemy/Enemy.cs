using UnityEngine;
using UnityEngine.AI;

public class Enemy : Character
{
    public NavMeshAgent agent;
    public Camera cam;
    private IState<Enemy> currentState;
    private bool isMoving;
    private BrickGround target;
    public Door doorEnemy;
    private LayerMask groundLayer;
    private BrickGround[,] currentGround; 

    void Update()
    {
        if (currentState != null)
        {
            currentState.OnExecute(this);
        }
    }

    public void ChangeState(IState<Enemy> newState)
    {
        if (currentState != null)
        {
            currentState.OnExit(this);
        }

        currentState = newState;

        if (currentState != null)
        {
            currentState.OnEnter(this);
        }
    }

    public override void OnInit()
    {
        base.OnInit();
        ChangeState(new IdleState());
        currentGround = MapManager.Instance.GetFirstArray();
    }

    public override void AddBrick()
    {
        base.AddBrick(); 
    }

    public void FindNearestBrick()
    {
        target = MapManager.Instance.GetNearestBrick(this.transform, this, currentGround);
        if(target != null)
        {
            agent.SetDestination(target.transform.position);
        }  
    }    

    public void Moving()
    {
        if(this.GetListBrickCharacter() > 6)
        {
            ChangeState(new BuildBridge());
        }
        if (target != null)
        {
            if( Vector3.Distance(this.transform.position, target.transform.position) < .1f)
            {
                ChangeState(new IdleState());
            }    
        }
        else
        {
            ChangeState(new IdleState());
        }
    }

    public void EnemyBuildBridge()
    { 
        agent.SetDestination(doorEnemy.transform.position);
    }    

    public void StopMoving()
    {

    } 
    
    public void SetDoorEnemy(Door door)
    {
        doorEnemy = door;
    }   

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("doormid"))
        {            
            BrickEnemyGroundMid();
            currentGround = MapManager.Instance.GetMidArray();
            ChangeState(new TakeBrick());
        }
        if (other.CompareTag("doorend"))
        {
            BrickEnemyGroundEnd();
            currentGround = MapManager.Instance.GetEndArray();
            ChangeState(new TakeBrick());
        }
    }
}
