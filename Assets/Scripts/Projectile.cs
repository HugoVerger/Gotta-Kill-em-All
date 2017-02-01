using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class Projectile : MonoBehaviour
{
    public float projectileSpeed = 3;
    public Vector2 direction = new Vector2(0, 1);
    public AudioClip playerShot;
    public AudioClip playerShot2;

    void Start()
    {
        int i = Random.Range(0, 2);
        if (i == 1)
        {
            gameObject.GetComponent<AudioSource>().clip = playerShot;
        } else
        {
            gameObject.GetComponent<AudioSource>().clip = playerShot2;
        }        
        gameObject.GetComponent<AudioSource>().Play();
    }
    void Update()
    {
        transform.Translate(direction * projectileSpeed * Time.deltaTime);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag != "Player")
        {
            //Destroy(other.gameObject);
            DestroyObject(gameObject);
        }
    }
}
