using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public static int currentLevel;
    public static LevelManager instance;
    [SerializeField] private List<GameObject> LevelList;
    private GameObject currentLevelPrefab;
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
        currentLevel = 1;
    }

    public void NextLevel(int level)
    {
        currentLevel += 1;

    }
    public void InstantiateLevel(int level, Vector3 pos)
    {
        if (currentLevelPrefab != null)
        {
            Destroy(currentLevelPrefab);
        }
        currentLevelPrefab = Instantiate(LevelList[level - 1], pos, Quaternion.identity);
    }

    public int GetCurrentLevel() => currentLevel;
}
