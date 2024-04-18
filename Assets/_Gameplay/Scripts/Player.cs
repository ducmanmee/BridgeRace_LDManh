using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Character
{
    public static Player Instance;
    [SerializeField] private FixedJoystick joystick;
    [SerializeField] private float moveSpeed;
    public LayerMask groundLayer;
    

    private void MakeInstance()
    {
        if(Instance == null)
        {
            Instance = this;
        }    
    }

    private void Awake()
    {
        MakeInstance();
        
    }

    private void Update()
    {
        if(CheckGround())
        {
            Debug.Log("ground");
        }
    }

    private void FixedUpdate()
    {
        Movement();
    }

    public override void OnInit()
    {
        SetColorCharacter(Constain.ColorPlay.purple);
        base.OnInit();
    }    

    public override void Movement()
    {
        GetRigibody().velocity = new Vector3(joystick.Horizontal * moveSpeed, GetRigibody().velocity.y, joystick.Vertical * moveSpeed);
        if (joystick.Horizontal != 0 || joystick.Vertical != 0)
        {
            transform.rotation = Quaternion.LookRotation(GetRigibody().velocity);
            ChangeAnim(Constain.ANIM_RUN); 
        }
        else
        {
            ChangeAnim(Constain.ANIM_IDLE);
        }
    }

    private bool CheckGround()
    {
        RaycastHit hit;
        if(Physics.Raycast(transform.position, Vector3.down, out hit, 5f, groundLayer))
        {
            return true;
        }    
        return false;
    }    

    public override void AddBrick()
    {
        base.AddBrick();
    }

    private void OnTriggerEnter(Collider other) 
    {
        if(other.CompareTag("doormid"))
        {
            BrickEnemyGroundMid();
        }  
        if(other.CompareTag("doorend"))
        {
            BrickEnemyGroundEnd();
        }  
    }

}
