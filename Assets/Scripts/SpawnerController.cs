using TouchScript.Examples.Cube;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SpawnerController : MonoBehaviour
{
    public Transform[] spawnPoints;
    public GameObject[] Targets;
    private int randomSpawnPoints, randomTarget;
    [SerializeField] TextMeshProUGUI timerText;
   // [SerializeField] TextMeshProUGUI scoreText;
    [SerializeField] float remainingTime;
    private int spawnedTargets = 0;
    private int score = 0;
    [SerializeField] int maxTargets = 50;
    private bool isGameRunning = false;
   // public static int scoreValue = 0;
   private float lastSpawnTime = 0f;
   private float spawnInterval = 3f; // 3 seconds between spawns

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
    }

    private void StartGame()
    {
        isGameRunning = true;
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
            EndGame();
        }
    }

   /* private void SpawnATarget()
    {
        if (spawnedTargets < maxTargets && remainingTime > 0f)
        {
            randomSpawnPoints = Random.Range(0, spawnPoints.Length);
            randomTarget = Random.Range(0, Targets.Length);
            Instantiate(Targets[randomTarget], spawnPoints[randomSpawnPoints].position, Quaternion.identity);
            spawnedTargets++;
            //score++;
           // UpdateScoreText();
        }
    } */
   
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
               //score++;
               // UpdateScoreText();
           }
       }
   }

    private void EndGame()
    {
        isGameRunning = false;
        CancelInvoke("SpawnATarget");
        DestroyAllActiveTargets();
    }

    private void RestartGame()
    {
        score = 0;
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
        //scoreText.text = "Score: " + score;
        scoreScript.scoreValue += 1;
    }
}