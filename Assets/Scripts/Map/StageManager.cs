using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Experimental.AssetDatabaseExperimental.AssetDatabaseCounters;

public class StageManager : MonoBehaviour
{
    public static StageManager instance;

    public GameObject stage;
    public float spacing = 40f;

    private void Awake()
    {
        instance = this;
    }

    void Start()
    {
        StageCreate();
    }

    void Update()
    {
        
    }

    void StageCreate()
    {
        for (int x = 0; x < 5; x++)
        {
            for (int z = 0; z < 5; z++)
            {
                Vector3 spawnPos = new Vector3(
                    x * spacing,
                    0f,
                    z * spacing
                );

                Instantiate(stage, spawnPos, Quaternion.identity);
            }
        }
    }
}
