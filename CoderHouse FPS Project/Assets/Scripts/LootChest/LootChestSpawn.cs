using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LootChestSpawn : MonoBehaviour
{
    [SerializeField] private GameObject LootChest;
    [SerializeField] private Transform[] spawnPoints = new Transform[4];



    void Start()
    {
        ChooseSpawnPoint();
    }

    private void ChooseSpawnPoint()
    {
        for (int i = 0; i < spawnPoints.Length; i++)
        {
            CreateLootChest(spawnPoints[i]);
        }
    }
    private void CreateLootChest(Transform _spawnPoint
        )
    {
        Instantiate(LootChest, _spawnPoint);
    }


}
