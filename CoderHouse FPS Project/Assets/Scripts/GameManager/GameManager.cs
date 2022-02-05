using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public Text scoreText;
    public Text highscoreText;
    int score = 0;
    int highscore = 0;
    public bool _playerIsDead;
    public Transform gameOver;
    public Transform player;
    [SerializeField] private Transform crosshair;


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

    public void Start()
    {
        scoreText.text = score.ToString() + " POINTS";
        highscoreText.text = "Highscore: " + highscore.ToString() + " POINTS";
    }

    public void AddScore(int pointsToAdd)
    {

    }

    public void GameOverScreen()
    {
        gameOver.gameObject.SetActive(true);
        crosshair.gameObject.SetActive(false);
        Time.timeScale = 0;
        Cursor.lockState = CursorLockMode.Confined;
        Cursor.visible = true;


    }

    public void AddPoints()
    {
        score += 10;
        scoreText.text = score.ToString() + " POINTS";

        if (score >= 1000)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);

        }
    }

    public void QuitButton()
    {
        UnityEditor.EditorApplication.isPlaying = false;
    }


}


