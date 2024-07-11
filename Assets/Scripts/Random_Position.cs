using UnityEngine;
using UnityEngine.UI;

public class Random_Position : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    // public Vector2 pos;
    // public int score;
    // public Text scoreText;


    // void Start()
    // {
       
    //     move();
    // }

    // // Update is called once per frame
    // void Update()
    // {
    //     if(Input.GetMouseButtonDown(0)){
    //         move();
            
    //     }
    // }

    // void move(){
    //      transform.position = new Vector2(Random.Range(0,pos.x),Random.Range(0,pos.y));

    // }

    void OnMouseDown()
    {
        scoreScript.scoreValue += 1;
        Destroy(gameObject);
        // score++;
        // scoreText.text = score.ToString();
    }
}
