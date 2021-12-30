using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    [SerializeField] private int _score;
    public Text pointsText;
    public bool _playerIsDead;
    public Transform gameOver;


    private void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject);
            Debug.Log("singleton awake");
        }
    }

    public void AddScore(int pointsToAdd)
    {
        _score += pointsToAdd;
        Debug.Log($"You has died {_score} times");
    }

    public void GameOverScreen()
    {
        gameObject.SetActive(true);
    }
}


