using TMPro;
using TouchScript.Examples.Cube;
using UnityEngine;
using UnityEngine.UI;

public class SpawnerController : MonoBehaviour
{
    public Transform[] spawnPoints;
    public GameObject[] Targets;
    private int randomSpawnPoints, randomTarget;
    [SerializeField] TextMeshProUGUI timerText;
    [SerializeField] float remainingTime;
    private int spawnedTargets = 0;
    [SerializeField] int maxTargets = 50;

    private void Start()
    {
        InvokeRepeating("SpawnATarget", 0f, 2f);
    }

    private void SpawnATarget()
    {
        if (spawnedTargets < maxTargets && remainingTime > 0f)
        {
            randomSpawnPoints = Random.Range(0, spawnPoints.Length);
            randomTarget = Random.Range(0, Targets.Length);
            Instantiate(Targets[randomTarget], spawnPoints[randomSpawnPoints].position, Quaternion.identity);
            spawnedTargets++;
        }
    }

    void Update()
    {
        remainingTime -= Time.deltaTime;
        remainingTime = Mathf.Max(0f, remainingTime); // Ensure remainingTime doesn't go negative
        int minutes = Mathf.FloorToInt(remainingTime / 60);
        int seconds = Mathf.FloorToInt(remainingTime % 60);
        timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);

        if (remainingTime <= 0f)
        {
            CancelInvoke("SpawnATarget");
            DestroyAllActiveTargets();
        }
    }

    private void DestroyAllActiveTargets()
    {
        GameObject[] activeTargets = GameObject.FindGameObjectsWithTag("Target");
        foreach (GameObject target in activeTargets)
        {
            Destroy(target);
        }
        
       
    }
}