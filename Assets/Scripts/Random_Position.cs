using System;
using UnityEngine;
using UnityEngine.UI;

public class RandomPosition : MonoBehaviour
{
    public float speed = 5f;
    private bool isMovingTarget;
    private GameObject target;
    [SerializeField] private GameObject effectVFX;
    [SerializeField] private AudioClip destroySound;
    private AudioSource audioSource;

    void Start()
    {
        target = GameObject.FindGameObjectWithTag("tocount");
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
        GetComponent<Collider2D>().enabled = false;
        isMovingTarget = true;
        PlayDestroySound();
        Destroy(gameObject);
        GameObject explosion = Instantiate(effectVFX, transform.position, transform.rotation);
        Destroy(explosion, 0.5f);
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