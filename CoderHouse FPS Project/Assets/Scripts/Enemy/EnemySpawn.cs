using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawn : MonoBehaviour
{
    [SerializeField] private List<GameObject> typesOfEnemies = new List<GameObject>();
    [SerializeField] private Transform[] enemySpots = new Transform[11];
    public List<GameObject> enemiesOnMap = new List<GameObject>();
    private int cantidadDeEnemigos;


    // Start is called before the first frame update
    void Start()
    {

        cantidadDeEnemigos = 5;// Random.Range(0, 10);
        Debug.Log(typesOfEnemies.Count);

        for (int i = 0; i < cantidadDeEnemigos; i++)
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



}

