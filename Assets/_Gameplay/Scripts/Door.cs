using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    [SerializeField] private Animator animator;
    [SerializeField] private GameObject leftWay;
    [SerializeField] private GameObject righttWay;
    private Character ownerDoor;
    public bool isActiveDoor;

    private void Start()
    {
        ownerDoor = null;
        isActiveDoor = true;
    }

    private void OpenDoor()
    {
        animator.SetTrigger(Constain.ANIM_OPENDOOR);
    }    
   
    public Character GetOwnerDoor() => ownerDoor;
    public void SetOwnerDoor(Character owner)
    {
        ownerDoor = owner;
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player") || other.CompareTag("Enemy"))
        {
            if (isActiveDoor)
            {
                Enemy E = other.GetComponent<Enemy>();
                if (E != null)
                {
                    GameManager.Instance.GetNearestDoor(DoorManager.instance.GetDoorList(), E);
                }
                OpenDoor();
                isActiveDoor = false;
            }
        }    
    }
}
