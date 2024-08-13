using UnityEngine;
using UnityEngine.UI;
public class CountDownTimer : MonoBehaviour
{
    public string levelToLoad;
    private float timer = 10f;
    private Text timerSeconds;
    void Start()
    {
        timerSeconds = GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        timer -= Time.deltaTime;
        timerSeconds.text = timer.ToString("f0");
        if (timer<= 0)
        {
            Application.LoadLevel(levelToLoad);
        }
    }
}
