using System;
using TouchScript.Examples.Cube;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Unity.VisualScripting;
using UnityEngine.SceneManagement;
using UnityEngine.Playables;
using Random = UnityEngine.Random;

public class SpawnerController : MonoBehaviour
{
    private PlayableDirector director;
    public Transform[] spawnPoints;
    public GameObject[] Targets;
    private int randomSpawnPoints, randomTarget;
    [SerializeField] TextMeshProUGUI timerText;
    [SerializeField] TextMeshProUGUI endTimerText;
    [SerializeField] float remainingTime;
    [SerializeField] float endremainingTime;
    
    private int spawnedTargets = 0;
    private int score = 0;
    [SerializeField] int maxTargets = 50;
    private bool isGameRunning = false;
   // public static int scoreValue = 0;
   private float lastSpawnTime = 0f;
   private float spawnInterval = 3f; // 3 seconds between spawns
   public GameObject GameOverUi;
   public GameObject MainUI;
   private float restartTimer = 10f;
   private bool isGameOver = false;
  // public GameObject LeaderbordUI;

   private void Awake()
   {
       director = GetComponent<PlayableDirector>();
       director.played += Director_Played;
       director.stopped += Director_Stopped;
   }

   private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && !isGameRunning)
        {
            
            StartGame();
        }

        if (isGameRunning)
        {
            UpdateTimer();
        }

        if (Input.GetKeyDown(KeyCode.Return))
        {
            
            RestartGame();
        }
       /*if (isGameOver && remainingTime <=0f)
       {
            
           RestartGame();
       }*/

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }
        
    }

    private void StartGame()
    {
        isGameRunning = true;
        GameOverUi.SetActive(false);
      //  LeaderbordUI.SetActive(true);
        remainingTime = 60f; // 60 seconds
        //UpdateTimer();
        score = 0;
        scoreScript.scoreValue = 0;
        UpdateScoreText();
        InvokeRepeating("SpawnATarget", 0f, 2f);
    }

    private void UpdateTimer()
    {
        remainingTime -= Time.deltaTime;
        remainingTime = Mathf.Max(0f, remainingTime); // Ensure remainingTime doesn't go negative
        int minutes = Mathf.FloorToInt(remainingTime / 60);
        int seconds = Mathf.FloorToInt(remainingTime % 60);
        timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);

        if (remainingTime <= 0f)
        {
            //restartTimer = 30f;
           // restartTimer -= Time.deltaTime;
           // isGameOver = true;
            EndGame();
        }

        
        
    }
   
   //-------
   private void SpawnATarget()
   {
       if (spawnedTargets < maxTargets && remainingTime > 0f)
       {
           // Check if it's time to spawn a new target
           if (Time.time - lastSpawnTime >= spawnInterval)
           {
               randomSpawnPoints = Random.Range(0, spawnPoints.Length);
               randomTarget = Random.Range(0, Targets.Length);
               Instantiate(Targets[randomTarget], spawnPoints[randomSpawnPoints].position, Quaternion.identity);
               spawnedTargets++;
               lastSpawnTime = Time.time; // Update the last spawn time
               
           }
       }
   }

   private void Director_Stopped(PlayableDirector obj)
   {
       GameOverUi.SetActive(true);
   }

   private void Director_Played(PlayableDirector obj)
   {
       GameOverUi.SetActive(false);
   }

    private void EndGame()
    {
        GameOverUi.SetActive(true);
        
        //director.Play();
        MainUI.SetActive(false);
        isGameRunning = false;
        CancelInvoke("SpawnATarget");
        DestroyAllActiveTargets();
        /*endremainingTime = 10f; 
        endremainingTime -= Time.deltaTime;
        endremainingTime = Mathf.Max(0f, endremainingTime); // Ensure remainingTime doesn't go negative
        int seconds = Mathf.FloorToInt(endremainingTime % 60);
        int minutes = Mathf.FloorToInt(endremainingTime / 60);
        endTimerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
        if (endremainingTime <= 0)
        {
            
            SceneManager.LoadScene("G2");
        }*/
        
        
        
    }

    private void RestartGame()
    {
        score = 0;
        scoreScript.scoreValue = 0;
        GameOverUi.SetActive(false);
        MainUI.SetActive(true);
        DestroyAllActiveTargets();
        StartGame();
    }

    private void DestroyAllActiveTargets()
    {
        GameObject[] activeTargets = GameObject.FindGameObjectsWithTag("Target");
        foreach (GameObject target in activeTargets)
        {
            Destroy(target);
        }
    }

    private void UpdateScoreText()
    {
        
        scoreScript.scoreValue += 1;
    }
}