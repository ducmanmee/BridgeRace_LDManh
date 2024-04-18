using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VictoryBox : MonoBehaviour
{
    public static VictoryBox instance;
    public bool isWin;

    private void MakeInstance()
    {
        if (instance == null)
        {
            instance = this;
        }    
    }

    private void Awake()
    {
        MakeInstance();
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            isWin = true;
            UIManager.Instance.OpenUI<CanvasVictory>();
        }   
        if(other.CompareTag("Enemy"))
        {
            isWin = false;
            UIManager.Instance.OpenUI<CanvasFail>();
        }    
    }
}
