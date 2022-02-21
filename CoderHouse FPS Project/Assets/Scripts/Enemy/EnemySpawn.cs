using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawn : MonoBehaviour
{
    [SerializeField] private List<GameObject> typesOfEnemies = new List<GameObject>();
    [SerializeField] private Transform[] enemySpots = new Transform[11];
    public List<GameObject> enemiesOnMap = new List<GameObject>();
    private int enemyQtys;


    // Start is called before the first frame update
    void Start()
    {
        SpawnEnemies();
    }

    private void LateUpdate()
    {
        if (enemiesOnMap.Count <= 1)
        {
            SpawnEnemies();
        }


        int auxToRemove = -1;
        for (int i = 0; i < enemiesOnMap.Count; i++)
        {
            if (enemiesOnMap[i] == null)
            {
                auxToRemove = i;
            }
        }
        if (auxToRemove >= 0)
        {
            enemiesOnMap.RemoveAt(auxToRemove);
        }
    }

    private void SpawnEnemies()
    {
        enemyQtys = Random.Range(0, 10);

        for (int i = 0; i < enemyQtys; i++)
        {
            int enemyType = Random.Range(0, typesOfEnemies.Count - 1);
            CreateEnemies(typesOfEnemies[enemyType], enemySpots[i]);
        }
    }
    private void CreateEnemies(GameObject _enemyType, Transform _enemyspots)
    {
        GameObject GO = Instantiate(_enemyType, _enemyspots);
        enemiesOnMap.Add(GO);
    }

    public int GetEnemies()
    {
        return 0;

    }


}

