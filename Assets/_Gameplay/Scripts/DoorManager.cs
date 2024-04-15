using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorManager : MonoBehaviour
{
    [SerializeField] private List<Door> doorList;
    public static DoorManager instance;
    private void MakeInstance()
    {
        if(instance == null)
        {
            instance = this;
        }    
    }
    private void Awake()
    {
        MakeInstance();
    }

    public List<Door> GetDoorList() => doorList;
}
