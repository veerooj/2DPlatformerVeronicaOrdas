using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    private int scoreAmount;
    
    //  UI
    public Slider scoreSlider;
    
    //  LOAD LEVELS
    public GameObject player;
    public GameObject LoadCanvas;
    public List<GameObject> levels;
    private int currentLevelIndex = 0;

    public new Vector3 spawnPosition;

    public GameObject gameOverScreen;
    public TMP_Text survivedText;
    private int survivedLevelsCount;
    
    public static event Action OnReset;
    
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        scoreAmount = 0;
        scoreSlider.value = 0;
        LoadCanvas.SetActive(false);
        
        Gem.OnGemCollect += IncreaseProgress;
        HoldToLoadLevel.OnHoldComplete += LoadNextLevel;
        PlayerHealth.OnPlayerDied += GameOverScreen;
        gameOverScreen.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }   

    void IncreaseProgress(int score)
    {
        scoreAmount += score;
        scoreSlider.value = scoreAmount;

        if (scoreAmount >= 100)
        {
            LoadCanvas.SetActive(true);
            
            Debug.Log("Level complete!");
        }
    }

    void LoadLevel(int level, bool wantSurviveIncrease)
    {
        LoadCanvas.SetActive(false);
        
        levels[currentLevelIndex].gameObject.SetActive(false);
        levels[level].gameObject.SetActive(true);

        player.transform.position = spawnPosition;

        currentLevelIndex = level;
        scoreAmount = 0;
        scoreSlider.value = 0;

        if (wantSurviveIncrease)
        {
            survivedLevelsCount++;
        }
        
        
       
    }
    void LoadNextLevel()
    {
        int nextLevelIndex = (currentLevelIndex == levels.Count - 1) ? 0 : currentLevelIndex + 1; //if statement but shorter
        LoadLevel(nextLevelIndex, true);
    }

    void GameOverScreen()
    {
       gameOverScreen.SetActive(true);
       survivedText.text = "YOU SURVIVED " + survivedLevelsCount + " LEVEL";
       if (survivedLevelsCount != 1)
       {
           survivedText.text += "S";
       }
    }

    public void ResetGame()
    {
        /*gameOverScreen.SetActive(false);
        survivedLevelsCount = 0;
        LoadLevel(0, false);
        */
        SceneManager.LoadScene("Level1");

    }

    void OnDestroy()
    {
        PlayerHealth.OnPlayerDied -= GameOverScreen;
    }
}
