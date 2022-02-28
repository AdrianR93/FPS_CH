using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public Text scoreText;
    public Text timeText;
    public Text bestTimeText;
    int score = 0;
    float currentTime = 0;
    float bestTime = 0;
    public bool _playerIsDead;
    public Transform gameOver;
    public Transform BossRenegade;
    public Transform player;
    [SerializeField] private Transform crosshair;
    [SerializeField] private GameObject _pauseMenu;
    [SerializeField] private GameObject tutorialCanvas;
    private bool pauseToggle;
    private bool isBossSpawned;
    private bool tutKey;


    private void Awake()
    {
        Time.timeScale = 1;
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
        bestTime = PlayerPrefs.GetFloat("bestTime", 0);
        scoreText.text = score.ToString() + " POINTS";
        bestTimeText.text = "Best Time Alive:  " + bestTime.ToString("N2") + " SECONDS";

        isBossSpawned = false;
        pauseToggle = false;
        tutKey = true;
    }

    public void Update()
    {
        // Tutorial Canvas
        if (Input.GetKeyDown(KeyCode.T))
        {
            tutKey = !tutKey;
            {
                if (tutKey == true)
                {
                    tutorialCanvas.gameObject.SetActive(true);
                }
                if (tutKey == false)
                {
                    tutorialCanvas.gameObject.SetActive(false);
                }
            }
        }
           
            
        // Timer and Best Time Canvas
        currentTime += Time.deltaTime;
        timeText.text = "Time:  " + currentTime.ToString("N2") + " SECONDS";
        if (currentTime > bestTime && RenegadeBoss.endGame == true)
        {
            PlayerPrefs.SetFloat("bestTime", currentTime);
        }


        // Pause Menu
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            pauseToggle = !pauseToggle;

            {
                if (pauseToggle == true)

                _pauseMenu.gameObject.SetActive(true);
                Time.timeScale = 0;
                //pauseToggle = true;
                Cursor.lockState = CursorLockMode.Confined;
                Cursor.visible = true;
            }
            
                if(pauseToggle == false)
            {
                _pauseMenu.gameObject.SetActive(false);
                Time.timeScale = 1;
                //pauseToggle = false;
                Cursor.lockState = CursorLockMode.Locked;
                Cursor.visible = false;
            }

        }

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
        if (isBossSpawned == false)
        {
            if (score >= 100)
            {
                SpawnBossRenegade();
                isBossSpawned = true;

            }
        }


    }

    public void QuitButton()
    {
        Application.Quit();
    }

    public void ReloadLevel()
    {
        Scene scene = SceneManager.GetActiveScene(); SceneManager.LoadScene(scene.name);
        Time.timeScale = 1;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    public void MainMenu()
    {
        SceneManager.LoadScene("MainMenu");
        Time.timeScale = 1;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }


    public void ResumeGame()
    {
        Time.timeScale = 1;
        _pauseMenu.gameObject.SetActive(false);
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }


    public void SpawnBossRenegade()
    {

        BossRenegade.gameObject.SetActive(true);
        Vector3 playerPos = player.transform.position;
        Vector3 playerDirection = player.transform.forward;
        Quaternion playerRotation = player.transform.rotation;
        float spawnDistance = 5;

        Vector3 spawnPos = playerPos + playerDirection * spawnDistance;

        BossRenegade.transform.position = spawnPos;
    }


}



