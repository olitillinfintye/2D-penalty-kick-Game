using UnityEngine;
using UnityEngine.UI;

public class SpawnerController : MonoBehaviour
{
    public Transform[] spawnPoints;
    public GameObject[] Targets;
    private int randomSpawnPoints, randomTarget;
    // public int score;
    // public Text scoreText;
    private int spawnedTargets = 0;
    private const int maxTargets = 5;

    private void Start()
    {
        InvokeRepeating("SpawnATarget", 0f, 1f);
    }

    private void SpawnATarget()
    {
        if (spawnedTargets < maxTargets)
        {
            randomSpawnPoints = Random.Range(0, spawnPoints.Length);
            randomTarget = Random.Range(0, Targets.Length);
            Instantiate(Targets[randomTarget], spawnPoints[randomSpawnPoints].position, Quaternion.identity);
            spawnedTargets++;
        }
    }

    private void Update()
    {
        // UpdateScore();
    }

    // private void UpdateScore()
    // {
    //     score++;
    //     scoreText.text = score.ToString();
    // }
}