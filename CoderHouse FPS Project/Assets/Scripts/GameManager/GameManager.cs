using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public Text pointsText;
    public bool _playerIsDead;
    public Transform gameOver;
    public Transform lights;
    public Transform player;


    private void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject);
            Debug.Log("singleton awake");
        }
        else
        {
            instance = this;
        }
    }

    public void AddScore(int pointsToAdd)
    {

    }

    public void GameOverScreen()
    {
        gameOver.gameObject.SetActive(true);



    }

    public void RestartButtong()
    {
        Debug.Log("Button Test");
        SceneManager.LoadScene("EnemyTest");
        
    }
}


