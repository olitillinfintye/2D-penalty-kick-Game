using System;
using UnityEngine;
using UnityEngine.UI;
using TouchScript.Gestures;

public class RandomPosition : MonoBehaviour
{
    public float speed = 5f;
    private bool isMovingTarget;
    private GameObject target;
    [SerializeField] private GameObject effectVFX;
    [SerializeField] private AudioClip destroySound;
    private AudioSource audioSource;
    private TapGesture gesture;
    private Rigidbody rb;
    private Camera activeCamera;

    private void OnEnable()
    {
        activeCamera = GameObject.Find("Main Camera").GetComponent<Camera>();
        gesture = GetComponent<TapGesture>();
        gesture.Tapped += tappedHandler;
    }

    private void OnDisable()
    {
        gesture.Tapped -= tappedHandler;
    }
    void Start()
    {
        target = GameObject.FindGameObjectWithTag("tocount");
        if (target == null)
        {
            Debug.LogError("Target object with tag 'tocount' not found.");
        }
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
        }
        audioSource.volume = 1f;
    }
    

    void OnMouseDown()
    {
        scoreScript.scoreValue += 1;
       // SpawnerController.scoreValue += 1;
        GetComponent<Collider>().enabled = false;
        isMovingTarget = true;
        PlayDestroySound();
        Destroy(gameObject);
        GameObject explosion = Instantiate(effectVFX, transform.position, transform.rotation);
        Destroy(explosion, 0.8f);
    }
    private void tappedHandler(object sender, System.EventArgs e)
    {
        var ray = activeCamera.ScreenPointToRay(gesture.ScreenPosition);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit) && hit.transform == transform)
        {
            scoreScript.scoreValue += 1;
            GetComponent<Collider>().enabled = false;
            isMovingTarget = true;
            PlayDestroySound();
            Destroy(gameObject);
            GameObject explosion = Instantiate(effectVFX, transform.position, transform.rotation);
            Destroy(explosion, 0.8f);
        }
    }

    private void Update()
    {
        if (isMovingTarget && target != null)
        {
            transform.position = Vector3.Lerp(transform.position, target.transform.position, speed * Time.deltaTime);
        }
    }

    private void PlayDestroySound()
    {
        if (destroySound != null)
        {
            audioSource.PlayOneShot(destroySound);
        }
    }
}