using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public float projectileSpeed = 3;
    public Vector2 direction = new Vector2(0, 1);

    private void Start()
    {
        AudioSource audio = GetComponent<AudioSource>();
        audio.Play();
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
