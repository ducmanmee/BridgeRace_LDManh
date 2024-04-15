using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    [SerializeField] private List<Material> materialColor;
    [SerializeField] private List<Transform> startPointEnemy;
    [SerializeField] private List<GameObject> EnemyList;
    [SerializeField] private Vector3 startPointPlayer;
    [SerializeField] private GameObject enemy;
    private Door nearestDoor;
    private float minDistance = Mathf.Infinity;
    private float distanceEnemyToDoor;

    private void MakeInstance()
    {
        if (Instance == null)
        {
            Instance = this;
        }    
    }

    private void Awake()
    {
        MakeInstance();
        
    }

    private void Start()
    {
        OnInit();
    }

    public void OnInit()
    {
        EnemyList = new List<GameObject>();
        startPointPlayer = Vector3.zero;
        Player.Instance.OnInit();
        SwarmEnemy();
    }    

    private void SwarmEnemy()
    {
        List<Constain.ColorPlay> colors = new List<Constain.ColorPlay>((Constain.ColorPlay[])Enum.GetValues(typeof(Constain.ColorPlay)));
        colors.Remove(Player.Instance.GetColorCharacter());   
        for (int i = 0; i < startPointEnemy.Count; i++)
        {
            GameObject Enemy = Instantiate(enemy, startPointEnemy[i].position, Quaternion.identity);
            Enemy.gameObject.name = "Enemy" + i.ToString();
            EnemyList.Add(Enemy);
            Enemy E = Enemy.GetComponent<Enemy>();
            GetNearestDoor(DoorManager.instance.GetDoorList(), E);
            E.SetColorCharacter(colors[i]);
            E.OnInit();
        }    
    }    

    public List<Material> GetListMaterial() => materialColor;
    public void GetNearestDoor(List<Door> doorList, Enemy enemy)
    {
        minDistance = Mathf.Infinity; 
        foreach (Door door in doorList)
        {
            if (door.GetOwnerDoor() == null && door.isActiveDoor)
            {
                distanceEnemyToDoor = Vector3.Distance(door.transform.position, enemy.transform.position);
                if (distanceEnemyToDoor < minDistance)
                {
                    minDistance = distanceEnemyToDoor;
                    nearestDoor = door;

                }
            }
        }
        if (nearestDoor != null)
        {
            enemy.SetDoorEnemy(nearestDoor);
            nearestDoor.SetOwnerDoor(enemy);
        }
    }
}
