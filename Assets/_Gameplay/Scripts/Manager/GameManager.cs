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
    public static Vector3 startPoint;
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
        UIManager.Instance.OpenUI<CanvasMainMenu>();
        Time.timeScale = 0;
    }

    public void OnInit()
    {
        EnemyList = new List<GameObject>();
        startPoint = Vector3.zero;
        LevelManager.instance.InstantiateLevel(startPoint);
        SwarmEnemy();
        Player.Instance.OnInit();
    }    

    public void SwarmEnemy()
    {
        List<Constain.ColorPlay> colors = new List<Constain.ColorPlay>((Constain.ColorPlay[])Enum.GetValues(typeof(Constain.ColorPlay)));
        colors.RemoveAt(((int)Player.Instance.GetColorCharacter()));
        for (int i = 0; i < startPointEnemy.Count; i++)
        {
            GameObject Enemy = Instantiate(enemy, startPointEnemy[i].position, Quaternion.identity);
            Enemy.gameObject.name = "Enemy" + i.ToString();
            EnemyList.Add(Enemy);
            Enemy E = Enemy.GetComponent<Enemy>();
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

    public void DestroyEnemy()
    {
        for(int i = 0; i < EnemyList.Count; i++)
        {
            Destroy(EnemyList[i]);
        }
    }
        
}
