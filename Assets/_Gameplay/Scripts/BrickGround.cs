using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BrickGround : GameUnit
{
    [SerializeField] private MeshRenderer renderer;
    [SerializeField] private BoxCollider box;
    private Vector3 currentPos;

    public Constain.ColorPlay colorBrick;
    public bool isPickBrick;

    void Start()
    {
        OnInit();
    }


    public void OnInit()
    {
        isPickBrick = false;
        colorBrick = (Constain.ColorPlay)Random.Range(0, 5);
        SetMaterialBrick();
    }

    private void OnEnable()
    {
        OnInit();
    }

    public void OnDespawn()
    {
        box.enabled = false;
        renderer.enabled = false;
        isPickBrick = true;
        StartCoroutine(IE_Refill());
        //SimplePool.Despawn(this);
    }   

    IEnumerator IE_Refill()
    {
        yield return new WaitForSeconds(5f);
        box.enabled = true;
        renderer.enabled = true;
        OnInit();
    }    

    
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") || other.CompareTag("Enemy"))
        {
            Character C = other.GetComponent<Character>();
            if (C != null && C.GetColorCharacter() == colorBrick)
            {
                isPickBrick = true;
                OnDespawn();
                C.AddBrick();
            }
        }    
    }

    public bool IsPickBrick() => isPickBrick;
    public void SetMaterialBrick( )
    {
        if (renderer != null)
        {
            switch ((int)colorBrick)
            {
                case 0:
                    renderer.material = GameManager.Instance.GetListMaterial()[0];
                    break;
                case 1:
                    renderer.material = GameManager.Instance.GetListMaterial()[1];
                    break;
                case 2:
                    renderer.material = GameManager.Instance.GetListMaterial()[2];
                    break;
                case 3:
                    renderer.material = GameManager.Instance.GetListMaterial()[3];
                    break;
                case 4:
                    renderer.material = GameManager.Instance.GetListMaterial()[4];
                    break;
                default:
                    break;
            }
        }
    }
}
