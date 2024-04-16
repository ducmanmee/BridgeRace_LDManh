using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BrickBridge : MonoBehaviour
{
    private bool isActivePlayer;
    private bool isActiveEnemy;
    [SerializeField] private MeshRenderer brickBridge;
    [SerializeField] private GameObject boxBrickBr;
    public Constain.ColorPlay currentColor;

    private void OnTriggerEnter(Collider other)
    {
        Character C = other.GetComponent<Character>();
        if (other.CompareTag("Player"))
        {
            if(C.GetListBrickCharacter() <= 0)
            {
                boxBrickBr.SetActive(true);
            }    
            if(!isActivePlayer)
            {
                isActivePlayer = true;
                BuildBridge(C);
            }
            else
            {
                if(currentColor != C.GetColorCharacter() && C.CheckUpBridge())
                {
                    BuildBridge(C);
                }    
            }    
        }   

        if (other.CompareTag("Enemy"))
        {
            if (!isActiveEnemy)
            {
                isActiveEnemy = true;
                BuildBridge(C);
            }
            else
            {
                if (currentColor != C.GetColorCharacter())
                {
                    BuildBridge(C);
                }
            }
        }    
    }

    private void BuildBridge(Character C)
    {
        if (C.GetListBrickCharacter() > 0)
        {
            boxBrickBr.SetActive(false);
            this.gameObject.SetActive(true);
            brickBridge.enabled = true;
            brickBridge.material = C.GetMaterialCharacter();
            currentColor = C.GetColorCharacter();
            C.UnBrickBuildBridge();
        }  
    }


}
