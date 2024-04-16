using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapManager : MonoBehaviour
{
    public static MapManager Instance;
    [SerializeField] private GameObject brick;
    [SerializeField] private GameObject[] ground;
    [SerializeField] private List<Vector3> posBrick;
    public bool isActiveGround1;
    public bool isActiveGround2;
    public bool isActiveGround3;
    private BrickGround[,] brickArrayFirst;
    private BrickGround[,] brickArrayMid;
    private BrickGround[,] brickArrayEnd;
    public int width;
    public int height;
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

    void Start()
    {
        OnInit();
    }

    public void OnInit()
    {
        brickArrayFirst = new BrickGround[height, width];
        brickArrayMid = new BrickGround[height, width];
        brickArrayEnd = new BrickGround[height, width];
        InstantiateBrick(Constain.POSBRICK_FIRSTGROUND, brickArrayFirst);
        isActiveGround1 = true;
        isActiveGround2 = false;
        isActiveGround3 = false;
    }

    public void OnDespawn()
    {

    }


    public void InstantiateBrick(Vector3 pos, BrickGround[,] brickArray)
    {
        for (int i = 0; i <= height; i += 2)
        {
            for (int j = 0; j <= width; j += 2)
            {
                BrickGround brickGr = SimplePool.Spawn<BrickGround>(PoolType.BrickGround, new Vector3(pos.x + j, pos.y, pos.z + i), Quaternion.identity);
                posBrick.Add(brickGr.transform.position);
                brickGr.transform.parent = transform;
                brickArray[i / 2, j / 2] = brickGr;

            }
        }
    }

    public BrickGround GetNearestBrick(Transform trans, Enemy enemy, BrickGround[,] brickArray)
    {
        if (brickArray == null || brickArray.Length == 0)
        {
            return null;
        }

        int rows = brickArray.GetLength(0);
        int cols = brickArray.GetLength(1);
        float minDistance = float.MaxValue;
        BrickGround nearestBrick = null;

        for (int i = 0; i < rows; i++)
        {
            for (int j = 0; j < cols; j++)
            {
                BrickGround brick = brickArray[i, j];
                if (brick != null && !brick.IsPickBrick() && enemy.CheckMaterial(brick))
                {
                    float distance = Vector3.Distance(brick.transform.position, trans.position);
                    if (distance < minDistance)
                    {
                        minDistance = distance;
                        nearestBrick = brick;
                    }
                }
            }
        }
        return nearestBrick;
    }
    


    public bool GetIsActiveGround2() => isActiveGround2;
    public void SetIsActiveGround2(bool value) => isActiveGround2 = value;
    public bool GetIsActiveGround3() => isActiveGround3;
    public void SetIsActiveGround3(bool value) => isActiveGround3 = value;
    public BrickGround[,] GetFirstArray() => brickArrayFirst;
    public BrickGround[,] GetMidArray() => brickArrayMid;
    public BrickGround[,] GetEndArray() => brickArrayEnd;
}
