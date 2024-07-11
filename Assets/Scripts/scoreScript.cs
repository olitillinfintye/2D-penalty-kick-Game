using UnityEngine;
using UnityEngine.UI;

public class scoreScript : MonoBehaviour
{
    public static int scoreValue = 0;
    Text score;
    void Start()
    {
        score = GetComponent<Text> ();
    }

    // Update is called once per frame
    void Update()
    {
        score.text = "Score: " + scoreValue; 
    }
}
